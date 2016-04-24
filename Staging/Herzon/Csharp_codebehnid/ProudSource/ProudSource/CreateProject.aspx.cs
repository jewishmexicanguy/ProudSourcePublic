using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections.Specialized;
using Npgsql;

namespace ProudSource
{
    public partial class CreateProject : System.Web.UI.Page
    {
        public string UserName;

        private int UserID, EntrepreneurID, ProjectID;
        // PostGreSQLconnection object
        private NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["ProudSourceDB"].ConnectionString);

        /// <summary>
        /// This method is called whenever this page is loaded and for our purposes this method takes care of 2 things.
        /// It makes sure that the client is logged in as a user.
        /// It makes sure that the client is logged in as an entrepreneur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // This a page where the user using it must be identifed and logged in so that we know what master user ID is associated with the project account that will be created
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
            }

            // This page also requires that the user is logged in as an entrepreneur account, check to make sure that they are if not take them back to the user dashboard.
            if (Session["EntrepreneurAccountID"] != null)
            {
                EntrepreneurID = (int)Session["EntrepreneurAccountID"];
            }
            else
            {
                if (!Page.IsCallback)
                {
                    Server.Transfer("UserDashboard.aspx");
                }
                else 
                {
                    Response.Redirect("UserDashboard.aspx");
                }
            }
        }

        /// <summary>
        /// This method get called when the submit button initates a postback to our server with data to insert a new record into our projects table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            // check that value in box is not empty, if so return.
            if (Tbox_ProjectDescription.Text.Length < 1 | Tbox_ProjectDescription.Text.Length > 255)
            {
                return;
            }

            // check that value is not larger than 255, if so return.
            if (Tbox_ProjectDescription.Text.Contains(";"))
            {
                return;
            }

            // make our Ordered dictionary
            OrderedDictionary OD = new OrderedDictionary();
            OD.Add("project_description", Tbox_ProjectDescription.Text);

            // make sure that the text entered into project goals is a number
            try
            {
                OD.Add("investment_goal", Convert.ToInt32(Tbox_InvestmentGoal.Text));
            }
            catch (Exception error)
            {
                return;
            }

            // call method that will insert our new project into our table in our database
            create_project(UserID, EntrepreneurID, OD);

            // now take the client to the project account just made
            Response.Redirect("ViewProject/" + ProjectID.ToString());
        }

        /// <summary>
        /// This method when called will attempt to create a new record on our table and retrive the ID of the project account that has just been created and updates the ProjectID resident of this page object with that ID.
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="EntrepreneurID"></param>
        /// <param name="Project_Dict"></param>
        private void create_project(int UserID, int EntrepreneurID, OrderedDictionary Project_Dict)
        {
            string query = "INSERT INTO project_master (project_description, create_user_master_id, entrepreneur_master_id, investment_goal, project_public, mod_user_master_id, create_date_time) VALUES (@project_description, @create_user_master_id, @entrepreneur_master_id, @investment_goal, TRUE, 2, now()); SELECT currval(pg_get_serial_sequence('project_master','project_master_id'));";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            da.SelectCommand.Parameters.AddWithValue("@entrepreneur_master_id", EntrepreneurID);
            da.SelectCommand.Parameters.AddWithValue("@create_user_master_id", UserID);
            da.SelectCommand.Parameters.AddWithValue("@project_description", Project_Dict["project_description"].ToString());
            da.SelectCommand.Parameters.AddWithValue("@investment_goal", (int)Project_Dict["investment_goal"]);
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

            if (ds.Tables[0].Rows.Count > 0)
            {
                ProjectID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                throw new Exception("Failure to create project account.");
            }
        }
    }
}