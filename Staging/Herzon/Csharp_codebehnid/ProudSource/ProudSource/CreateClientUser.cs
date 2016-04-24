using System;
using System.Data;
using Npgsql;

namespace ProudSource
{
	public class CreateClientUser : PSUIQuery
	{
		public VerifyClientData newuser;
		public bool valid = false;
		public bool usercreated = false;
		private int email_length, username_length, password_length;
        private string query = @"INSERT INTO user_master (user_login, password, email, mod_user_master_id) VALUES (@UserName, @Password, @Email, 2)";

        /// <summary>
        /// Class constructor, at instantiation this class consumes our VerifyClientData object and it is verified on instatiation.
        /// </summary>
        /// <param name="newuserdata"></param>
		public CreateClientUser (VerifyClientData newuserdata) : base()
		{
            newuser = newuserdata;

            // Verify the new user data is not a buffer overflow attempt.
            check();
		}

        /// <summary>
        /// This method will verify the data of this object, it will only be called privatley.
        /// </summary>
		private void check()
		{
			try
            {
				email_length = newuser.Email.Length;
				username_length = newuser.UserName.Length;
				password_length = newuser.Password.Length;

			}
            catch (Exception e)
            {
				valid = false;
			} 

			if (email_length > 255 | username_length > 255 | password_length > 255)
            {
				valid = false;
			}
            else if (email_length > 10 & username_length > 10 & password_length > 10)
            {
				valid = true;
			}
            else
            {
				valid = false;
			}
		}

        /// <summary>
        /// This method is responsible for directly creating our user record.
        /// </summary>
		public void create_user()
		{
			/// Prepare connection to PostGre SQL server
			Npgsql.NpgsqlCommand command = new NpgsqlCommand (query, conn);

			/// Prepare data to be validated for login authentication
			command.Parameters.AddWithValue ("@UserName",newuser.UserName);
			command.Parameters.AddWithValue ("@Password", newuser.Password);
			command.Parameters.AddWithValue ("@Email", newuser.Email);

			/// Open connection and fire sql command with parameters
			try
            {
				conn.Open();
				command.ExecuteNonQuery();
			}
            catch (Exception e)
            {
                usercreated = false;
			}
            finally
            {
                conn.Close();
			}
		}
	}
}

