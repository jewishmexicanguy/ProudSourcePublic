using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Npgsql;

namespace ProudSource
{
    public partial class ViewEntrepreneur : System.Web.UI.Page
    {
        public string EntrepreneurName, ProjectsResults;

        private int EntrepreneurID;

        /// <summary>
        /// Define a connection object that is private to this object.
        /// </summary>
        private NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["ProudSourceDB"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet EntrepreneurData = (DataSet)HttpContext.Current.Items["Entrepreneur"];

            // load EntrepreneurID from data that is saved in context to this client event.
            try
            {
                EntrepreneurID = (int)EntrepreneurData.Tables[0].Rows[0]["entrepreneur_master_id"];
            }
            catch (Exception k)  // if this fires then the client tried going to the ViewEntrepreneur.aspx without an ID at the end of the URL request or a dataset was not loaded into context.
            {
                throw new HttpException(404, "Not found");
            }

            // load Entrepreneur Name
            EntrepreneurName = EntrepreneurData.Tables[0].Rows[0]["entrepreneur_profile_name"].ToString();
            lbl_EntrepreneurName.Text = EntrepreneurName;

            // load Entrepreneur img
            if (EntrepreneurData.Tables[0].Rows[0]["image_file"].ToString().Length > 0) // check to make sure that the field is not null before we try any fancy byte[] to image business.
            {
                // This totaly works and is awesome
                img_Entrepreneur.ImageUrl = string.Format(
                    "data:image/jpg;base64,{0}",
                    Convert.ToBase64String(
                        (byte[])EntrepreneurData.Tables[0].Rows[0]["image_file"]
                    )
                );
            }
            else // we need to make the picture to show be our default picture for empty profiles
            {
                //img_Entrepreneur.ImageUrl = "../ProudSource/images/profile_empty_m.jpg";
            }

            // load Projects related to this Entrepreneur
            load_Projects();
        }

        private void load_Projects()
        {
            // make query to get a dataset from our Data Base
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
                    ProjectsResults = "<h3>No Projects were found for this account... how lame of them</h3>";
                }
                else // start building what we will render onto the page by placing an open table bracket and table head to be shown
                {
                    ProjectsResults += "<h3>Projects</h3><br />";
                    ProjectsResults += "<table id='tableProjectAccounts' class='table table-hover table-striped table-condensed'>";
                    ProjectsResults += "<thead><tr><th>Name</th><th>Date Created</th><th>Investment goal</th></tr></thead>";
                    ProjectsResults += "<tbody>";
                    foreach (DataRow i in ds.Tables[0].Rows)
                    { // loop over results set of our query for each row add a row to our html table
                        ProjectsResults += "<tr>";
                        ProjectsResults += "<td><a href='" + "/ProudSource/ViewProject/" + i["project_master_id"].ToString() + "'>" + i["project_description"].ToString() + "</a></td>";
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
                ProjectsResults = "<h3>Something went wrong trying to retrive data about this profile!</h3>";
            }
        }
    }
}