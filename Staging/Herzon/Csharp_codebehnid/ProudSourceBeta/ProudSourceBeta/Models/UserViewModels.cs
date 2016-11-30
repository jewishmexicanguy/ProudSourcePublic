using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;

namespace ProudSourceBeta.Models
{
    /// <summary>
    /// This class is used to gather the data to display about a user's profile.
    /// </summary>
    public class UserIndexViewModel : PSDataConnection
    {
        /// <summary>
        /// The user email for this account.
        /// </summary>
        [Display(Name = "User Email")]
        public string User_Email { get; private set; }
        /// <summary>
        /// Boolean storing whether or not the email has been verfied.
        /// </summary>
        [Display(Name = "Email Verified")]
        public bool Email_Verified { get; private set; }
        /// <summary>
        /// Byte array that will be converted into a picture.
        /// </summary>
        [Display(Name = "User Profile Picture")]
        public byte[] User_Image { get; private set; }

        public int Entrepreneur_ID { get; private set; }

        public string Entrepreneur_Name { get; private set; }

        public bool Entrepreneur_Public { get; private set; }

        public bool Entrepreneur_Verified { get; private set; }

        public byte[] Entrepreneur_Image { get; private set; }

        public int Project_Count { get; private set; }

        public List<decimal> Project_Funding_Levels { get; private set; }

        public int Investor_ID { get; private set; }

        public string Investor_Name { get; private set; }

        public bool Investor_Public { get; private set; }

        public bool Investor_Verified { get; private set; }

        public byte[] Investor_Image { get; private set; }

        public int PROC_Count { get; private set; }

        public DataRowCollection Investor_Pending_Transaction { get; private set; }

