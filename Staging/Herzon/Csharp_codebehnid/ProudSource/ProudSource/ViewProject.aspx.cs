using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Specialized;
using System.Configuration;
using Npgsql;

namespace ProudSource
{
    public partial class ViewProject : System.Web.UI.Page
    {
        public bool UserLoggedIn, InvestorLoggedIn, EntrepreneurLoggedIn, EntrepreneurOwner;

        public string InvestorHTML;

        public string EntrepreneurOpenDiv, EntrepreneurCloseDiv;

        public string[] EntrepreneurHTML = new string[8];

        private int UserID, InvestorID, EntrepreneurID, ProjectID;

        private DataSet ProjectDS;

        private NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["ProudSourceDB"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            // check to see if a client is logged in, try to get thier ID if they are, if not take not of that.
            if (Session["ProudSourceUser"] != null)
            {
                UserSessionAccessor Token = new UserSessionAccessor((object[])Session["ProudSourceUser"]);
                {
                    if (Token.UserAuthenticated)
                    {
                        UserID = Token.UserID;
                        Token.Dispose();
                        UserLoggedIn = true;
                    }
                    else
                    {
                        UserID = -1;
                        UserLoggedIn = false;
                    }
                }
            }
            else
            {
                UserID = -1;
                UserLoggedIn = false;
            }

            // check to see if client is logged in as an Investor.
            if (Session["InvestorAccountID"] != null)
            {
                InvestorID = (int)Session["InvestorAccountID"];
                InvestorLoggedIn = true;
            }
            else
            {
                InvestorID = -1;
                InvestorLoggedIn = false;
            }

            // check to see if client is logged in as an Entrepreneur.
            if (Session["EntrepreneurAccountID"] != null)
            {
                EntrepreneurID = (int)Session["EntrepreneurAccountID"];
                EntrepreneurLoggedIn = true;
            }
            else
            {
                EntrepreneurID = -1;
                EntrepreneurLoggedIn = false;
            }

            // load ProjectID in from data stored in context data.
            ProjectDS = (DataSet)HttpContext.Current.Items["Project"];
            try
            {
                ProjectID = (int)ProjectDS.Tables[0].Rows[0]["project_master_id"];
            }
            catch (Exception k)
            {
                throw new HttpException(404, "Not found");
            }

            // check to see if client is Entrepreneur owner.
            if (EntrepreneurID == (int)ProjectDS.Tables[0].Rows[0]["entrepreneur_master_id"])
            {
                EntrepreneurOwner = true;
                Session.Add("ProjectID", ProjectID);
            }
            else
            {
                EntrepreneurOwner = false;
            }

            // Now handle what controls will be shown on the page and what will not
            load_investor_page_elements();
            load_entrepreneur_page_elements();

            // load contents of controls that display information about this project
            load_ProjectDetails();
        }

        /// <summary>
        /// This method will fill html string holders with appropriate html element strings.
        /// It will also add a session variable to the clients browser with that ProjectID of the project displayed for use when an investor clicks the create proc button.
        /// </summary>
        private void load_investor_page_elements()
        {
            if (InvestorLoggedIn != true)
            {
                // make strings that show up on page empty
                InvestorHTML = "<h4>If you are an investor and are insterested in registering a PROC with this Project pleas log in, select your Investor account and navigate back to this Project profile.</h4>";

                // make controls that load when an investor is logged in explicitly null
                lbl_InvestorCreatePROC.Visible = false;
                return;
            }

            if (InvestorLoggedIn == true)
            {
                // construct string that will render a button that takes the user to our PROC creation page.
                InvestorHTML = "<button id=\"Btn_add_PROC\" type=\"button\" onclick=\"window.location.href = '/ProudSource/CreatePROC.aspx';\"><span class=\"glyphicon glyphicon-plus\" aria-hidden=\"true\"></span></button>";

                // make controls that show up when an ijvestor is logged in explicitly true
                lbl_InvestorCreatePROC.Visible = true;

                // add Session variable for this project that will be used on the project creation page.
                Session.Add("ProjectID", ProjectID);
            }
        }

        private void load_entrepreneur_page_elements()
        {
            if (EntrepreneurOwner != true)
            {
                // make strings that show up on page empty
                EntrepreneurOpenDiv = "<div class=\"container\"><h4>If you are the Entrepreneur owner of this profile please log in and navigate to this profile or click on this project from your Entrepreneur profile account.</h4><div>";
                for (int i = 0; i < EntrepreneurHTML.Length; i++)
                {
                    EntrepreneurHTML[i] = string.Empty;
                }
                EntrepreneurCloseDiv = string.Empty;

                // deactivate controls that are for entrepreneur owners explicitly.
                lbl_OwnerDescription.Visible = false;
                lbl_OwnerInvestmentGoal.Visible = false;
                Tbox_ProjectDescription.Visible = false;
                Tbox_ProjectInvestmentGoal.Visible = false;
                Btn_ProjectUpdate.Visible = false;
            }

            if (EntrepreneurOwner == true)
            {
                // fill strings that are to be rendered onto the page with appropriate HTML
                EntrepreneurOpenDiv = "<div id=\"entrepreneur-contents-update\" class=\"container\">";
                for (int i = 0; i < EntrepreneurHTML.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        EntrepreneurHTML[i] = "<li class=\"list-group-item\">";
                    }
                    if (i % 2 == 1)
                    {
                        EntrepreneurHTML[i] = "</li>";
                    }
                }
                EntrepreneurCloseDiv = "</div>";

                // Activate controls that are for entrepreneur owners explicitly.
                lbl_OwnerDescription.Visible = true;
                lbl_OwnerInvestmentGoal.Visible = true;
                Tbox_ProjectDescription.Visible = true;
                Tbox_ProjectInvestmentGoal.Visible = true;
                Btn_ProjectUpdate.Visible = true;
            }
            
        }

        private void load_ProjectDetails()
        {
            lbl_ProjectDescription.Text = ProjectDS.Tables[0].Rows[0]["project_description"].ToString();
            lbl_InvestmentGoal.Text = ProjectDS.Tables[0].Rows[0]["investment_goal"].ToString();
        }

        protected void Btn_ProjectUpdate_Click(object sender, EventArgs e)
        {
            OrderedDictionary ProjectDict = new OrderedDictionary();
            ProjectDict.Add("project_description", Tbox_ProjectDescription.Text);
            ProjectDict.Add("investment_goal", Convert.ToInt32(Tbox_ProjectInvestmentGoal.Text));

            // call method to update this project account
            update_Project(ProjectID, ProjectDict);

            // reload page to reflect update changes
            Response.Redirect(Request.RawUrl);
        }

        private void update_Project(int ProjectID, OrderedDictionary Dict)
        {
            string query = "UPDATE project_master SET project_description = @project_description, investment_goal = @investment_goal WHERE project_master_id = @project_master_id";
            NpgsqlCommand update_command = new NpgsqlCommand(query, conn);
            update_command.Parameters.AddWithValue("@project_master_id", ProjectID);
            update_command.Parameters.AddWithValue("@project_description", Dict["project_description"].ToString());
            update_command.Parameters.AddWithValue("@investment_goal", (int)Dict["investment_goal"]);
            try
            {
                conn.Open();
                update_command.ExecuteNonQuery();
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