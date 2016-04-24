using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace ProudSourceBeta.Models
{
    /// <summary>
    /// Abstract Class that provides an overridable method intended to allow derived types to be aware if the client is the entrepreneur or the investor related to a particular PROC.
    /// </summary>
    public abstract class PROCInitializeViewModel : PSDataConnection
    {
        /// <summary>
        /// Accessor for entrepreneur id.
        /// </summary>
        public int Entrepreneur_ID { get; set; }
        /// <summary>
        /// Accessor for investor id.
        /// </summary>
        public int Investor_ID { get; set; }
        /// <summary>
        /// Accessor for project id.
        /// </summary>
        public int Project_ID { get; set; }
        /// <summary>
        /// Accessor for PROC id.
        /// </summary>
        public int PROC_ID { get; set; }
        /// <summary>
        /// Accessor that holds a boolean storing whether or not this the investor owner's session or not.
        /// </summary>
        public bool isInvestorSession { get; protected set; }
        /// <summary>
        /// Accessor that holds a boolean storing whether or not this is the entrepreneur owner's session or not.
        /// </summary>
        public bool isEntrepreneurSession { get; protected set; }
        /// <summary>
        /// Accessor that holds a boolean specifying whether or not this type has been validated.
        /// </summary>
        public bool Valid { get; protected set; }
        /// <summary>
        /// Class constructor used when any kind of user auth will be used by derived objects of this type.
        /// </summary>
        /// <param name="userIdentity_id"></param>
        public PROCInitializeViewModel(string userIdentity_id) : base(userIdentity_id)
        {
            // start by setting these booleans as false, in this way they are false by default and are inherited that way.
            isEntrepreneurSession = false;
            isInvestorSession = false;
            Valid = false;
        }
        /// <summary>
        /// Constructor used when derived types will only catch incoming data from client input.
        /// </summary>
        public PROCInitializeViewModel() : base()
        {
            isEntrepreneurSession = false;
            isInvestorSession = false;
            Valid = false;
        }
        /// <summary>
        /// This method is meant to be overriden by derived types of this class.
        /// </summary>
        /// <param name="entrepreneur_id">Entrepreneur_ID retrived from the client's session variable.</param>
        /// <param name="investor_id">Investor_ID retrived from the client's session variable.</param>
        /// <param name="project_id">Project_ID retrived from the client's session variable.</param>
        /// <param name="proc_id">PROC_ID retrived from the end of the http request made by th client's browser.</param>
        protected abstract void elucidate_Session();
    }
    /// <summary>
    /// Class that will provide the facility to display PROC details to be viewed by the entrepreneur owner and provides the facility to update PROC parameters.
    /// </summary>
    public class PROCEntrepreneurViewModel : PROCInitializeViewModel
    {
        public DateTime Performance_BeginDate { get; set; }

        public DateTime Performance_EndDate { get; set; }

        public decimal Investment_ammount { get; set; }

        public decimal Revenue_Percentage { get; set; }

        public string Project_Name { get; private set; }

        public bool Project_Accepted { get; set; }

        public bool Investor_Accepted { get; set; }

        public bool Mutually_Accepted { get; set; }

        public DateTime Date_Mutually_Accepted { get; set; }

        public bool Active { get; set; }

        public DateTime Date_Activated { get; set; }

        public byte[] Investor_Image { get; private set; }

        public byte[] Entrepreneur_Image { get; private set; }

        public string Investor_Name { get; private set; }

        public string Entrepreneur_Name { get; private set; }

        public int proc_investor_id { get; private set; }

        // we already have the entrepreneur ID.

        /// <summary>
        /// Constructor used to authenticate an entrepreneur client that is viewing a PROC record, after instantiation this class will be aware if this the entrepreneur and project owner or not.
        /// </summary>
        /// <param name="userIdentity_id">.Net Identity id</param>
        /// <param name="entrepreneur_id">Entrepreneur_ID that will be retrived from a session object</param>
        /// <param name="project_id">Project_ID that will be retrived from a session object.</param>
        /// <param name="proc_id">PROC_ID of the PROC that tha client is trying to view.</param>
        public PROCEntrepreneurViewModel(string userIdentity_id, int entrepreneur_id, int project_id, int proc_id) : base(userIdentity_id)
        {
            // set our accessors to the values that have been given to us, User_ID has already been set and inherited.
            Entrepreneur_ID = entrepreneur_id;
            Project_ID = project_id;
            PROC_ID = proc_id;
            // now lets see if this client is the right combination of things.
            elucidate_Session();
        }
        /// <summary>
        /// Constructor used to capture incoming data to update the PROC in question via public accessor of this class.
        /// </summary>
        public PROCEntrepreneurViewModel() : base()
        {

        }
        /// <summary>
        /// Overredided method that authenticates that the current entrepreneur session is the entrepreneur owner of this proc.
        /// </summary>
        protected override void elucidate_Session()
        {
            //throw new NotImplementedException();
            // This query should only ever return the integer 1 if succesfull and 0 if not.
            string query = "SELECT COUNT(*) ";
            query += "FROM Procs PR ";
            query += "JOIN Projects P ON PR.[Project_ID] = P.[Project_ID] ";
            query += "JOIN Entrepreneurs E ON P.[Entrepreneur_ID] = E.[Entrepreneur_ID] ";
            query += "JOIN Users U ON E.[User_ID] = U.[User_ID] ";
            query += "WHERE PR.[Proc_ID] = @PROC_ID AND P.[Project_ID] = @Project_ID AND E.[Entrepreneur_ID] = @Entrepreneur_ID AND U.[User_ID] = @User_ID";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@User_ID", User_Id);
            command.Parameters.AddWithValue("@Entrepreneur_ID", Entrepreneur_ID);
            command.Parameters.AddWithValue("@Project_ID", Project_ID);
            command.Parameters.AddWithValue("@PROC_ID", PROC_ID);
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
            if (res == 1)
            {
                // we now know that the current client session is the entrepreneur owner of this proc.
                isEntrepreneurSession = true;
                Valid = true;
            }
        }
        /// <summary>
        /// Private class that gets the data for this PROC to diaply to the client.
        /// </summary>
        public void get_PROC_Data()
        {
            if (!Valid)
                return; // do nothing.
            string query = "sp_GetPROC_Details";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.Text;
            adapter.SelectCommand.Parameters.AddWithValue("@PROC_ID", PROC_ID);
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
            // now get all of the data returned from this set and place it into the accessors of this object.
            // there will be three tables
            //      [0] : Proc details
            //      [1] : Investor image
            //      [2] : Entrepreneur image

            DataRow PROC_row = set.Tables[0].Rows[0];
            if (set.Tables[1].Rows[0]["Binary_Image"] != null)
                Investor_Image = (byte[])set.Tables[1].Rows[0]["Binary_Image"];
            else
                Investor_Image = null;
            if (set.Tables[2].Rows[0]["Binary_Image"] != null)
                Entrepreneur_Image = (byte[])set.Tables[2].Rows[0]["Binary_Image"];
            else
                Entrepreneur_Image = null;
            Performance_BeginDate = (DateTime)PROC_row["Performance_BeginDate"];
            Performance_EndDate = (DateTime)PROC_row["Performance_EndDate"];
            Investment_ammount = (decimal)PROC_row["Investment_Ammount"];
            Revenue_Percentage = (decimal)PROC_row["Revenue_Percentage"];
            Project_Name = PROC_row["Project Name"].ToString();
            Investor_Name = PROC_row["Investor Name"].ToString();
            Entrepreneur_Name = PROC_row["Entrepreneur Name"].ToString();
            if (PROC_row["Project_Accepted"] != null)
                Project_Accepted = (bool)PROC_row["Project_Accepted"];
            else
                Project_Accepted = false;
            if (PROC_row["Investor_Accepted"] != null)
                Investor_Accepted = (bool)PROC_row["Investor_Accepted"];
            else
                Investor_Accepted = false;
            if (PROC_row["Mutually_Accepted"] != null)
                Mutually_Accepted = (bool)PROC_row["Mutually_Accepted"];
            else
                Mutually_Accepted = false;
            if (PROC_row["Date_Mutually_Accepted"] != null)
                Date_Mutually_Accepted = (DateTime)PROC_row["Date_Mutually_Accepted"];
            else
                Date_Mutually_Accepted = DateTime.MinValue;
            if (PROC_row["Active"] != null)
                Active = (bool)PROC_row["Active"];
            else
                Active = false;
            if (PROC_row["Date_Activated"] != null)
                Date_Activated = (DateTime)PROC_row["Date_Activated"];
            else
                Date_Activated = DateTime.MinValue;

            // the only difference in setting accessor values for this class that makes it diffirent from it's sister class.
            proc_investor_id = (int)PROC_row["Investor_ID"];
        }
        /// <summary>
        /// Method that updates this PROC with data incoming from the client.
        /// </summary>
        public void update_PROC()
        {
            if (!Valid)
                return; // do nothing
            string query = "sp_updatePROC_Entrepreneur";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PROC_ID", PROC_ID);
            command.Parameters.AddWithValue("@Performance_BeginDate", Performance_BeginDate);
            command.Parameters.AddWithValue("@Performance_EndDate", Performance_EndDate);
            command.Parameters.AddWithValue("@Project_Accepted", Project_Accepted);
            command.Parameters.AddWithValue("@Revenue_Percentage", Revenue_Percentage);
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
    /// Class that will provide the facility to display PROC details to be viewed by the investor owner and provides the facility to update PROC parameters.
    /// </summary>
    public class PROCInvestorViewModel : PROCInitializeViewModel
    {
        public DateTime Performance_BeginDate { get; set; }

        public DateTime Performance_EndDate { get; set; }

        public decimal Investment_ammount { get; set; }

        public decimal Revenue_Percentage { get; set; }

        public string Project_Name { get; private set; }

        public bool Project_Accepted { get; set; }

        public bool Investor_Accepted { get; set; }

        public bool Mutually_Accepted { get; set; }

        public DateTime Date_Mutually_Accepted { get; set; }

        public bool Active { get; set; }

        public DateTime Date_Activated { get; set; }

        public byte[] Investor_Image { get; private set; }

        public byte[] Entrepreneur_Image { get; private set; }

        public string Investor_Name { get; private set; }

        public string Entrepreneur_Name { get; private set; }

        public int proc_project_id { get; private set; }

        // we already know what the investor id is in this object
         
        /// <summary>
        /// Constructor used to authenticate an entrepreneur client that is viewing a PROC record, after instantiation this class will be aware if this the investor owner or not.
        /// </summary>
        /// <param name="userIdentity_id">.Net User identity id</param>
        /// <param name="investor_id">Investor id gathered from session</param>
        /// <param name="proc_id">PROC id gathered from the client browser's http request</param>
        public PROCInvestorViewModel(string userIdentity_id, int investor_id, int proc_id) : base(userIdentity_id)
        {
            // set our accessors to the values that have been given to us, User_ID has already been set and inherited.
            Investor_ID = investor_id ;
            PROC_ID = proc_id;
            // now lets see if this client is the right combination of things.
            elucidate_Session();
            // get the data for this proc.
            get_PROC_Data();
        }
        /// <summary>
        /// Constructor used when this object will capture data to update this proc via exposed public accessors.
        /// </summary>
        public PROCInvestorViewModel() : base()
        {

        }
        /// <summary>
        /// Protected method that will elucidate whether or not the client who is viewing this is the investor owner of the PROC in question.
        /// </summary>
        protected override void elucidate_Session()
        {
            //throw new NotImplementedException();
            // This query should only ever return the integer 1 if succesfull and 0 if not.
            string query = "SELECT COUNT(*) FROM Users U ";
            query += "JOIN Investors I ON U.[User_ID] = I.[User_ID] ";
            query += "JOIN Procs P ON I.[Investor_ID] = P.[Investor_ID] ";
            query += "WHERE P.[Proc_ID] = @PROC_ID AND I.[Investor_ID] = @Investor_ID AND U.[User_ID] = @User_ID";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@User_ID", User_Id);
            command.Parameters.AddWithValue("@Investor_ID", Investor_ID);
            command.Parameters.AddWithValue("@PROC_ID", PROC_ID);
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
            if (res == 1)
            {
                isInvestorSession = true;
                Valid = true;
            }
            return;
        }
        /// <summary>
        /// Private class that gets the data for this PROC to diaply to the client.
        /// </summary>
        private void get_PROC_Data()
        {
            if (!Valid)
                return; // do nothing.
            string query = "sp_GetPROC_Details";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@PROC_ID", PROC_ID);
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
            // now get all of the data returned from this set and place it into the accessors of this object.
            // there will be three tables
            //      [] : Proc details
            //      [] : Investor image
            //      [] : Entrepreneur image

            DataRow PROC_row = set.Tables[0].Rows[0];
            if (set.Tables[1].Rows.Count > 0)
                Investor_Image = (byte[])set.Tables[1].Rows[0]["Binary_Image"];
            else
                Investor_Image = null;
            if (set.Tables[2].Rows.Count > 0)
                Entrepreneur_Image = (byte[])set.Tables[2].Rows[0]["Binary_Image"];
            else
                Entrepreneur_Image = null;
            Performance_BeginDate = (DateTime)PROC_row["Performance_BeginDate"];
            Performance_EndDate = (DateTime)PROC_row["Performance_EndDate"];
            Investment_ammount = (decimal)PROC_row["Investment_Ammount"];
            Revenue_Percentage = (decimal)PROC_row["Revenue_Percentage"];
            Project_Name = PROC_row["Project Name"].ToString();
            Investor_Name = PROC_row["Investor Name"].ToString();
            Entrepreneur_Name = PROC_row["Entrepreneur Name"].ToString();
            if (PROC_row["Project_Accepted"] != null)
                Project_Accepted = (bool)PROC_row["Project_Accepted"];
            else
                Project_Accepted = false;
            if (PROC_row["Investor_Accepted"] != null)
                Investor_Accepted = (bool)PROC_row["Investor_Accepted"];
            else
                Investor_Accepted = false;
            if (PROC_row["Mutually_Accepted"] != null)
                Mutually_Accepted = (bool)PROC_row["Mutually_Accepted"];
            else
                Mutually_Accepted = false;
            if (PROC_row["Date_Mutually_Accepted"].ToString() != "")
                Date_Mutually_Accepted = (DateTime)PROC_row["Date_Mutually_Accepted"];
            else
                Date_Mutually_Accepted = DateTime.MinValue;
            if (PROC_row["Active"] != null)
                Active = (bool)PROC_row["Active"];
            else
                Active = false;
            if (PROC_row["Date_Activated"].ToString() != "")
                Date_Activated = (DateTime)PROC_row["Date_Activated"];
            else
                Date_Activated = DateTime.MinValue;

            // the only difference in setting accessors for this class from our query, what makes it different from it's brother class.
            proc_project_id = (int)PROC_row["Project_ID"];
            Entrepreneur_ID = (int)PROC_row["Entrepreneur_ID"];
        }
        /// <summary>
        /// Method that allows the investor to update this PROC with 
        /// </summary>
        public void update_PROC()
        {
            if (!Valid)
                return; // do nothing
            string query = "sp_updatePROC_Investor";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PROC_ID", PROC_ID);
            command.Parameters.AddWithValue("@Performance_BeginDate", Performance_BeginDate);
            command.Parameters.AddWithValue("@Performance_EndDate", Performance_EndDate);
            command.Parameters.AddWithValue("@Investor_Accepted", Investor_Accepted);
            command.Parameters.AddWithValue("@Revenue_Percentage", Revenue_Percentage);
            command.Parameters.AddWithValue("@Investment_Ammount", Investment_ammount);
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
    /// Class that will provide facility to display PROC details to clients who are not owners of the PROC, neither the investor or entrepreneur.
    /// </summary>
    public class PROCFloaterViewModel : PROCInitializeViewModel
    {
        public DateTime Performance_BeginDate { get; set; }

        public DateTime Performance_EndDate { get; set; }

        public decimal Investment_ammount { get; set; }

        public decimal Revenue_Percentage { get; set; }

        public string Project_Name { get; private set; }

        public bool Mutually_Accepted { get; set; }

        public DateTime Date_Mutually_Accepted { get; set; }

        public byte[] Investor_Image { get; private set; }

        public byte[] Entrepreneur_Image { get; private set; }

        public string Investor_Name { get; private set; }
        
        public string Entrepreneur_Name { get; private set; }

        public PROCFloaterViewModel(int proc_id) : base()
        {
            PROC_ID = proc_id;
            get_PROC_Data();
        }

        public void get_PROC_Data()
        {
            string query = "sp_GetPROC_Details";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@PROC_ID", PROC_ID);
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
            // now fill the accessors of this object with the data we have just gathered.
            DataRow PROC_row = set.Tables[0].Rows[0];
            if (set.Tables[1].Rows[0]["Binary_Image"] != null)
                Investor_Image = (byte[])set.Tables[1].Rows[0]["Binary_Image"];
            else
                Investor_Image = null;
            if (set.Tables[2].Rows[0]["Binary_Image"] != null)
                Entrepreneur_Image = (byte[])set.Tables[2].Rows[0]["Binary_Image"];
            else
                Entrepreneur_Image = null;
            Performance_BeginDate = (DateTime)PROC_row["Performance_BeginDate"];
            Performance_EndDate = (DateTime)PROC_row["Performance_EndDate"];
            Investment_ammount = (decimal)PROC_row["Investment_Ammount"];
            Revenue_Percentage = (decimal)PROC_row["Revenue_Percentage"];
            Project_Name = PROC_row["Project Name"].ToString();
            Investor_Name = PROC_row["Investor Name"].ToString();
            Entrepreneur_Name = PROC_row["Entrepreneur Name"].ToString();
            if (PROC_row["Mutually_Accepted"] != null)
                Mutually_Accepted = (bool)PROC_row["Mutually_Accepted"];
            else
                Mutually_Accepted = false;
            if (PROC_row["Date_Mutually_Accepted"] != DBNull.Value)
                Date_Mutually_Accepted = (DateTime)PROC_row["Date_Mutually_Accepted"];
            else
                Date_Mutually_Accepted = DateTime.MinValue;
            Entrepreneur_ID = (int)PROC_row["Entrepreneur_ID"];
            Project_ID = (int)PROC_row["Project_ID"];
            Investor_ID = (int)PROC_row["Investor_ID"];
        }

        protected override void elucidate_Session()
        {
            throw new NotImplementedException();
        }
    }   
}