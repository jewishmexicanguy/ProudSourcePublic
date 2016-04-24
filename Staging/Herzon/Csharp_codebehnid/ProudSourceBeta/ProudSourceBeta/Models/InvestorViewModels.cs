using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System;

namespace ProudSourceBeta.Models
{
    // we need a super class that has a method to check the relation between the user and the investor.
    public class InvestorInitialize : PSDataConnection
    {
        /// <summary>
        /// Investor identity
        /// </summary>
        public int Investor_ID { get; set; }
        /// <summary>
        /// Accessor to whether or not this investor object is realted to the user the client is. 
        /// </summary>
        public bool Valid { get; private set; }
        /// <summary>
        /// Accessor for the name of this investor.
        /// </summary>
        [Display(Name = "Profile Name")]
        public string Name { get; set; }
        /// <summary>
        /// Construcotr to be used for accepting incoming data to update this investor.
        /// </summary>
        public InvestorInitialize() : base()
        {

        }
        /// <summary>
        /// Constructor to be used to display Investor data.
        /// </summary>
        /// <param name="identityUser_Id"></param>
        /// <param name="input_Investor_Id"></param>
        public InvestorInitialize(string identityUser_Id, int input_Investor_Id) : base(identityUser_Id)
        {
            Investor_ID = input_Investor_Id;
            _auth_Investor_User_Relation();
        }
        /// <summary>
        /// Use this to determine if the user is really related to this investor.
        /// </summary>
        public void _auth_Investor_User_Relation()
        {
            string query = "SELECT COUNT(*) FROM Investors I WHERE I.[Investor_ID] = @Investor_ID AND I.[User_ID] = @User_ID";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@User_ID", User_Id);
            command.Parameters.AddWithValue("@Investor_ID", Investor_ID);
            int result = 0;
            try
            {
                conn.Open();
                result = (int)command.ExecuteScalar();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            if (result == 1)
            {
                Valid = true;
            }
            else
            {
                Valid = false;
            }
        }
    }

    // we need a class to display investor index.
    public class InvestorIndexViewModel : InvestorInitialize
    {
        /// <summary>
        /// Accessor that houses a collection of PROCS related to this investor.
        /// </summary>
        [Display(Name = "PROCs")]
        public DataRowCollection PROCS_Collection { get; set; }
        /// <summary>
        /// Byte array accessor that houses the investor's profile image.
        /// </summary>
        [Display(Name = "Profile Image")]
        public byte[] Profile_Picture { get; set; }
        /// <summary>
        /// Is the investor verified by prousource.
        /// </summary>
        public bool Verified { get; set; }
        /// <summary>
        /// Is the investor's profile public?
        /// </summary>
        [Display(Name = "Profile Public")]

        public bool Profile_Public { get; set; }

        public int Viewer_ID { get; set; }

        [Display(Name = "Balance of USD")]
        public decimal Balance_USD { get; set; }

        [Display(Name = "Balance of BTC")]
        public decimal Balance_BTC { get; set; }

        public DataRowCollection Pending_Transactions { get; set; }

        public int Financial_Account_ID { get; set; }

