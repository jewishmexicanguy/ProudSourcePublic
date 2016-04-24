using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using System.Net.Mail;
using MimeKit;
using System.IO;
using MimeKit.Encodings;

namespace Hermes
{
    /// <summary>
    /// This class will serve to allow acces to our gmail account using a service account tied to our Google Domain.
    /// 
    /// The certificate used for authentication will be prepared at class instantiation.
    /// 
    /// Authorization will be a protected method that will be called by other public methods defined later in this class.
    /// In this way we do not expose the authenticaton action to anything on any layer above this class.
    /// </summary>
    class GmailBox : EmailBox
    {
        private X509Certificate2 certificate;
        private string ServiceAccountEmail;

        /// <summary>
        /// This will house where the path to our service account P12 certificate lives in our application.
        /// </summary>
        private string PrivateKeyFile;

        /// <summary>
        /// The secret password that google gave us for use with this certificate.
        /// </summary>
        private const string Secret = "notasecret";

        /// <summary>
        /// The email that will be logged into using this certificate.
        /// </summary>
        //protected const string ProudSourceEmail = "ryan@proudsource.us";
        protected const string ProudSourceEmail = "herzon@proudsource.us";

        /// <summary>
        /// Class constructor.
        /// </summary>
        public GmailBox(string keyFileName, string serviceEmail)
        {
            ServiceAccountEmail = serviceEmail;
            // This weird oneliner here gets the folder path where the current executing binary is, we will use this to tell the program where to find our service account certificate.
            //PrivateKeyFile = System.IO.Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Path));
            // Our certificate is in two directories up from the results of reflection. I counted 10 characters, that should allow this to be generic enough to run on any machine. "Crosses fingers"
            //PrivateKeyFile = PrivateKeyFile.Remove(PrivateKeyFile.Count() - 10) + @"\Resources\" + keyFileName;
            PrivateKeyFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Hermes-232edbf43f3e.p12");
            // Prepare our certificate for use.
            certificate = new X509Certificate2(PrivateKeyFile, Secret, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);
        }

        /// <summary>
        /// Private method that requests for a credential with specified scopes using our p12 keyfile.
        /// </summary>
        /// <returns></returns>
        private ServiceAccountCredential getGmailCred()
        {
            ServiceAccountCredential credential = new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(ServiceAccountEmail)
                {
                    User = ProudSourceEmail,
                    Scopes = new[] { GmailService.Scope.GmailReadonly, GmailService.Scope.GmailModify }
                }.FromCertificate(certificate)
            );
            return credential;
        }

