using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using Npgsql;

namespace ProudSource
{
    public partial class EntrepreneurAccount : Page
    {
        public string UserName, ProjectsResults;
        // Both of these Integers are loaded into memory the moment a page is served on Page_Load()
        private int UserID, EntrepreneurID;
        // PostGreSQLconnection object
        private NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["ProudSourceDB"].ConnectionString);
        // Private variables that hold datum for this account, they are filled when load_EntrepreneurData() is called
        private string AccountName;
        // private Image AccountImage;
        /// <TODO>
        /// Private PDF_document AccountPDF
        /// Figure out some way of storing and displaying a PDF onto the client side.
        /// </TODO>

        protected void Page_Load(object sender, EventArgs e)
        {
            // This a page where the user using it must be identifed and logged in so that we know what master user ID investor accounts created are associated with.
            // This if ... else statement will check to make sure that a session object exists with the information that we are looking for.
            if (Session["ProudSourceUser"] != null)
            {
                UserSessionAccessor Token = new UserSessionAccessor((object[])Session["ProudSourceUser"]);
                {
                    if (Token.UserAuthenticated)
                    {
                        UserName = Token.UserName;
                        UserID = Token.UserID;
                        Token.Dispose();

                        // greet our Entrepreneur
                        lbl_UserName.Text = UserName;
                    }
                    else
                    {
                        Response.Redirect("UserLogin.aspx");
                    }
                }

                // This page also requires that the user is logged in as an entrepreneur account, check to make sure that they are if not take them back to the user dashboard.
                if (Session["EntrepreneurAccountID"] != null)
                {
                    EntrepreneurID = (int)Session["EntrepreneurAccountID"];

                    // load projects that are owned by the Entrepreneur
                    load_Projects();

                    // load entrepreneur data to display on the page
                    load_EntrepreneurData();

                    // greet account user
                    lbl_Name.Text = AccountName;
                }
                else
                {
                    Response.Redirect("UserDashboard.aspx");
                }
            }
            else
            {
                if (!Page.IsCallback)
                {
                    Server.Transfer("UserLogin.aspx");
                }
                else
                {
                    Response.Redirect("UserLogin.aspx");
                }
            }
        }

        /// <summary>
        /// This method will load results for projects that are tied to this entrepreneur account from our DataBase.
        /// Projects are modeled in our data base off of the PROCs table.
        /// </summary>
        private void load_Projects()
        {
            /// <SQL> Statement that will be used in the future when projects have multiple images associated with them.
            /// SELECT *
            /// FROM project_master PM
            /// LEFT OUTER JOIN project_image_xref PIXREF 
            ///     ON PIXREF.project_master_id = PM.project_master_id
            /// LEFT OUTER JOIN image_master IM
            ///     ON IM.image_master_id = PIXREF.image_master_id
            /// WHERE PM.project_master_id = @ProjectID
            /// </SQL>
            string query = "SELECT project_master_id, project_description, create_date_time, investment_goal FROM project_master WHERE entrepreneur_master_id = @EntrepreneurID";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            da.SelectCommand.Parameters.AddWithValue("@EntrepreneurID", EntrepreneurID);
            DataSet ds = new DataSet();
            try
            {
                // open connection
                conn.Open();
                da.Fill(ds);
            }
            catch (Exception e)
            {
                /// <TODO>
                /// Handle something going wrong here
                /// </TODO>
            }
            finally
            {
                // always close conection
                conn.Close();
            }

            // if our query resturned no rows we should probably inform the user instead of making an empty table
            try
            {
                if (ds.Tables[0].Rows.Count < 0)
                {
                    ProjectsResults = "<h3>No Projects were found for your account... perhaps you should make one</h3>";
                }
                else // start building what we will render onto the page by placing an open table bracket and table head to be shown
                {
                    ProjectsResults += "<h3>Projects</h3><br />";
                    ProjectsResults += "<table id='tableProjectAccounts' class='table table-hover table-striped table-condensed'>";
                    ProjectsResults += "<thead><tr><th>Description</th><th>Date Created</th><th>Investment goal</th></tr></thead>";
                    ProjectsResults += "<tbody>";
                    foreach (DataRow i in ds.Tables[0].Rows)
                    { // loop over results set of our query for each row add a row to our html table
                        ProjectsResults += "<tr>";
                        ProjectsResults += "<td><a href='" + "ViewProject/" + i["project_master_id"].ToString() + "'>" + i["project_description"].ToString() +"</a></td>";
                        ProjectsResults += "<td>" + i["create_date_time"].ToString() + "</td>";
                        ProjectsResults += "<td>" + i["investment_goal"].ToString() + " </td>";
                        ProjectsResults += "</tr>";
                    }
                    // after loop, now add the closing table tags
                    ProjectsResults += "</tbody>";
                    ProjectsResults += "</table>";
                }
            }
            catch (Exception e)
            {
                ProjectsResults = "<h3>Something went wrong trying to retrive data about your profile!</h3>";
            }
        }

