using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace ProudSourceBeta.Models
{
    /// <summary>
    /// Class that initiates and validates whether or not the Project is related to the Entrepreneur in question.
    /// </summary>
    public class ProjectInitializeViewModel : PSDataConnection
    {
        /// <summary>
        /// Accessor that is the current entrepreneur id in the current session.
        /// </summary>
        public int Entrepreneur_ID { get; private set; }
        /// <summary>
        /// Accessor that is the current project id in the current session.
        /// </summary>
        public int Project_ID { get; private set; }
        /// <summary>
        /// Public accessor that is the name of this project.
        /// </summary>
        public bool Valid { get; private set; }
        /// <summary>
        /// Class constructor when ids need to be validated for consuming or using data.
        /// </summary>
        /// <param name="userIdentity_id"></param>
        /// <param name="entrepreneur_id"></param>
        /// <param name="project_id"></param>
        public ProjectInitializeViewModel(string userIdentity_id, int entrepreneur_id, int project_id) : base(userIdentity_id)
        {
            Entrepreneur_ID = entrepreneur_id;
            Project_ID = project_id;
            _auth_Ids();
        }
        /// <summary>
        /// Empty class constructor to be used only when this object needs to be inherited empty for later types to accept values into thier public accessors.
        /// </summary>
        public ProjectInitializeViewModel() : base()
        {

        }
        /// <summary>
        /// Private method of this class that will determine if our client's session Ids match up and will permit them to view this particular project account.
        /// </summary>
        private void _auth_Ids()
        {
            string query = "SELECT COUNT(*) FROM Users U JOIN Entrepreneurs E ON U.[User_ID] = E.[User_ID] JOIN Projects P ON E.[Entrepreneur_ID] = P.[Entrepreneur_ID] WHERE U.[User_ID] = @User_ID AND E.[Entrepreneur_ID] = @Entrepreneur_ID AND P.[Project_ID] = @Project_ID";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@User_ID", User_Id);
            command.Parameters.AddWithValue("@Entrepreneur_ID", Entrepreneur_ID);
            command.Parameters.AddWithValue("@Project_ID", Project_ID);
            int res = 0;
            try
            {
                conn.Open();
                res = (int)command.ExecuteScalar();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            // res should only ever be one
            if (res == 1)
            {
                Valid = true;
            }
            else
            {
                Valid = false;
            }
        }
    }

    /// <summary>
    /// Class that gets the details for this project account to display to the client.
    /// </summary>
    public class ProjectIndexViewModel : ProjectInitializeViewModel
    {
        /// <summary>
        /// Public accessor that is the name of this project.
        /// </summary>
        [Display(Name = "Project name")]
        public string Name { get; set; }
        /// <summary>
        /// Accessor exposing what will hold the description of this project.
        /// </summary>
        [Display(Name = "A description of this project")]
        public string Description { get; set; }
        /// <summary>
        /// Public accessor that stores the investment goal for this project.
        /// </summary>
        [Display(Name = "Project investment goal")]
        public decimal Investment_Goal { get; set; }
        /// <summary>
        /// This public accessor exposes whether or not this account is publicallly viewable.
        /// </summary>
        [Display(Name = "Project is public")]
        public bool Profile_Public { get; set; }
        /// <summary>
        /// Public accessor that is a collection of the image ids that are used for this profile.
        /// </summary>
        public ICollection<int> Profile_Images_IDs { get; set; }
        /// <summary>
        /// Accessor exposing an object that will be able to hold multiple image files for this account.
        /// </summary>
        public ICollection<byte[]> Profile_Images { get; set; }
        /// <summary>
        /// Accessor exposing a collection of document IDs related to this account.
        /// </summary>
        public ICollection<int> Document_IDs { get; set; }
        /// <summary>
        /// Accessor expsing a collection of document File Names related to this account.
        /// </summary>
        public ICollection<string> Document_FileNames { get; set; }
        /// <summary>
        /// An accessor that exposes ability for this class to hold records that represent PROCs related to this project.
        /// </summary>
        public DataRowCollection PROC_Agrements { get; set; }
        /// <summary>
        /// This accessor holds the image of the entrepreneur that owns this project.
        /// </summary>
        public byte[] Entrepreneur_Image { get; set; }
        /// <summary>
        /// Expossed accessor for noting if the client viewing the profile is a logged in user or not.
        /// </summary>
        public bool IsRegisteredViewer { get; set; }
        /// <summary>
        /// Expossed accessor that stores a project ID that is being viewd by a non owner of the project account.
        /// </summary>
        public int display_Project_ID { get; set; }
        /// <summary>
        /// Accessor that holds the account balance for this project in USD
        /// </summary>
        public decimal Project_Account_Balnce_USD { get; private set; }
        /// <summary>
        /// Accessor that holds the account balance for this project in BTC
        /// </summary>
        public decimal Project_Account_Balance_BTC { get; private set; }
        /// <summary>
        /// Accessor that holds the Identity of this projects financial account.
        /// </summary>
        public int Financial_Account_ID { get; private set; }

        /// <summary>
        /// Class constructor called when data needs to be loaded into this object to display details about this project account.
        /// </summary>
        /// <param name="userIdentity_id">The user identity id loaded in by the Owin Identity system in .Net</param>
        /// <param name="entrepreneur_id">The entrepreneur id loaded in from session.</param>
        /// <param name="project_id">The project id loaded in from the URL requested by the client.</param>
        public ProjectIndexViewModel(string userIdentity_id, int entrepreneur_id, int project_id) : base(userIdentity_id, entrepreneur_id, project_id)
        {
            get_ProjectData(Project_ID);
        }
        /// <summary>
        /// Constructor used when this object only needs to be instantiated to get project details.
        /// </summary>
        /// <param name="project_id"></param>
        public ProjectIndexViewModel(int project_id) : base()
        {
            
        }
        /// <summary>
        /// Method that retrives and fills the accessors of this object.
        /// </summary>
        public void get_ProjectData(int project_id)
        {
            string query = "sp_get_ProjectDetails";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Project_ID", project_id);
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
            // There will be three tables returned
            //      [0] : Project data
            //      [1] : Table of project images with ids
            //      [2] : Table of document names with ids
            //      [3] : Table with PROC rows tied to this project
            //      [4] : Table with a singular value that is the USD Balance
            //      [5] : Table with a singular value that is the BTC Balance
            //      [6] : Table with a singular value which is the financial account of this thing 
            //      [7] : Table with possibly a singular image of the entrepreneur owner

            Description = set.Tables[0].Rows[0]["Description"].ToString();
            Name = set.Tables[0].Rows[0]["Name"].ToString();
            Investment_Goal = (decimal)set.Tables[0].Rows[0]["Investment_Goal"];
            Profile_Public = (bool)set.Tables[0].Rows[0]["Profile_Public"];
            Document_IDs = new System.Collections.ObjectModel.Collection<int>();
            Document_FileNames = new System.Collections.ObjectModel.Collection<string>();
            Profile_Images_IDs = new System.Collections.ObjectModel.Collection<int>();
            Profile_Images = new System.Collections.ObjectModel.Collection<byte[]>();
            if (set.Tables[1].Rows.Count > 0)
            {
                foreach(DataRow i in set.Tables[1].Rows)
                {
                    Profile_Images_IDs.Add((int)i["Image_ID"]);
                    Profile_Images.Add((byte[])i["Binary_Image"]);
                }
            }
            if (set.Tables[2].Rows.Count > 0)
            {
                foreach(DataRow i in set.Tables[2].Rows)
                {
                    Document_IDs.Add((int)i["Document_ID"]);
                    Document_FileNames.Add(i["File_Name"].ToString());
                }
            }
            PROC_Agrements = set.Tables[3].Rows;
            if(!DBNull.Value.Equals(set.Tables[4].Rows[0]["USD_Balance"]))
            {
                Project_Account_Balnce_USD = (decimal)set.Tables[4].Rows[0]["USD_Balance"];
            }
            else
            {
                Project_Account_Balnce_USD = 0.0m;
            }
            if(!DBNull.Value.Equals(set.Tables[5].Rows[0]["BTC_Balance"]))
            {
                Project_Account_Balance_BTC = (decimal)set.Tables[5].Rows[0]["BTC_Balance"];
            }
            else
            {
                Project_Account_Balance_BTC = 0.0m;
            }
            Financial_Account_ID = (int)set.Tables[6].Rows[0]["Account_ID"];
            if(set.Tables[7].Rows.Count > 0)
            {
                Entrepreneur_Image = (byte[])set.Tables[7].Rows[0]["Binary_Image"];
            }
            else
            {
                Entrepreneur_Image = null;
            }
            return;
        }
        /// <summary>
        /// This method exposes the facility to send a message to the project account that is being viwed by the client.
        /// </summary>
        /// <param name="project_id">The ID of the project that is being viewed by the client.</param>
        /// <param name="id">The ID of the client viewing the project, if they are logged in.</param>
        /// <param name="reference_type_id">The kind of ID that is the client's ID. 3 is for investors, 4 is for entrepreneurs, 1 is for users.</param>
        /// <param name="message">The message that is being passed in.</param>
        public void message_ProjectOwner(int project_id, int id, int reference_type_id, string message)
        {
            string query = "sp_message_ProfileAccount";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Reciving_ID", project_id);
            // Use the number 5 for project accounts because this message is going to a projece account.
            command.Parameters.AddWithValue("@Reciving_Type", 5);
            command.Parameters.AddWithValue("@Messenger_ID", id);
            command.Parameters.AddWithValue("@Reference_Type", reference_type_id);
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
        }
    }

    /// <summary>
    /// Class that gets data to show the client what they will change and exposses a method to change the account details.
    /// </summary>
    public class ProjectEditViewModel : ProjectInitializeViewModel
    {
        /// <summary>
        /// Public accessor that contains and accepts an incoming string to update this project.
        /// </summary>
        [Display(Name = "Project Name")]
        public string Name { get; set; }
        /// <summary>
        /// Public accessor that allows getting whether or not the account is public.
        /// </summary>
        [Display(Name = "Project is public")]
        public bool Profile_Public { get; set; }
        /// <summary>
        /// Public accessor that houses and accepts this project's investment goal.
        /// </summary>
        [Display(Name = "Project investment goal")]
        public decimal Investment_Goal { get; set; }
        /// <summary>
        /// Public accessor that contains and accepts a string that will update the project's description.
        /// </summary>
        [Display(Name = "Project Description")]
        public string Description { get; set; }
        /// <summary>
        /// Constructor used to accept incoming values to be used to update the project account.
        /// </summary>
        public ProjectEditViewModel() : base()
        {

        }
        /// <summary>
        /// Class constructor used when collecting data or the update method will need to be called to update this project account.
        /// </summary>
        /// <param name="userIdentity_Id"></param>
        /// <param name="entrepreneur_id"></param>
        /// <param name="project_id"></param>
        public ProjectEditViewModel(string userIdentity_Id, int entrepreneur_id, int project_id) : base(userIdentity_Id, entrepreneur_id, project_id)
        {
            get_ProjectData();
        }
        /// <summary>
        /// Private method used to collect the data for this project account to deisplay on the edit page.
        /// </summary>
        private void get_ProjectData()
        {
            if(!Valid)
            {
                return; // do nothing.
            }
            string query = "SELECT [Project_ID], [Name], [Description], [Investment_Goal], [Profile_Public] ";
            query += "FROM Projects ";
            query += "WHERE [Project_ID] = @Project_ID";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.Text;
            adapter.SelectCommand.Parameters.AddWithValue("@Project_ID", Project_ID);
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
            if (set.Tables[0].Rows.Count == 1)
            {
                Description = set.Tables[0].Rows[0]["Description"].ToString();
                Name = set.Tables[0].Rows[0]["Name"].ToString();
                Profile_Public = (bool)set.Tables[0].Rows[0]["Profile_Public"];
                Investment_Goal = (decimal)set.Tables[0].Rows[0]["Investment_Goal"];
            }
            return;
        }
        /// <summary>
        /// Public methos used to update this project profile.
        /// </summary>
        public void update_Project()
        {
            if(!Valid)
            {
                return; // Do nothing.
            }
            string query = "sp_update_ProjectAccount";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Project_ID", Project_ID);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@Description", Description);
            command.Parameters.AddWithValue("@Profile_Public", Profile_Public);
            command.Parameters.AddWithValue("@Investment_Goal", Investment_Goal);
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
    /// Class that uploads documents and associates them to this project account.
    /// </summary>
    public class ProjectUploadDocumentViewModel : ProjectInitializeViewModel
    {
        /// <summary>
        /// Public accessor of the file to be uploaded.
        /// </summary>
        public string File_Name { get; set; }
        /// <summary>
        /// Public accessor of the mime type of the file uploaded.
        /// </summary>
        public string Mime_Type { get; set; }
        /// <summary>
        /// Public accessor of the file binary to be uploaded.
        /// </summary>
        [Display(Name = "The file to be uploaded")]
        public byte[] Binary_File { get; set; }
        /// <summary>
        /// Constructor used when catching values to be used to upload a document.
        /// </summary>
        public ProjectUploadDocumentViewModel() : base()
        {

        }
        /// <summary>
        /// Construcotr used that will actually do the uploading of the file, this is because this instance will be validated.
        /// </summary>
        /// <param name="userIdentity_id"></param>
        /// <param name="entrepreneur_id"></param>
        /// <param name="project_id"></param>
        public ProjectUploadDocumentViewModel(string userIdentity_id, int entrepreneur_id, int project_id) : base(userIdentity_id, entrepreneur_id, project_id)
        {

        }
        /// <summary>
        /// Method that actually uploads the document.
        /// </summary>
        public void upload_file()
        {
            if(!Valid)
            {
                return; // do nothing.
            }
            string query = "sp_uploadDocument_Project";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Project_ID", Project_ID);
            command.Parameters.AddWithValue("@File_Name", File_Name);
            command.Parameters.AddWithValue("@Mime_Type", Mime_Type);
            command.Parameters.AddWithValue("@Binary_File", Binary_File);
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
    /// Class that gets the name of a document realted to this project account and exposses a method to delete it.
    /// </summary>
    public class ProjectDeleteDocumentViewModel : ProjectInitializeViewModel
    {
        /// <summary>
        /// Public accessor of the file name of the document in question.
        /// </summary>
        [Display(Name = "The document to be removed")]
        public string File_Name { get; set; }
        /// <summary>
        /// PUblic accessor of the document id in question. Do not forget to place a value here.
        /// </summary>
        public int Document_ID { get; set; }
        /// <summary>
        /// Constructor used to get info of the project to delete and to delete this file.
        /// </summary>
        /// <param name="userIdentity_id"></param>
        /// <param name="entrepreneur_id"></param>
        /// <param name="project_id"></param>
        public ProjectDeleteDocumentViewModel(string userIdentity_id, int entrepreneur_id, int project_id) : base(userIdentity_id, entrepreneur_id, project_id)
        {

        }
        /// <summary>
        /// Public method that actually does the deletion of the document in question
        /// </summary>
        public void delete_document()
        {
            if(!Valid)
            {
                return; // Do nothing.
            }
            string query = "sp_DeleteDocument_Project";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Project_ID", Project_ID);
            command.Parameters.AddWithValue("@Document_ID", Document_ID);
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
        /// <summary>
        /// Public method used to get the 
        /// </summary>
        /// <param name="document_id"></param>
        public void get_DocumentName()
        {
            if(!Valid)
            {
                return; // do nothing
            }
            string query = "SELECT [File_Name] FROM Documents WHERE [Document_ID] = @Document_ID";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@Document_ID", Document_ID);
            try
            {
                conn.Open();
                File_Name = (string)command.ExecuteScalar();
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
    /// Class that allows the client to upload an image for this project account.
    /// </summary>
    public class ProjectUploadImageViewModel : ProjectInitializeViewModel
    {
        /// <summary>
        /// Public accessor to the image that will be inserted for thia account, do not forget to set before calling upload_Image.
        /// </summary>
        [Display(Name = "Image to upload")]
        public byte[] Profile_Image { get; set; }
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="userIdentity_id"></param>
        /// <param name="entrepreneur_id"></param>
        /// <param name="project_id"></param>
        public ProjectUploadImageViewModel(string userIdentity_id, int entrepreneur_id, int project_id) : base(userIdentity_id, entrepreneur_id, project_id)
        {

        }
        /// <summary>
        /// Exposed method that actually does the inserting of this image and relates it to this project account.
        /// </summary>
        public void upload_Image()
        {
            if(!Valid)
            {
                return; // Do nothing;
            }
            string query = "sp_Upload_Project_Image";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@Project_ID", Project_ID);
            command.Parameters.AddWithValue("@Binary_Image", Profile_Image);
            command.CommandType = CommandType.StoredProcedure;
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
    /// Class that gets the pciture that the client wasnts to delete and exposses a method to delete that image from this project account. 
    /// </summary>
    public class ProjectDeleteImageViewModel : ProjectInitializeViewModel
    {
        /// <summary>
        /// Public accessor to the binary image, to show to the client before they confirm deletion.
        /// </summary>
        [Display(Name = "Image to remove")]
        public byte[] Profile_Image { get; private set; }
        /// <summary>
        /// Public accessor to the image id fr the image to delete. 
        /// </summary>
        public int Image_ID { get; set; }
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="userIdentity_id"></param>
        /// <param name="entrepreneur_id"></param>
        /// <param name="project_id"></param>
        public ProjectDeleteImageViewModel(string userIdentity_id, int entrepreneur_id, int project_id) : base(userIdentity_id, entrepreneur_id, project_id)
        {

        }
        /// <summary>
        /// Exposed method that gets the image to show to the client so they can confirm deletion.
        /// </summary>
        public void get_Image()
        {
            if (!Valid)
                return; // do nothing
            string query = "SELECT [Image_ID], [Binary_Image] FROM Images WHERE [Image_ID] = @Image_ID";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.Text;
            adapter.SelectCommand.Parameters.AddWithValue("@Image_ID", Image_ID);
            DataSet set = new DataSet();
            try
            {
                conn.Close();
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
            try
            {
                Profile_Image = (byte[])set.Tables[0].Rows[0]["Binary_Image"];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return;
        }
        /// <summary>
        /// Exposed method that deletes the image in question.
        /// </summary>
        public void delete_Image()
        {
            if (!Valid)
                return; // do nothing

            string query = "sp_Delete_Project_Image";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Project_ID", Project_ID);
            command.Parameters.AddWithValue("@Image_ID", Image_ID);
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
    /// Class that creates a PROC between a Project account being viewed by an investor and exposses a method to create a PROC record between them.
    /// </summary>
    public class ProjectCreatePROCViewModel : PSDataConnection
    {
        /// <summary>
        /// Public accessor to the project id.
        /// </summary>
        public int Project_ID { get; set; }
        /// <summary>
        /// Public accessor to the investor id.
        /// </summary>
        public int Investor_ID { get; set; }
        /// <summary>
        /// Public accessor too the begin date for the new PROC.
        /// </summary>
        [Display(Name = "Terms Begin on")]
        public DateTime Performance_Begin_DateTime { get; set; }
        /// <summary>
        /// Public accessor too the end date for the new PROC.
        /// </summary>
        [Display(Name = "Terms End on")]
        public DateTime Performance_End_DateTime { get; set; }
        /// <summary>
        /// Public accessor too the revenue percentage for this PROC.
        /// </summary>
        [Display(Name = "Revenue %")]
        public decimal Revenue_Percentage { get; set; }
        /// <summary>
        /// Public accessor too the amount that the client wants to invest.
        /// </summary>
        [Display(Name = "Investment Amount")]
        public decimal Investment_Amount { get; set; }
        /// <summary>
        /// Class constructor
        /// </summary>

        /// <summary>
        /// This accessor will house the account balance that the investor has in our proudsource accounts.
        /// </summary>
        [Display(Name = "Money available")]
        public decimal Financial_Account_Balance { get; private set; }

        public ProjectCreatePROCViewModel() : base()
        {

        }

        public ProjectCreatePROCViewModel(int investor_id) : base()
        {
            _get_Investor_Account_Balance(investor_id);
        }

        private void _get_Investor_Account_Balance(int investor_id)
        {
            string query = @"USE [ProudSourceAccounting]
                             SELECT SUM(T.[Amount]) AS 'Balance'
                             FROM Transactions T
                             JOIN Accounts A ON T.[Account_ID] = A.[Account_ID]
                             WHERE A.[Profile_Account_ID] = @Investor_ID
                                AND A.[Profile_Type_ID] = 3
                                AND T.[Transaction_State] = 'PROCESSED'
                             ";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@Investor_ID", Investor_ID);
            DataSet set = new DataSet();
            try
            {
                conn.Open();
                adapter.Fill(set);
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
            if (!set.Tables[0].Rows[0]["Balance"].Equals(DBNull.Value))
            {
                Financial_Account_Balance = (decimal)set.Tables[0].Rows[0]["Balance"];
            }
        }
        /// <summary>
        /// Exposed method that creates the PROC between the Investor and this Project.
        /// </summary>
        /// <returns></returns>
        public int create_PROC()
        {
            string query = "Proc_Create";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Project_ID", Project_ID);
            command.Parameters.AddWithValue("@Investor_ID", Investor_ID);
            command.Parameters.AddWithValue("@Revenue_Percentage", Revenue_Percentage);
            command.Parameters.AddWithValue("@Investment_Ammount", Investment_Amount);
            command.Parameters.AddWithValue("@Performance_BeginDate", Performance_Begin_DateTime);
            command.Parameters.AddWithValue("@Performance_EndDate", Performance_End_DateTime);
            int PROC_ID = 0;
            try
            {
                conn.Open();
                PROC_ID = (int)command.ExecuteScalar();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return PROC_ID;
        }
    }
}