        /// <summary>
        /// This public method will retrieve the number of gmails that are in the Gmail Inbox of the user email especified in this class.
        /// </summary>
        /// <returns></returns>
        public int getNumberOfEmails()
        {
            // Get a credential using our certificate.
            ServiceAccountCredential cred = getGmailCred();

            // instantiate a service object.
            GmailService service = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = cred });

            // execute our request and store the list returned from google.
            ListMessagesResponse response = service.Users.Messages.List(ProudSourceEmail).Execute();
            // return the count of mesages in the list returned to us.

            return response.Messages.Count;
        }

        /// <summary>
        /// Gets all of the emails from a selected account.  This is a 
        /// proof of concept and probably shouldn't be used by the
        /// application.
        /// </summary>
        /// <returns>An IList of every message.</returns>
        public IList<MailMessage> getEmails()
        {
            IList<MailMessage> result = new List<MailMessage>();

            GmailService service = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = getGmailCred() });
            IList<Message> messages = service.Users.Messages.List(ProudSourceEmail).Execute().Messages;

            foreach (Message message in messages) {

                result.Add(getEmail(message.Id)); UsersResource.MessagesResource.GetRequest getReq = new UsersResource.MessagesResource.GetRequest(service, "me", message.Id);
                getReq.Format = UsersResource.MessagesResource.GetRequest.FormatEnum.Full;

                Message fullMessage = getReq.Execute();

                MailMessage decodedMessage = convertToMailMessage(fullMessage);
                
                result.Add(decodedMessage);
            }

            return result;
        }

        /// <summary>
        /// Gets all of the labels from the email address.
        /// </summary>
        /// <returns>A list of all labels.</returns>
        public IList<Label> getLabels()
        {
            GmailService service = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = getGmailCred() });
            IList<Label> labels = service.Users.Labels.List(ProudSourceEmail).Execute().Labels;
            return labels;
        }

        /// <summary>
        /// Gets a specific MailMessage by ID.
        /// </summary>
        /// <param name="ID">The ID of the requested message.</param>
        /// <returns>A MailMessage of the requested message.</returns>
        public MailMessage getEmail(string ID)
        {
            GmailService service = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = getGmailCred() });
            UsersResource.MessagesResource.GetRequest getReq = new UsersResource.MessagesResource.GetRequest(service, "me", ID);
            Message result = getReq.Execute();
            
            return convertToMailMessage(result);
        }

        /// <summary>
        /// Private utility class to convert a Message into a MailMessage
        /// </summary>
        /// <param name="message">The Message to be converted.</param>
        /// <returns>The passed Message in the form of a MailMessage</returns>
        private MailMessage convertToMailMessage( Message message )
        {
            string to = "";
            string from = "";
            string subject = "";
            string replyTo = "";
            string body, id, date, mimetype;

            date = message.InternalDate.ToString();
            mimetype = message.Payload.MimeType;
            id = message.Id;

            foreach ( MessagePartHeader header in message.Payload.Headers)
            {
                switch(header.Name)
                {
                    case "From":
                        from = header.Value;
                        break;
                    case "To":
                        to = header.Value;
                        break;
                    case "Subject":
                        subject = header.Value;
                        break;
                    case "Delivered-To":
                        break; // Maybe use this as address?
                    case "Reply-To":
                        replyTo = header.Value;
                        break; // Maybe use this as the from?
                    case "Date":
                        break; // We don't want to use this date.
                    default:
                        //Console.WriteLine(header.Name + " - " + header.Value);
                        break;
                }
            }

            MailMessage result = new MailMessage();

            Console.WriteLine("AID: " + message.Payload.Body.AttachmentId);
            Console.WriteLine("Body size: " + message.Payload.Body.Size);
            Console.WriteLine("MimeType: " + mimetype);
            Console.WriteLine("");


            try
            {
                result = new MailMessage(from, to);

                result.Subject = subject;
                if (message.Payload.Body.Size == 0)
                {
                    result.Body = "EMPTY MESSAGE";
                }
                else
                {
                    result.Body = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(message.Payload.Body.Data));
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("FROM:\t" + from);
                Console.WriteLine("TO:\t" + to);
            }

            return result;
        }
        
        public IList<MailMessage> getUnreadEmails()
        {
            // The list of messages that we're going to return
            IList<MailMessage> result = new List<MailMessage>();

            // First we need to find the ID of the UNREAD label, which we'll use
            // to select all of the unread messages.
            GmailService service = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = getGmailCred() });
            IList<Label> labels = service.Users.Labels.List(ProudSourceEmail).Execute().Labels;

            string unreadID = "";

            foreach( Label l in labels )
            {
                if(l.Name == "UNREAD")
                {
                    unreadID = l.Id;
                }
            }

            // This should be impossible, because UNREAD is a system label
            // rather than a user label
            if(unreadID == "")
            {
                throw new Exception();
            }

            // And now we see what messages we have.
            UsersResource.MessagesResource.ListRequest listRequest = service.Users.Messages.List(ProudSourceEmail);
            // We specify that we only want unread messages
            listRequest.LabelIds = unreadID;

            IList<Message> messages = listRequest.Execute().Messages;

            // Now we get the full messages, and add them to a list
            foreach(Message message in messages)
            {
                // We have to get each email -- we've only listed them up until this point
                result.Add(getEmail(message.Id));
                UsersResource.MessagesResource.GetRequest getReq = new UsersResource.MessagesResource.GetRequest(service, "me", message.Id);
                getReq.Format = UsersResource.MessagesResource.GetRequest.FormatEnum.Full;

                Message fullMessage = getReq.Execute();
                MailMessage decodedMessage = convertToMailMessage(fullMessage);

                // Now we mark the message as read
                ModifyMessageRequest modMessageReq = new ModifyMessageRequest();
                // Because of the request parameters, we neeed to make a list.
                IList < string > labelIDs = new List<string>(); labelIDs.Add(unreadID);
                // We tell the request what labels we want to remove
                modMessageReq.RemoveLabelIds = labelIDs;
                // And execute
                //service.Users.Messages.Modify(modMessageReq, ProudSourceEmail, fullMessage.Id).Execute();

                result.Add(decodedMessage);
            }

            return result;
        }

        public void sendEmail(MailMessage mailMessage)
        {
            GmailService service = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = getGmailCred() });


            // I really hate having to use this, but Microsoft doesn't let the average user 
            // get at the raw RFC 2822 whatever internals.  So...
            // TODO:  Standardize w/o external library
            AE.Net.Mail.MailMessage msg = new AE.Net.Mail.MailMessage
            {
                Subject = mailMessage.Subject,
                Body = mailMessage.Body,
                From = mailMessage.From
            };

            foreach(MailAddress addr in mailMessage.To)
            {
                msg.To.Add(addr);
            }

            msg.ReplyTo.Add(msg.From); // Bounces without this!!
            var msgStr = new StringWriter();
            msg.Save(msgStr);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(msgStr.ToString());

            var result = service.Users.Messages.Send(new Message
            {
                Raw = Convert.ToBase64String(bytes)
                    .Replace('+', '-')
                    .Replace('/', '_')
                    .Replace("=", "")
            }, "me").Execute();
        }
    }
}
