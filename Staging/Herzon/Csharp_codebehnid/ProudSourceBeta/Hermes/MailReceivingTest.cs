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
    class MailReceivingTest
    {
        private string[] scopes = { GmailService.Scope.GmailModify };
        private string ApplicationName = "Hermes";

        private String keyFile;

        public MailReceivingTest( String myKey )
        {
            keyFile = myKey;
        }

        public void run()
        {
            ServiceAccountCredential serviceCred;

            String serviceEmail = "proudsourcehermes@elegant-works-115921.iam.gserviceaccount.com";
            String userEmail = "herzon@proudsource.us";
            String ryanEmail = "ryan@proudsource.us";

            using (var stream =
                  new FileStream("service_account.json", FileMode.Open, FileAccess.Read))
            {
                var certificate = new X509Certificate2(keyFile, "notasecret", X509KeyStorageFlags.Exportable);

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
                ApplicationName = ApplicationName
            });

            // Define parameters of request.
            UsersResource.MessagesResource.ListRequest getReq = service.Users.Messages.List("me"); ;
            IList<Message> messages = getReq.Execute().Messages;

            Console.WriteLine("$ $ $ $\n = Messages received:\n");
            foreach( Message message in messages ){
                Console.WriteLine(message.ToString());
            }
            Console.Read();
        }
    }
}