        /// <summary>
        /// This method will load the information for the Entrepreneur account that the user has selected by making a query to our database based off of the ID the we have for this investor.
        /// </summary>
        private void load_EntrepreneurData()
        {
            /// <SQL>
            /// SELECT EM.entrepreneur_profile_name, IM.image_file, EM.entrepreneur_public 
            /// FROM entrepreneur_master EM 
            /// LEFT OUTER JOIN entrepreneur_image_xref EXREF 
            ///     ON EXREF.entrepreneur_master_id = EM.entrepreneur_master_i 
            /// LEFT OUTER JOIN image_master IM 
            ///     ON IM.image_master_id = EXREF.image_master_id 
            /// WHERE EM.entrepreneur_master_id = @EntrepreneurID
            /// </SQL>
            string query = "SELECT EM.entrepreneur_profile_name, IM.image_file, EM.entrepreneur_public FROM entrepreneur_master EM LEFT OUTER JOIN entrepreneur_image_xref EXREF ON EXREF.entrepreneur_master_id = EM.entrepreneur_master_id LEFT OUTER JOIN image_master IM ON IM.image_master_id = EXREF.image_master_id WHERE EM.entrepreneur_master_id = @EntrepreneurID";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            da.SelectCommand.Parameters.AddWithValue("@EntrepreneurID", EntrepreneurID);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                da.Fill(ds);
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            // get profile name
            AccountName = (string)ds.Tables[0].Rows[0]["entrepreneur_profile_name"];

            // get profile picture
            {
                if (ds.Tables[0].Rows[0]["image_file"].ToString().Length > 0) // check to make sure that the field is not null before we try any fancy byte[] to image business.
                {
                    // This totaly works and is awesome
                    img_Entrepreneur.ImageUrl = string.Format(
                        "data:image/jpg;base64,{0}",
                        Convert.ToBase64String(
                            (byte[])ds.Tables[0].Rows[0]["image_file"]
                        )
                    );
                }
                else // we need to make the picture to show be our default picture for empty profiles
                {
                    img_Entrepreneur.ImageUrl = "../images/profile_empty_m.jpg";
                }
            }

            // get wether or not it is public
            if (ds.Tables[0].Rows[0]["entrepreneur_public"].ToString() == "True")
            {
                lbl_ProfilePublic.Text = "Yes";
            }
            else
            {
                lbl_ProfilePublic.Text = "No";
            }
        }

        /// <summary>
        /// This method is what gets called when the update button click event is triggered on the EntrepreneurAccount page from the client side.
        /// It gets the values from our aspx controls and allows us to then call a private method that does the acual updating.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Update_Click(object sender, EventArgs e)
        {
            // instantiate a dictionary object
            OrderedDictionary OD = new OrderedDictionary();
            OD.Add("entrepreneur_profile_name", Tbox_Name.Text);
            OD.Add("entrepreneur_image", FileUp_Image.FileBytes);
            OD.Add("entrepreneur_public", RButton_Public.Checked);
             
            // now that all our local variables are filled with whatever the client has chosen to update thier profile with call the update statement with these arguments
            update_Entrepreneur(EntrepreneurID, OD);

            // reload the page
            Response.Redirect(Request.RawUrl);
        }

        /// <summary>
        /// This method will call upon a stored procedure, which will check for null values and update where neccessary for this account.
        /// </summary>
        /// <StoredProcedure name="update_entrepreneur(INTEGER, CHARACTER VARYING, BOOLEAN, BYTEA)"></StoredProcedure>
        /// <note>
        /// It will replace the first iamge that is related to this account instead of adding new images.
        /// </note>
        /// <param name="ID"></param>
        /// <param name="new_name"></param>
        /// <param name="image_byte_array"></param>
        /// <param name="account_public"></param>
        private void update_Entrepreneur(int ID, OrderedDictionary Update_Dictionary)
        {
            // we are going to call a stored procedure that will handle all of the logic for us of when to insert what fields and to know whether they are empty or not.
            string query = "update_entrepreneur";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            da.SelectCommand.Parameters.AddWithValue("ATentrepreneur_id", EntrepreneurID);
            da.SelectCommand.Parameters.AddWithValue("ATentrepreneur_profile_name", Update_Dictionary["entrepreneur_profile_name"]);
            da.SelectCommand.Parameters.AddWithValue("ATentrepreneur_public", Update_Dictionary["entrepreneur_public"]);
            da.SelectCommand.Parameters.AddWithValue("ATentrepreneur_image", Update_Dictionary["entrepreneur_image"]);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                da.SelectCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
        }
    }
}