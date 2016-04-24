using Google.Apis.Gmail.v1.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Hermes
{
    class EmailDatabaseWrapper 
    {
        PSqlAdapter database;
        public EmailDatabaseWrapper( String address )
        {
            database = new PSqlAdapter();
            database.connect(address);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">The message to be inserted.</param>
        /// <returns>Status of the operation.
        /// /returns>
        public bool insertInboundEmail(MailMessage message)
        {
            string commandString = "sp_insert_Email_Message";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("@Gmail_UID", DBNull.Value.ToString());
            parameters.Add("@Gmail_UID", message.Headers.AllKeys.ToString());
            //parameters.Add("@Rfc_Message_ID", DBNull.Value.ToString());
            parameters.Add("@Origin", message.From.Address);
            // TODO:  We need to handle more than one To:
            //parameters.Add("@Destination", message.To.First().Address);
            parameters.Add("@Subject", message.Subject);
            parameters.Add("@Body", message.Body);
        
            return database.Insert(commandString, CommandType.StoredProcedure, parameters);
        }

        public void processUserMessage(string messageID)
        {
            string commandString = "sp_process_User_Message";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@Email_Message_ID", DBNull.Value.ToString());
            parameters.Add("@Processed", "1");

            database.Insert(commandString, CommandType.StoredProcedure, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All of the emails that have the property Outbound=true and Sent=false.</returns>
        public IList<MailMessage> getUnprocessedMessages()
        {
            IList<MailMessage> result = new List<MailMessage>();

            string query = "SELECT * FROM Email_Messages WHERE sent = false AND outbound = true";
            DataSet messages = database.Query(query);

            foreach( DataTable table in messages.Tables)
            {
                foreach( DataRow row in table.Rows){
                    String to = row.Field<string>("Destination");
                    String from = row.Field<string>("Origin_Address");
                    String body = row.Field<string>("Body");
                    String subject = row.Field<string>("Subject");
                    
                    result.Add(new MailMessage(from, to, subject, body));
                }
            }

            return result;
        }
    }
}