        public decimal Investor_Balance { get; private set; }

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="userIdentity_id">A string id that is used with .Net's identity system from it's auth tables</param>
        public UserIndexViewModel(string userIdentity_id) : base(userIdentity_id)
        {
            _get_UserData();
        }
        /// <summary>
        /// Private method that will get the user details for our user and fill out the accessors of this class.
        /// </summary>
        private void _get_UserData()
        {
            string query = "sp_get_UserDetails_with_Accounts";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@User_ID", User_Id);
            DataSet set = new DataSet();
            // we will be returned 9 tables
            //      0 : user details
            //      1 : user image
            //      2 : entrepreneurs details
            //      3 : entrepreneur image
            //      4 : entrepreneur projects
            //      5 : investor details
            //      6 : investor account balance
            //      7 : investor pending transactions
            //      8 : investor image
            //      9 : investor PROCs

            try
            {
                conn.Open();
                adapter.Fill(set);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            User_Email = (string)set.Tables[0].Rows[0]["Email"];
            Email_Verified = (bool)set.Tables[0].Rows[0]["Email_Verified"];
            if (set.Tables[1].Rows.Count > 0)
            {
                User_Image = (byte[])set.Tables[1].Rows[0]["Binary_Image"];
            }
            else
            {
                User_Image = null;
            }
            Entrepreneur_ID = (int)set.Tables[2].Rows[0]["Entrepreneur_ID"];
            Entrepreneur_Name = set.Tables[2].Rows[0]["Name"].ToString();
            Entrepreneur_Public = (bool)set.Tables[2].Rows[0]["Profile_Public"];
            Entrepreneur_Verified = (bool)set.Tables[2].Rows[0]["Verified"];
            if (set.Tables[3].Rows.Count > 0)
            {
                Entrepreneur_Image = (byte[])set.Tables[3].Rows[0]["Binary_Image"];
            }
            else
            {
                Entrepreneur_Image = null;
            }
            Project_Count = set.Tables[4].Rows.Count;
            if (set.Tables[4].Rows.Count > 0)
            {
                Project_Funding_Levels = new List<decimal>();
                foreach (DataRow i in set.Tables[4].Rows)
                {
                    if ((decimal)i["Project_Balance"] == 0)
                    {
                        Project_Funding_Levels.Add(0);
                    }
                    else
                    {
                        Project_Funding_Levels.Add((decimal)i["Project_Balance"] / (decimal)i["Investment_Goal"]);
                    }
                }
            }
            Investor_ID = (int)set.Tables[5].Rows[0]["Investor_ID"];
            Investor_Name = set.Tables[5].Rows[0]["Name"].ToString();
            Investor_Public = (bool)set.Tables[5].Rows[0]["Profile_Public"];
            Investor_Verified = (bool)set.Tables[5].Rows[0]["Verified"];
            if (set.Tables[6].Rows.Count > 0)
            {
                Investor_Balance = (decimal)set.Tables[6].Rows[0]["Investor_Balance"];
            }
            else
            {
                Investor_Balance = 0.0m;
            }
            if (set.Tables[8].Rows.Count > 0)
            {
                Investor_Image = (byte[])set.Tables[8].Rows[0]["Binary_Image"];
            }
            else
            {
                Investor_Image = null;
            }
            PROC_Count = set.Tables[8].Rows.Count;
            Investor_Pending_Transaction = set.Tables[7].Rows;
            return;
        }
    }
    /// <summary>
    /// This class is used to show user details to be updated and to update the user accounts.
    /// </summary>
    public class UserEditViewModel : PSDataConnection
    {
        /// <summary>
        /// Used to update the email for this account.
        /// </summary>
        [Display(Name = "User Email")]
        public string User_Email { get; set; }
        /// <summary>
        /// Used to update the specific image that is used for this user account.
        /// </summary>
        public int Image_Id { get; set; }
        /// <summary>
        /// Used to update the image that for this user account, is a byte array.
        /// </summary>
        [Display(Name = "User Image")]
        public byte[] User_Image { get; set; }
        /// <summary>
        /// Class constructor used when populating the EditView with user details.
        /// </summary>
        /// <param name="userIdentity_Id">The .Net Iidentity for this registered user.</param>
        public UserEditViewModel(string userIdentity_Id) : base(userIdentity_Id)
        {
            _get_UserData();
        }
        /// <summary>
        /// Class constructor used when the user's details are going to be updated.
        /// </summary>
        public UserEditViewModel() : base()
        {

        }
        /// <summary>
        /// The actual method that updates the user data.
        /// </summary>
        /// <param name="userID">User id for the account that will be updadted.</param>
        public void updateUser(int userID)
        {
            string query = "sp_update_UserAccount";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@user", userID);
            command.Parameters.AddWithValue("@Image_ID", Image_Id);
            command.Parameters.Add("@Image_Bytes", SqlDbType.VarBinary);
            command.Parameters.Add("@Email", SqlDbType.VarChar);
            if (User_Image == null)
            {
                command.Parameters["@Image_Bytes"].Value = DBNull.Value;
            }
            else
            {
                command.Parameters["@Image_Bytes"].Value = User_Image;
            }
            if (User_Email.Length <= 0)
            {
                command.Parameters["@Email"].Value = DBNull.Value;
            }
            else
            {
                command.Parameters["@Email"].Value = User_Email;
            }
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// Private method that gets the data to show for this user account.
        /// </summary>
        private void _get_UserData()
        {
            string query = "sp_get_UserEditDetails";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@User_ID", User_Id);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet set = new DataSet();
            try
            {
                conn.Open();
                adapter.Fill(set);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            // we will get two tables from this result  
            // 0:users
            // 1:images
            User_Email = (string)set.Tables[0].Rows[0]["Email"];
            try
            {
                Image_Id = (int)set.Tables[1].Rows[0]["Image_ID"];
                User_Image = (byte[])set.Tables[1].Rows[0]["Binary_Image"];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                Image_Id = 0;
                User_Image = null;
            }
            return;
        }
    }
    /// <summary>
    /// DO NOT USE, THIS IS NOW DEPRICATED
    /// 
    /// This class is used to create Entrepreneur accounts.
    /// </summary>
    public class UserCreateEntrepreneurViewModel : PSDataConnection
    {
        // Declare accessors for creating Entrepreneur accounts.
        public string Name { get; set; }
        /// <summary>
        /// Class constructor used when data will be placed for creating an account.
        /// </summary>
        public UserCreateEntrepreneurViewModel() : base()
        {

        }
        /// <summary>
        /// Class constructor used when user data is neccessary.
        /// </summary>
        /// <param name="IdentityUser_Id"></param>
        public UserCreateEntrepreneurViewModel(string IdentityUser_Id) : base(IdentityUser_Id)
        {

        }
        /// <summary>
        /// Method used to create an Entrepreneur account. Returns the Id of the Entrepreneur account.
        /// </summary>
        /// <param name="userId"></param>
        public int createEntrepreneur(int userId)
        {
            string query = "Entrepreneur_Create";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@User_ID", userId);
            command.Parameters.AddWithValue("@Entrepreneur_Name", Name);
            int entrepreneurId = 0;
            try
            {
                conn.Open();
                entrepreneurId = (int)command.ExecuteScalar();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            if (entrepreneurId == 0)
            {
                throw new Exception("Failed to make Entrepreneur Account");
            }
            return entrepreneurId;
        }
    }
    /// <summary>
    /// DO NOT USE, THIS IS NOW DEPRICATED
    /// 
    /// This class is used to creste Investor accounts.
    /// </summary>
    public class UserCreateInvestorViewModel : PSDataConnection
    {
        // Declare accessors for creating Investor accounts.
        public string Name { get; set; }
        /// <summary>
        /// Class constructor used when data will be placed for creating an account.
        /// </summary>
        public UserCreateInvestorViewModel() : base()
        {

        }
        /// <summary>
        /// Class constructor used when user data is neccessary.
        /// </summary>
        /// <param name="IdentityUser_Id"></param>
        public UserCreateInvestorViewModel(string IdentityUser_Id) : base(IdentityUser_Id)
        {

        }
        /// <summary>
        /// Method used to create an Investor account. Returns the id of the Investor account.
        /// </summary>
        /// <param name="userId"></param>
        public int createInvestor(int userId)
        {
            string query = "Investor_Create";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@User_ID", userId);
            command.Parameters.AddWithValue("@Investor_Name", Name);
            int investorId = 0;
            try
            {
                conn.Open();
                investorId = (int)command.ExecuteScalar();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            if (investorId == 0)
            {
                throw new Exception("Failed to make Investor Account");
            }
            return investorId;
        }
    }
}