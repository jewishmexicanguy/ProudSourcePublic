using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System;

namespace ProudSourceBeta.Models
{
    /// <summary>
    /// This class is used to authorize relationships between the entrepreneur account and user account the client is accessing.
    /// </summary>
    public class EntrepreneurInitialize : PSDataConnection
    {
        /// <summary>
        /// Entrepreneur Id of this account.
        /// </summary>
        public int Entrepreneur_ID { get; set; }
        /// <summary>
        /// This accessor holds whether the relationship between the user and entrepreneur account actually exists. 
        /// </summary>
        public bool valid { get; private set; }
        /// <summary>
        /// Class constructor used when data is being displayed.
        /// </summary>
        /// <param name="entrepreneur_id"></param>
        /// <param name="userIdentity_id"></param>
        public EntrepreneurInitialize(int entrepreneur_id, string userIdentity_id) : base(userIdentity_id)
        {
            Entrepreneur_ID = entrepreneur_id;
            valid = _auth_Entrepreneur_User_Relation();
        }
        /// <summary>
        /// Class constructor used when this object will be accepting data to update the Entrepreneur account.
        /// </summary>
        public EntrepreneurInitialize() : base()
        {

        }
        /// <summary>
        /// This private method is what does the actual authentication.
        /// </summary>
        /// <returns></returns>
        private bool _auth_Entrepreneur_User_Relation()
        {
            string query = "SELECT COUNT(*) FROM Entrepreneurs E WHERE E.[Entrepreneur_ID] = @Entrepreneur_ID AND E.[User_ID] = @User_ID";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@Entrepreneur_ID", Entrepreneur_ID);
            command.Parameters.AddWithValue("@User_ID", User_Id);
            command.CommandType = CommandType.Text;
            int results = 0;
            try
            {
                conn.Open();
                results = (int)command.ExecuteScalar();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            if (results == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// This class is used to prepare data to be presented to for this Entrepreneur account. 
    /// </summary>
    public class EntrepreneurIndexViewModel : EntrepreneurInitialize
    {
        /// <summary>
        /// Accessor that is this account's name.
        /// </summary>
        [Display(Name = "Entrepreneur Name")]
        public string Name { get; set; }
        /// <summary>
        /// Accessor that denotes whether or not this entrepreneur account can be shown to the public.
        /// </summary>
        [Display(Name = "Profile is public")]
        public bool Profile_Public { get; set; }
        /// <summary>
        /// Accessor that holds whether or not this Entrepreneur has been audited.
        /// </summary>
        [Display(Name = "Entrepreneur has been audited")]
        public bool Verified { get; set; }
        /// <summary>
        /// Accessor that gets the byte array that is the profile picture for this profile.
        /// </summary>
        [Display(Name = "Profile Picture")]
        public byte[] Profile_Picture { get; set; }

        [Display(Name = "Number Projects owned")]
        public int Project_Count { get; private set; }

        public DataRowCollection Projects { get; private set; }

        public int Viewer_ID { get; set; }
        public EntrepreneurIndexViewModel() : base()
        {

        }
        /// <summary>
        /// Class constructor used when displaying information to the client user.
        /// </summary>
        /// <param name="entrepreneur_id">Entrepreneur account id</param>
        /// <param name="userIdentity_id">User account id from our tables</param>
        public EntrepreneurIndexViewModel(int entrepreneur_id, string userIdentity_id) : base(entrepreneur_id, userIdentity_id)
        {
            Entrepreneur_ID = entrepreneur_id;
            _get_EntrepreneurData(entrepreneur_id);
        }
        /// <summary>
        /// This class constructor is used when registered users view an entrepreneur detail page, there is a caveat however; the view that this object renders to checks to make sure that the view_id and entrepreneur_id are not the same if they are it does not display a message entrepreneur field.
        /// </summary>
        /// <param name="entrepreneur_id"></param>
        /// <param name="userIdentity_id"></param>
        /// <param name="viewer_id"></param>
        public EntrepreneurIndexViewModel(int entrepreneur_id, string userIdentity_id, int viewer_id) : base(entrepreneur_id, userIdentity_id)
        {
            Entrepreneur_ID = entrepreneur_id;
            Viewer_ID = viewer_id;
            _get_EntrepreneurData(entrepreneur_id);
        }
        /// <summary>
        /// This method actually gets the data for our model.
        /// </summary>
        /// <param name="entrepreneur_id"></param>
        public void _get_EntrepreneurData(int entrepreneur_id)
        {
            string query = "sp_get_EntrepreneurDetails_with_Projects";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Entrepreneur_ID", entrepreneur_id);
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
            // there will be 4 tables returned from our query
            //      0 : Entrepreneur Details
            //      1 : Entrepreneur Image
            //      2 : Projects owned by entrepreneur
            //      3 : Projects owned by entrepreneur that have a ballance

            // set this classes accessors with the results from our Entrepreneur record in our first set.
            DataRow row = set.Tables[0].Rows[0];
            Entrepreneur_ID = (int)row["Entrepreneur_ID"];
            Name = row["Name"].ToString();
            Profile_Public = (bool)row["Profile_Public"];
            Verified = (bool)row["Verified"];

            // set image bytes for our accessor if there is an image result in our second set.
            try
            {
                Profile_Picture = (byte[])set.Tables[1].Rows[0]["Binary_Image"];
            }
            catch (IndexOutOfRangeException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                Profile_Picture = null;
            }

            // place the results sets into some lists
            List<DataRow> projects_and_details = new List<DataRow>();
            if(set.Tables[2].Rows.Count > 0)
            {
                for(int i = 0; i < set.Tables[2].Rows.Count; i++)
                {
                    projects_and_details.Add(set.Tables[2].Rows[i]);
                }
            }

            List<DataRow> projects_with_finance_data = new List<DataRow>();
            if (set.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < set.Tables[3].Rows.Count; i++)
                {
                    projects_with_finance_data.Add(set.Tables[3].Rows[i]);
                }
            }

            // P.[Project_ID], P.[Name], P.[Profile_Public], P.[Investment_Goal], SUM(T.[Amount]) AS 'Project_Balance'
            DataTable dt = new DataTable();
            dt.Columns.Add("Project_ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Profile_Public");
            dt.Columns.Add("Investement_Goal");
            dt.Columns.Add("Project_Balance");
            if (projects_and_details.Count > 0)
            {
                foreach (DataRow i in projects_and_details)
                {
                    decimal curr_balance = 0.0m;
                    if (projects_with_finance_data.Exists(e => (int)e["Project_ID"] == (int)i["Project_ID"]))
                    {
                        curr_balance = (decimal)projects_with_finance_data.Find(e => (int)e["Project_ID"] == (int)i["Project_ID"])["Project_Balance"];
                    }

                    dt.Rows.Add(new object[] { i["Project_ID"], i["Name"], i["Profile_Public"], i["Investment_Goal"], curr_balance });
                }
                Projects = dt.Rows;
            }
            else
            {
                Projects = new DataTable().Rows;
            }

        }
        /// <summary>
        /// Exposed public method that will post a message to our Entrepreneur account.
        /// </summary>
        /// <param name="entreprenur_id"></param>
        /// <param name="messenger_id">The id that is the identity of the messanger</param>
        /// <param name="messenger_type">A number that is tied to a type of user account. 1 : user messages, 2 : Financial messages, 3 : Investor messages, 4 : Entrepreneur messages, 5 : project messages</param>
        /// <param name="message">the message to post</param>
        public void message_Entrepreneur(int entreprenur_id, int messenger_id, int messenger_type, string message)
        {
            string query = "sp_message_ProfileAccount";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Reciving_ID", entreprenur_id);
            command.Parameters.AddWithValue("@Reciving_Type", 4);
            command.Parameters.AddWithValue("@Messenger_ID", messenger_id);
            command.Parameters.AddWithValue("@Reference_Type", messenger_type);
            command.Parameters.AddWithValue("@Message", message);
            if(entreprenur_id < 0)
            {
                throw new Exception("Input parameter for entrepreneur is not valid; Cannot be 0 or less");
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
            return;
        }
    }

    /// <summary>
    /// This class wil be used to accept and validate data passsed in too update an entrepreneur account.
    /// </summary>
    public class EntrepreneurEditViewModel : EntrepreneurInitialize
    {
        /// <summary>
        /// Accessor that contains the name of this profile.
        /// </summary>
        [Display(Name = "Entrepreneur Name")]
        public string Name { get; set; }
        /// <summary>
        /// Accessor that contains whether or not this profile is public or not.
        /// </summary>
        [Display(Name = "Profile Public")]
        public bool Profile_Public { get; set; }
        /// <summary>
        /// Accessor of the profile image for this account.
        /// </summary>
        [Display(Name = "Profile Image")]
        public byte[] Profile_Picture { get; set; }
        /// <summary>
        /// Accessor of Image Id of the profile picture for this account.
        /// </summary>
        public int Image_ID { get; set; }
        /// <summary>
        /// Class constructor used to gather entrepreneur profile data.
        /// </summary>
        /// <param name="Entrepreneur_id"></param>
        /// <param name="userIdentity_id"></param>
        public EntrepreneurEditViewModel(int entrepreneur_id, string userIdentity_id) : base(entrepreneur_id, userIdentity_id)
        {
            Entrepreneur_ID = entrepreneur_id;
            get_EntrepreneurData();
        }
        /// <summary>
        /// Class constructor that is used when this object is meant to accept incoming data to update the entrepreneur profile.
        /// </summary>
        public EntrepreneurEditViewModel() : base()
        {

        }
        /// <summary>
        /// Private method that actually gets the data for this account.
        /// </summary>
        private void get_EntrepreneurData()
        {
            string query = "sp_get_EntrepreneurDetails";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Entrepreneur_ID", Entrepreneur_ID);
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
            // we will get 2 tables from running this stored procedure query
            //      [0] : Entrepreneur details
            //      [1] : Entrepreneur images

            // now that we have our results and there should only ever be one.
            if (set.Tables[0].Rows.Count > 1)
                throw new Exception("Duplicate Entrepreneur Accounts!");
            else if (set.Tables[0].Rows.Count < 0)
                throw new Exception("Invalid Operation Exception");

            // set this classes accessors with the results from our Entrepreneur record in our first set.
            DataRow row = set.Tables[0].Rows[0];
            Entrepreneur_ID = (int)row["Entrepreneur_ID"];
            Name = row["Name"].ToString();
            Profile_Public = (bool)row["Profile_Public"];

            // set image bytes for our accessor if there is an image result in our second set.
            try
            {
                Profile_Picture = (byte[])set.Tables[1].Rows[0]["Binary_Image"];
            }
            catch (IndexOutOfRangeException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                Profile_Picture = null;
            }
        }
        /// <summary>
        /// Class method called when updating entrepreneur account.
        /// </summary>
        public void updateEntrepreneur()
        {
            if(!valid)
            {
                return; // Do nothing.
            }
            string query = "sp_update_EntrepreneurAccount";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Entrepreneur_ID", Entrepreneur_ID);
            command.Parameters.AddWithValue("@Profile_Public", Profile_Public);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@Image_ID", Image_ID);
            command.Parameters.Add("@Binary_Image", SqlDbType.VarBinary);
            if(Profile_Picture != null)
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

    /// <summary>
    /// This class will be used to display and accept client inpt when making new project accounts under a logged in Entrepreneur account
    /// </summary>
    public class EntrepreneurCreateProjectViewModel : EntrepreneurInitialize
    {
        /// <summary>
        /// Public accessor that will hold the name of this new Project to be created.
        /// </summary>
        [Display(Name = "Project Name")]
        public string Name { get; set; }
        /// <summary>
        /// Public accessor that contains a discription of what this project account is about.
        /// </summary>
        [Display(Name = "A synopsis of your Project")]
        public string Description { get; set; }
        /// <summary>
        /// Public accessor to get the amoint of money that is required for this project.
        /// </summary>
        [Display(Name = "Investment goal for this project")]
        public decimal Investment_Goal { get; set; }
        /// <summary>
        /// Class constructor used to instantiate this object, it must be instantiated with these parameters to check that the client user and Entrepreneur are related before submiting any action to the data base. 
        /// </summary>
        /// <param name="entrepreneurID"></param>
        /// <param name="userIdentity_Id"></param>
        public EntrepreneurCreateProjectViewModel(int entrepreneurID, string userIdentity_Id) : base(entrepreneurID, userIdentity_Id)
        {
            
        }
        /// <summary>
        /// Class constructor used only when accepting input that will be used from the client to make a new project account.
        /// </summary>
        public EntrepreneurCreateProjectViewModel() : base()
        {

        }
        /// <summary>
        /// Public method that creates the new Project account and returns an integer that is the Id of that newly created Project
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="investmentGoal"></param>
        /// <returns></returns>
        public int createProject()
        {
            // check to make sure that client identity is indeed related to this account.
            if(!valid)
            {
                return 0; // if it is not do nothing, return 0.
            }
            string query = "Project_Create";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Entrepreneur_ID", Entrepreneur_ID);
            command.Parameters.AddWithValue("@Project_Name", Name);
            command.Parameters.AddWithValue("@Project_Description", Description);
            command.Parameters.AddWithValue("@Investment_Goal", Investment_Goal);
            int Project_Id = 0;
            try
            {
                conn.Open();
                Project_Id = (int)command.ExecuteScalar();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return Project_Id;
        }
    }
}