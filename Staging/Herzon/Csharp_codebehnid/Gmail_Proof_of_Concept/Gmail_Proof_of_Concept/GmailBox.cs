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

namespace Gmail_Proof_of_Concept
{
    /// <summary>
    /// This class will serve to allow acces to our gmail account using a service account tied to our Google Domain.
    /// 
    /// The certificate used for authentication will be prepared at class instantiation.
    /// 
    /// Authorization will be a protected method that will be called by other public methods defined later in this class.
    /// In this way we do not expose the authenticaton action to anything ona ny layer above this class.
    /// </summary>
    class GmailBox
    {
        private X509Certificate2 certificate;
        /// <summary>
        /// Did this work?
        /// </summary>
        private const string ServiceAccountEmail = "proudsourcehermes@elegant-works-115921.iam.gserviceaccount.com";

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
        protected const string ProudSourceEmail = "ryan@proudsource.us";

        /// <summary>
        /// Class constructor.
        /// </summary>
        public GmailBox()
        {
            // This weird oneliner here gets the folder path of the where current executing program came from, we will use this to tell the program where to find our service account certificate.
            PrivateKeyFile = System.IO.Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Path));
            // Our certificate is in two directories up from the results of reflection. I counted 10 characters, that should allow this to be generic enough to run on any machine. "Crosses fingers"
            PrivateKeyFile = PrivateKeyFile.Remove(PrivateKeyFile.Count() - 10) + @"\Resources\Hermes-232edbf43f3e.p12";
            // Prepare our certificate for use.
            certificate = new X509Certificate2(PrivateKeyFile, Secret, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);
        }

        /// <summary>
        /// Private method that requests for a credential with specified scopes using our p12 keyfile.
        /// </summary>
        /// <returns></returns>
        private ServiceAccountCredential get_GmailCred()
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
        /// This public method will retrive the number of gmails that are in the Gmail Inbox of the user email especified in this class.
        /// </summary>
        /// <returns></returns>
        public int get_number_of_Gmails()
        {
            // Get a credential using our certificate.
            ServiceAccountCredential cred = get_GmailCred();

            // instantiate a service object.
            GmailService service = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = cred });

            // execute our request and store the list returned from google.
            ListMessagesResponse response = service.Users.Messages.List(ProudSourceEmail).Execute();

            // return the count of mesages in the list returned to us.
            return response.Messages.Count;
        }
    }
}
 