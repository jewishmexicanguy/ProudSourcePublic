using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ConsoleDataBaseExample
{
    /// <summary>
    /// A small basic class with a protected connection object to be inherited by later classes.
    /// </summary>
    public class BasicDBConnection
    {
        /// <summary>
        /// A connection object, it uses a connection string deffined in app.config.
        /// </summary>
        protected SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConsoleDataBaseExample.Properties.Settings.ProudSourceDB"].ConnectionString);

        /// <summary>
        /// Class constructor.
        /// </summary>
        protected BasicDBConnection()
        {

        }
    }

    /// <summary>
    /// This class inherits the more basic class BasicDBConnection.
    /// </summary>
    public class SelectQueries : BasicDBConnection
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        public SelectQueries() : base()
        {

        }

        /// <summary>
        /// This method uses the stored procedure sp_get_User_Messages to acquire the set of messages from our tables that are outbound.
        /// </summary>
        /// <returns>A DataSet object containing the result set from our tables.</returns>
        public DataSet get_Outbound_UserMessages()
        {
            string query = "sp_get_User_Messages";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet set = new DataSet();
            try
            {
                // Open the connection to the data base.
                conn.Open();
                adapter.Fill(set);
            }
            catch (System.Exception e)
            {
                // Do something with the error message.
            }
            finally
            {
                // Always close the connections that you open to the data base. If you don't you can cause the sql server to crash if too many connections are opened and not closed.
                conn.Close();
            }
            return set;
        }
    }

    /// <summary>
    /// This class also inherits the more basic class BasicDBConnection.
    /// </summary>
    public class ManagementActions : BasicDBConnection
    {
        /// <summary>
        /// Private resident string.
        /// </summary>
        private string create_test_message_string = "INSERT INTO Email_Messages([Origin_Address], [Destination], [Inbound], [Outbound], [Subject], [Body], [Reference_ID], [Reference_Type]) VALUES (@Origin_Address, @Destination, @Inbound, @Outbound, @Subject, @Body, @Reference_ID, @Reference_Type);";
        
        /// <summary>
        /// This is an accessor, it acts like a resident but it can be set or retrived
        /// </summary>
        public string origin_address { get; set; }
        public string destination { get; set; }
        public bool inbound { get; set; }
        public bool outbound { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public int reference_id { get; set; }
        public int reference_type { get; set; }

        public void make_userTestMessages(int repititions)
        {
            // call private method that sets our accessors for this class.
            _setAccessors();
            // instantiate a sql command object, similar to the dataadapter but it does not return a set nor does it expect to recive one.
            SqlCommand command = new SqlCommand(create_test_message_string, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@Origin_Address", origin_address);
            command.Parameters.AddWithValue("@Destination", destination);
            command.Parameters.AddWithValue("@Inbound", inbound);
            command.Parameters.AddWithValue("@Outbound", outbound);
            command.Parameters.AddWithValue("@Subject", subject);
            command.Parameters.AddWithValue("@Body", body);
            command.Parameters.AddWithValue("@Reference_Type", reference_type);
            command.Parameters.Add("@Reference_ID", SqlDbType.Int);
            for (int i = 0; i < repititions; i++)
            {
                try
                {
                    // adding some variation to the reference_id.
                    command.Parameters["@Reference_ID"].Value = reference_id + i;
                    conn.Open();
                    command.ExecuteNonQuery();
                }
                catch (System.Exception e)
                {
                    /// do something here with error message e if you want.
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Private method that sets values for the accessors of this class.
        /// </summary>
        private void _setAccessors()
        {
            origin_address = "herzon@proudsource.us";
            destination = "someperson@someserver.com";
            inbound = false;
            outbound = true;
            subject = "Test message";
            body = "Here be some words";
            reference_id = 1200;
            reference_type = 1;
        }
    }
}