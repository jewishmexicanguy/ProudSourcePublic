using System;
using Npgsql;
using System.Data;

namespace ProudSource
{
	public class LoginData : PSUIQuery
	{
		private VerifyClientData login;

        private string query = @"SELECT * FROM user_master WHERE user_login = @UserName AND password = @Password";

		public LoginData(VerifyClientData loginevent) : base()
		{
			login = loginevent;
		}

		public bool authenticated = false;

        public int UserID { get; set; }

        public string UserName { get; set; }

		public void login_User()
		{
            /// Prepare connection to PostGre SQL server
            NpgsqlDataAdapter da = new NpgsqlDataAdapter (query, conn);

			/// Prepare data to be validated for login authentication
			da.SelectCommand.Parameters.AddWithValue ("@UserName",login.UserName);
			da.SelectCommand.Parameters.AddWithValue ("@Password", login.Password);

			/// Prepare datset to house results
			DataSet ds = new DataSet ();

			/// Open connection and fire sql command with parameters
			try
            {
				conn.Open();
				da.Fill(ds);
			}
            catch (Exception e)
            {
                authenticated = false;
			}
            finally
            {
				int results = ds.Tables [0].Rows.Count;
				if (results == 1)
                {
                    authenticated = true;
                    UserID = (int)ds.Tables[0].Rows[0]["user_master_id"];
                    UserName = (string)ds.Tables[0].Rows[0]["user_login"];
				}
                else
                {
                    authenticated = false;
				}
			}
		}
	}
}

