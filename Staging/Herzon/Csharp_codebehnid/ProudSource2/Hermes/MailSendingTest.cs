using Google.Apis.Auth.OAuth2;
using Google.Apis.Http;
using Google.Apis.Json;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace Hermes
{
    class MailSendingTest
    {
        private string[] scopes = { GmailService.Scope.GmailModify };
        private string ApplicationName = "Hermes";

        private MailMessage createEmail(String to, String from, String body, String subject)
        {

            MailMessage result = new MailMessage(from, to, subject, body);
            /// TODO: Deprecated
            result.ReplyTo = new MailAddress(from);
            result.BodyTransferEncoding = TransferEncoding.Base64;
            return result;
        }

        public void run()
        {
            ServiceAccountCredential serviceCred;

            String serviceEmail = "proudsourcehermes@elegant-works-115921.iam.gserviceaccount.com";
            String userEmail = "herzon@proudsource.us";
            String ryanEmail = "ryan@proudsource.us";

            String to = "murphy2902@gmail.com";


            using (var stream =
                  new FileStream("service_account.json", FileMode.Open, FileAccess.Read))
            {
                var certificate = new X509Certificate2(@"key.p12", "notasecret", X509KeyStorageFlags.Exportable);

                serviceCred = new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(serviceEmail)
                {
                    Scopes = scopes,
                    User = userEmail
                }.FromCertificate(certificate));


            }

            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {

                HttpClientInitializer = serviceCred,
                ApplicationName = ApplicationName,
                
            });

            Draft message = new Draft();
            Message content = new Message();
            content.Raw = createEmail(to, userEmail, "Asdf", "Asdf").ToString();
            message.Message = content;
            // Define parameters of request.
            UsersResource.DraftsResource.CreateRequest request = service.Users.Drafts.Create(message, "me");
      
            
            request.Execute();

            UsersResource.DraftsResource.SendRequest sendReq = service.Users.Drafts.Send(message, "me");

            sendReq.Execute();

        }
    }
}
