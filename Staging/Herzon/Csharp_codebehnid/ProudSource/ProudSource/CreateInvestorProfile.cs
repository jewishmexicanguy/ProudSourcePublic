using System;
using System.Data;
using Npgsql;

namespace ProudSource
{
    public class CreateInvestorProfile : PSUIQuery
    {
        public VerifyClientData newprofile;
        public bool valid;
        private string StoredProcedure = "create_investor";

        public CreateInvestorProfile(VerifyClientData profile) : base()
        {
            newprofile = profile;
            valid = false;
        }

        /// <summary>
        /// Calling this method will attempt to make a new investor profile by filling a new row 
        /// with that profile's User ID for relational purposes and that profiles custom name.
        /// A check will be run first to make sure that the parameters are not bad.
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="investor_name_str"></param>
        /// <returns></returns>
        public int create_investor(int UserID, string investor_name_str)
        {
            // verify that input data is safe
            verify(UserID, investor_name_str);

            // if the data is safe then pass it to our stored procedure "create_investor(integer, character varying)"
            if (valid)
            {
                // if safe continue to call the stored procedure that will have this profile made
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(StoredProcedure, conn);
                da.SelectCommand.Parameters.AddWithValue("@user_master_id", UserID);
                da.SelectCommand.Parameters.AddWithValue("@new_investor_profile_name", investor_name_str);
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

                // set InvestorID to be the ID retunred by the stored procedure called
                try
                {
                    return (int)ds.Tables[0].Rows[0]["create_investor"];
                }
                // If for some reason this fails then there was nothing returned and creating anew user failed.
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("{0}, {1}, {2}", e.Message, e.InnerException, e.Data));
                    return -1;
                }
            }
            else
            {
                // if creating user was unsuccesfull due to unsafe input then return -1, which will be used as the fail signal.
                return -1;
            }
        }

        /// <summary>
        /// This private method will check to make sure that "userID" is sql safe and also not going to cause an application 
        /// buffer overflow.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="name"></param>
        private void verify(int userID,  string name)
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