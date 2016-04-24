using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Gmail.v1;
using System.Net.Mail;

// TODO: Move over to the new .json way of authenticating, which is recommended


namespace Hermes
{
    public class Hermes
    {
        private static GmailBox inbox;
        private static EmailDatabaseWrapper database;

        public Hermes()
        {
            Main(new string[] { null });
        }

        static void Main(String[] args)
        {
            
            String keyFile = "Hermes-232edbf43f3e.p12";
            string serviceEmail = "proudsourcehermes@elegant-works-115921.iam.gserviceaccount.com";

            inbox = new GmailBox(keyFile, serviceEmail);
            string address = ConfigurationManager.ConnectionStrings["ProudSourceStaging"].ConnectionString;
            database = new EmailDatabaseWrapper(address);


            try
            {
                while (true)
                {
                    ///TODO Implement these as asynchronous tasks
                    testingRun();
                    receiveRun();
                    processRun();
                }
            }
            catch (StopProgramException e)
            {
                //Console.WriteLine("Received kill signal.. Shutting down");
            }
            catch (Exception e)
            {
                //Console.WriteLine("Caught exception of type: " + e.GetType());
                //Console.WriteLine(e.ToString());
            }
            //Console.Read();
        }

        static void testingRun()
        {
            IList<MailMessage> messages = inbox.getUnreadEmails();

            foreach (MailMessage message in messages)
            {
                //Console.WriteLine("To: " + message.To.First().Address);
                //Console.WriteLine("From: " + message.From.Address);
                //Console.WriteLine("Subject: " + message.Subject);
                //Console.WriteLine("Body: " + message.Body);
                //Console.Read();
                database.insertInboundEmail(message);
            }

            Console.Read();
            throw new StopProgramException();
        }

        //TODO: Implement as asynchronous tasks
        static void receiveRun()
        {
            IList<MailMessage> messages = inbox.getUnreadEmails();
            if( messages.Count != 0 )
            {
                foreach( MailMessage message in messages ){
                    database.insertInboundEmail(message);
                }
            }
        }

        static void processRun()
        {
            IList<MailMessage> messages = database.getUnprocessedMessages();
            
            if( messages.Count != 0 )
            {
                foreach( MailMessage message in messages)
                {
                    inbox.sendEmail(message);
                }
            }

        }
    }
}
