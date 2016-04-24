using System;
using System.Data;
using Npgsql;

namespace ProudSource
{
    public class CreateEntreprenuerProfile : PSUIQuery
    {
        public VerifyClientData newprofile;
        public bool valid;
        private string StoredProcedure = "create_entrepreneur";

        public CreateEntreprenuerProfile(VerifyClientData profile) : base()
        {
            newprofile = profile;
            valid = false;
        }

        /// <summary>
        /// Calling this method will attempt to make a new entreprenuer profile.
        /// This is achived by making a new rown and associating that table entry with the userID that is passed into this page from the client's session variable.
        /// But first a check will be run to make sure that data is safe to be passed to our stored procedure.
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="entreprenuer_name"></param>
        /// <returns></returns>
        public int create_entreprenuer(int UserID, string entreprenuer_name)
        {
            // verify that the input parameters are safe, if safe set our variable valid to true.
            verify(UserID, entreprenuer_name);

            // if safe continue to makeing a new entreprenuer record.
            if (valid)
            {
                // call our stored procedure
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(StoredProcedure, conn);
                da.SelectCommand.Parameters.AddWithValue("", UserID);
                da.SelectCommand.Parameters.AddWithValue("", entreprenuer_name);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                try
                {
                    conn.Open();
                    da.Fill(ds);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("{0}, {1}, {2}", e.Message, e.InnerException, e.Data));
                }
                finally
                {
                    conn.Close();
                }

                // try to return the entreprenuerID returned by calling our stored procedure
                try
                {
                    return (int)ds.Tables[0].Rows[0]["create_entrepreneur"];
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("{0}, {1}, {2}", e.Message, e.InnerException, e.Data));
                    return -1;
                }
            }
            else
            {
                // this failes then return -1, which will be what we use to denote a failure to make an entreprenuer
                return -1;
            }
        }

        /// <summary>
        /// This private method is what checks the user input and makes sure that it is sql safe to be used.
        /// For now it will guard against buffer overflows and buffer underflows.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="name"></param>
        private void verify(int userID, string name)
        {
            /// <TODO>Do some kind of check on userID</TODO>
            if (name.Contains("'"))
            {
                valid = false;
                return;
            }
            if (name.Length > 255)
            {
                valid = false;
                return;
            }
            if (name.Length < 0)
            {
                valid = false;
                return;
            }
            if (name.Contains("$"))
            {
                valid = false;
                return;
            }
            if (name.Contains("-"))
            {
                valid = false;
                return;
            }
            if (name.Contains("@"))
            {
                valid = false;
                return;
            }
            else
            {
                valid = true;
            }
        }
    }
}