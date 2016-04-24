using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using Npgsql;
using Newtonsoft.Json;

namespace ProudSource
{
    public partial class UserDashboard : Page, ICallbackEventHandler
    {
        // Variable to store user name
        public string UserName;
        // Variable to store UserID
        private int UserID;
        // PostGreSQLconnection object
        private NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["ProudSourceDB"].ConnectionString);
        // Variables that will store the data retrived from our queries 
        private DataSet InvestorDataSet, EntreprenuerDataSet;
        // These booleans will house whether or not a clients clicks are going to entreprenuer accounts or to investor accounts.
        private bool DestinationInvestor = false;
        private bool DestinationEntrepreneur = false;
        // String variables that will be rendered onto our page
        public string InvestorResults, EntreprenuerResults;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ProudSourceUser"] != null)
            {
                UserSessionAccessor Token = new UserSessionAccessor((object[])Session["ProudSourceUser"]);
                {
                    if (Token.UserAuthenticated)
                    {
                        UserName = Token.UserName;
                        UserID = Token.UserID;
                        Token.Dispose();

                        // fill our datasets with data from our queries
                        load_entrepreneurs();
                        load_investors();

                        // build string literals to render onto page
                        build_string_literal_results();

                        // greet our user that has just logged into their account
                        lbl_UserName.Text = UserName;
                    }
                    else
                    {
                        Response.Redirect("UserLogin.aspx");
                    }
                }
            }
            else
            {
                if (Page.IsCallback)
                {
                    Server.Transfer("UserLogin.aspx");
                }
                else
                {
                    Response.Redirect("UserLogin.aspx");
                }
            }

            // Add scripts to page that will handle calls to go to Entrepreneur and Investor profiles but do so only after we see that the client is logged in.

            // Get the page's client script and assign it to a ClientScriptManager
            ClientScriptManager cm = Page.ClientScript;

            // Generate a callback reference
            string cbReference = cm.GetCallbackEventReference(this, "arg", "ToInvestor", null);
            string cbReference2 = cm.GetCallbackEventReference(this, "arg", "ToEntrepreneur", null);

            // Build the callback script block
            string cbscript = "function GoInvestor(arg, context) {" + cbReference + "};";
            string cbscript2 = "function GoEntrepreneur(arg, context) {" + cbReference2 + "};";