        /// <summary>
        /// This class constructor is used when registered users view an investor detail page, there is a caveat however; the view that this object renders to checks to make sure that the view_id and investor_id are not the same if they are it does not display a message investor field.
        /// </summary>
        /// <param name="identityUser_Id"></param>
        /// <param name="input_Investor_id"></param>
        /// <param name="viewer_id"></param>
        public InvestorIndexViewModel(string identityUser_Id, int input_Investor_id, int viewer_id)
        {
            get_InvestorData(input_Investor_id);
            Viewer_ID = viewer_id;
        }
        /// <summary>
        /// Constructor used to get the Investor's data.
        /// </summary>
        /// <param name="identityUser_Id"></param>
        /// <param name="input_Investor_id"></param>
        public InvestorIndexViewModel(string identityUser_Id, int input_Investor_id) : base(identityUser_Id, input_Investor_id)
        {
            get_InvestorData(Investor_ID);
        }
        /// <summary>
        /// Constructor used to get data to display to floaters and non owners of this investor account.
        /// </summary>
        public InvestorIndexViewModel() : base()
        {

        }
        /// <summary>
        /// Private method that retrives this investors data.
        /// </summary>
        public void get_InvestorData(int investor_id)
        {
            string query = "sp_get_InvestorDetails_with_PROCs";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Investor_ID", investor_id);
            DataSet set = new DataSet();
            if (investor_id <= 1)
            {
                throw new Exception("Investor id passed in is zero or less and is therefore an invalid id.");
            }
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
            // the following 6 sets are retunred from this procedure,
            //      [0] : investor details,
            //      [1] : investor images,
            //      [2] : investor PROCs,
            //      [3] : investor account balance in USD
            //      [4] : investor account balance in BTC
            //      [5] : investor account transactions pending
            //      [6] : investor account id

            Name = set.Tables[0].Rows[0]["Name"].ToString();
            Verified = (bool)set.Tables[0].Rows[0]["Verified"];
            Profile_Public = (bool)set.Tables[0].Rows[0]["Profile_Public"];
            try
            {
                Profile_Picture = (byte[])set.Tables[1].Rows[0]["Binary_Image"];
            }
            catch (IndexOutOfRangeException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                Profile_Picture = null;
            }
            try
            {
                PROCS_Collection = set.Tables[2].Rows;
            }
            catch (InvalidOperationException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                PROCS_Collection = null;
            }
            if(!DBNull.Value.Equals(set.Tables[3].Rows[0]["USD_Balance"]))
            {
                Balance_USD = (decimal)set.Tables[3].Rows[0]["USD_Balance"];
            }
            else
            {
                Balance_USD = 0.0m;
            }
            if (!DBNull.Value.Equals(set.Tables[4].Rows[0]["BTC_Balance"]))
            {
                Balance_BTC = (decimal)set.Tables[4].Rows[0]["BTC_Balance"];
            }
            else
            {
                Balance_BTC = 0.0m;
            }
            if(set.Tables[5].Rows.Count > 0)
            {
                Pending_Transactions = set.Tables[5].Rows;
            }
            else
            {
                Pending_Transactions = null;
            }
            Financial_Account_ID = (int)set.Tables[6].Rows[0]["Account_ID"];
        }
        /// <summary>
        /// Method that posts a message to our internal messages table for this investor account.
        /// </summary>
        /// <param name="investor_id">The id of thinvestor in question to post a message too</param>
        /// <param name="messenger_id">The id of the messanger sending the message.</param>
        /// <param name="messenger_type">A number that is tied to a type of user account. 1 : user messages, 2 : Financial messages, 3 : Investor messages, 4 : Entrepreneur messages, 5 : project messages</param>
        /// <param name="message">The message to post</param>
        public void message_Investor(int investor_id, int messenger_id, int messenger_type, string message)
        {
            string query = "sp_message_ProfileAccount";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Reciving_ID", investor_id);
            command.Parameters.AddWithValue("@Reciving_Type", 3);
            command.Parameters.AddWithValue("@Messenger_ID", messenger_id);
            command.Parameters.AddWithValue("@Reference_Type", messenger_type);
            command.Parameters.AddWithValue("@Message", message);
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
            return;
        }
    }

    // we need a class to display info to edit investor accounts and accept input info.
    public class InvestorEditViewModel : InvestorInitialize
    {
        /// <summary>
        /// Accessor for the profile image associated iwth this account.
        /// </summary>
        [Display(Name = "Profile Image")]
        public byte[] Profile_Picture { get; set; }
        /// <summary>
        /// Accessor for storing the image id of the profile picture.
        /// </summary>
        public int Image_ID { get; set; }
        /// <summary>
        /// Accessor for this profile if it is public.
        /// </summary>
        [Display(Name = "Profile Public")]
        public bool Profile_Public { get; set; }
        /// <summary>
        /// Class constructor used when the object needs to accept data to update the investor profile.
        /// </summary>
        public InvestorEditViewModel() : base()
        {

        }
        /// <summary>
        /// Class constructor used when initializing the object requires that it have the investor data with it.
        /// </summary>
        /// <param name="identityUser_Id"></param>
        /// <param name="input_Investor_id"></param>
        public InvestorEditViewModel(string identityUser_Id, int input_Investor_id) : base(identityUser_Id, input_Investor_id)
        {
            get_InvestorData();
        }
        /// <summary>
        /// This method will actually get the data to present to our investor user the record we have in our data base.
        /// </summary>
        private void get_InvestorData()
        {
            string query = "sp_get_InvestorDetails";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Investor_ID", Investor_ID);
            DataSet set = new DataSet();
            if (Investor_ID < 1)
            {
                throw new Exception("Invalid parameter for investor id was passed in.");
            }
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
            // There are two result sets
            //      [0] : investor details
            //      [1] : investor images
            if (set.Tables[0].Rows.Count < 1)
            {
                throw new Exception("No investor data");
            }
            Name = set.Tables[0].Rows[0]["Name"].ToString();
            Profile_Public = (bool)set.Tables[0].Rows[0]["Profile_Public"];
            if (set.Tables[1].Rows.Count < 1)
            {
                Profile_Picture = null;
            }
            else
            {
                Profile_Picture = (byte[])set.Tables[1].Rows[0]["Binary_Image"];
            }
            if (set.Tables[1].Rows.Count > 0)
            {
                Image_ID = (int)set.Tables[1].Rows[0]["Image_ID"];
            }
        }
        /// <summary>
        /// Method used to update this investore with data from the user client.
        /// </summary>
        public void updateInvestor()
        {
            if (!Valid)
            {
                return; // Do nothing.
            }
            string query = "sp_update_InvestorAccount";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Investor_ID", Investor_ID);
            command.Parameters.AddWithValue("@Profile_Public", Profile_Public);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@Image_ID", Image_ID);
            command.Parameters.Add("@Binary_Image", SqlDbType.VarBinary);
            if (Profile_Picture != null)
            {
                command.Parameters["@Binary_Image"].Value = Profile_Picture;
            }
            else
            {
                command.Parameters["@Binary_Image"].Value = DBNull.Value;
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
    }

}