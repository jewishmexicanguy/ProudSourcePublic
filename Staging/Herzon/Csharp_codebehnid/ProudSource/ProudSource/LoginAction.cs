using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Npgsql;

namespace ProudSource
{
    public class LoginAction : VerifyDataAction
    {
        private bool UserAuthenticated;
        private string query = @"SELECT * FROM ""Proudsource schema"".user_master U WHERE U.user_login = @UserName AND U.login_pswd = @Password";

        /// <summary>
        /// Initializes a new instance of the <see cref="ProudSource.LoginAction"/> class.
        /// </summary>
        public LoginAction()
        {
        }

        [JsonProperty(PropertyName = "UserName")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "PasswordHash")]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Checks the login data.
        /// </summary>
        public void CheckLoginData()
        {
            // Check length of the user name must be within 12 - 50 characters
            if (Username.Length >= 11 & Username.Length <= 50)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }

            // Chek length of password hash, must be above 10 - 255 characters
            if (PasswordHash.Length >= 10 & PasswordHash.Length <= 255)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ProudSource.LoginAction"/> user is authenticated.
        /// </summary>
        /// <value><c>true</c> if is user authenticated; otherwise, <c>false</c>.</value>
        public bool isUserAuthenticated { get { return UserAuthenticated; } }

        /// <summary>
        /// Inherited Abstract method that now overriden in this class to enable authentication of a user. 
        /// </summary>
        public override void execute_action()
        {
            /// Instantiate a connection object to our PostGreSQL server 
            Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDB"].ConnectionString);
            Npgsql.NpgsqlDataAdapter da = new NpgsqlDataAdapter(this.query, conn);
            /// prepare the data that will be compared against
            da.SelectCommand.Parameters.AddWithValue("@UserName", this.Username);
            da.SelectCommand.Parameters.AddWithValue("Password", this.PasswordHash);
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                conn.Open();
                da.Fill(ds);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("Error querying data base, user authentication. Error Data \r\n{0},\r\n{1}\r\n{2}", e.Message, e.InnerException, e.Data));
                UserAuthenticated = false;
            }
            finally
            {
                conn.Close();
            }
            /// Check for success or failure
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.UserAuthenticated = true;
            }
            else
            {
                this.UserAuthenticated = true;
            }
        }
    }
}