            // Register the block
            cm.RegisterClientScriptBlock(Page.GetType(), "GoInvestor", cbscript, true);
            cm.RegisterClientScriptBlock(Page.GetType(), "GoEntrepreneur", cbscript2, true);
        }

        /// <summary>
        /// This method loads a dataset from querying our database for Investor records tied to the UserID of the client.
        /// </summary>
        private void load_investors()
        {
            // use UserID loaded in session to load investor records related to this user
            string query = "SELECT investor_master_id, success_percentage_to_date, total_invested_to_date, create_date_time, investor_profile_name FROM investor_master I WHERE I.user_master_id = @UserID ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            da.SelectCommand.Parameters.AddWithValue("@UserID", UserID);
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
                /// handle exception that can be thrown here, most likley server fails to communicate
                /// </TODO>
            }
            finally
            {
                // close connection
                conn.Close();
            }

            // assign results to a variable we will work with later
            InvestorDataSet = ds;
        }

        /// <summary>
        /// This method loads a dataset from querying our database for Entrepreneur records tied to the UserId of the client.
        /// </summary>
        private void load_entrepreneurs()
        {
            // use UsserID loaded in session to load entrepreneur records related to this user
            string query = "SELECT entrepreneur_master_id, entrepreneur_profile_name, create_date_time FROM entrepreneur_master E WHERE E.user_master_id = @UserID";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            da.SelectCommand.Parameters.AddWithValue("@UserID", UserID);
            DataSet ds = new DataSet();
            try
            {
                // open connection to data base
                conn.Open();
                da.Fill(ds);
            }
            catch (Exception e)
            {
                /// <TODO>
                /// handle exception that can be thrown here, most likley is server fails to communicate
                /// </TODO>
            }
            finally
            {
                // close connection
                conn.Close();
            }

            // assign results to a variable we will work with later
            EntreprenuerDataSet = ds;
        }

        /// <summary>
        /// This method will call both <c>load_investors()</c> and <c>load_entrepreneurs()</c>
        /// Those two functions will load thier datasets by side effect and this function will read the datasets and compose tables to display on client side.
        /// </summary>
        private void build_string_literal_results()
        {
            try
            {
                // contruct our investor results to be rendered on page
                // With this crude implementation we can fillout a table with the data for our users's investor accounts.
                InvestorResults = "<div class=\"investor-results-container\">\r\n";
                InvestorResults += "<h3>Investor accounts</h3><br />";
                InvestorResults += "<table id='tableInvestorAccounts' class='table table-hover table-striped table-condensed'>";
                InvestorResults += "<thead><tr><th>Name</th><th>Date Created</th><th>% Success</th><th>Total Invested</th></tr></thead>";
                InvestorResults += "<tbody>";
                foreach (DataRow i in InvestorDataSet.Tables[0].Rows)
                {
                    //InvestorResults += "<span onclick='GoInvestor(JSON.stringify({ \"action\": \"goInvestor\", \"investorID\": " + i["investor_master_id"].ToString() + "}));'>" + i["investor_profile_name"].ToString() + " | " + i["create_date_time"].ToString() + " | " + i["success_percentage_to_date"].ToString() + " | " + i["total_invested_to_date"].ToString() + "</span><br />";

                    InvestorResults += "<tr onclick='GoInvestor(JSON.stringify({ \"action\": \"goInvestor\", \"investorID\": " + i["investor_master_id"].ToString() + "}));'>";
                    InvestorResults += "<td>" + i["investor_profile_name"].ToString() + "</td>";
                    InvestorResults += "<td>" + i["create_date_time"].ToString() + "</td>";
                    InvestorResults += "<td>" + i["success_percentage_to_date"].ToString() + "</td>";
                    InvestorResults += "<td>" + i["total_invested_to_date"].ToString() + "</td>";
                    InvestorResults += "</tr>";
                }
                InvestorResults += "</tbody>";
                InvestorResults += "</table>";
                InvestorResults += "</div>";
            }
            catch (NullReferenceException e)
            {
                InvestorResults = "<h4>You have no Investor accounts tied to your account.</h4>";
            }
            catch (Exception e)
            {
                InvestorResults = "<h4>An error occured getting Investor accounts tied to your user account.</h4>";
            }

            try
            {
                // construct entreprenuer results to be rendered on page
                // With this crude implementation we can fillout a table with the data for our users's entreprener accounts.
                EntreprenuerResults = "<div class\"entreprenuer-results-container\">\r\n";
                EntreprenuerResults += "<h3>Entreprenuer accounts</h3>\r\n";
                EntreprenuerResults += "<table id='tableEntrepreneurAccounts' class='table table-hover table-striped table-condensed'>";
                EntreprenuerResults += "<thead><tr><th>Name</th><th>Date Created</th></tr></thead>";
                EntreprenuerResults += "<tbody>";
                foreach (DataRow i in EntreprenuerDataSet.Tables[0].Rows)
                {
                    //EntreprenuerResults += string.Format("<span>{0}</span>\r\n", i["create_date_time"]).ToString();

                    EntreprenuerResults += "<tr onclick='GoEntrepreneur(JSON.stringify({ \"action\": \"goEntrepreneur\", \"entrepreneurID\": " + i["entrepreneur_master_id"].ToString() + "}));'>";
                    EntreprenuerResults += "<td>" + i["entrepreneur_profile_name"].ToString() + "</td>";
                    EntreprenuerResults += "<td>" + i["create_date_time"].ToString() + "</td>";
                    EntreprenuerResults += "</tr>";
                }
                EntreprenuerResults += "</tbody>";
                EntreprenuerResults += "</table>";
                EntreprenuerResults += "</div>";
            }
            catch (NullReferenceException e)
            {
                EntreprenuerResults = "<h4>You have no Entrepreneur accounts tied to your user account.</h4>";
            }
            catch (Exception e)
            {
                EntreprenuerResults = "<h4>An error occured getting Entrepreneur accounts tied to your user account.</h4>";
            }
        }

        /// <summary>
        /// This method allows our page to process client callbacks.
        /// </summary>
        /// <param name="eventArgument"></param>
        void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
        {
            // place the incoming eventArgument into a Json Helper object to get an action accessor
            ClientDataInbound clientevent = JsonConvert.DeserializeObject<ClientDataInbound>(eventArgument);

            if (clientevent.action == "goInvestor" && clientevent.check() == true)
            {
                VerifyClientData verifyevent = JsonConvert.DeserializeObject<VerifyClientData>(eventArgument);
                verifyevent.check();

                if (verifyevent.valid && verifyevent.InvestorID != null)
                {
                    // Check to make sure a previous session state does not exist, user may have hit the back page.
                    if (Session["InvestorAccountID"] == null)
                    {
                        Session.Add("InvestorAccountID", Convert.ToInt32(verifyevent.InvestorID));
                        DestinationInvestor = true;
                    }
                    // If the session state does exist remove it explicitly then apply the new one.
                    else if (Session["InvestorAccountID"] != null)
                    {
                        Session.Remove("InvestorAccountID");
                        Session.Add("InvestorAccountID", Convert.ToInt32(verifyevent.InvestorID));
                        DestinationInvestor = true;
                    }
                }
                else
                {
                    // something went wrong most likley the client is trying to hack us.
                }
            }

            if (clientevent.action == "goEntrepreneur" && clientevent.check() == true)
            {
                VerifyClientData verifyevent = JsonConvert.DeserializeObject<VerifyClientData>(eventArgument);
                verifyevent.check();

                if (verifyevent.valid && verifyevent.EntrepreneurID != null)
                {
                    // Check to make sure a previous session state does not exist, user may have hit the back page.
                    if (Session["EntrepreneurAccountID"] == null)
                    {
                        Session.Add("EntrepreneurAccountID", Convert.ToInt32(verifyevent.EntrepreneurID));
                        DestinationEntrepreneur = true;
                    }
                    // If the session state does exist remove it explicitly then apply the new one.
                    else if (Session["EntrepreneurAccountID"] != null)
                    {
                        Session.Remove("EntrepreneurAccountID");
                        Session.Add("EntrepreneurAccountID", Convert.ToInt32(verifyevent.EntrepreneurID));
                        DestinationEntrepreneur = true;
                    }
                }
                else
                {
                    // something went wrong most likley the client is trying to hack us.
                }
            }
        }

        /// <summary>
        /// This method is what is called at the end of a client callback
        /// </summary>
        /// <returns></returns>
        string ICallbackEventHandler.GetCallbackResult()
        {
            if (DestinationInvestor)
            {
                return "Redirecting to Investor Profile";
            }

            if (DestinationEntrepreneur)
            {
                return "Redirecting to Entrepreneur Profile";
            }

            return "Ohh noes Something went wrong!";
        }
    }
}