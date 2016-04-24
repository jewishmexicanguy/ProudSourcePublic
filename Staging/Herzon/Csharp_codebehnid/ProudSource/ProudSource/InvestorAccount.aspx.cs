using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using Npgsql;

namespace ProudSource
{
    public partial class InvestorAccount : System.Web.UI.Page
    {
        public string UserName, PROCResults;

        private string InvestorName;

        private int UserID, InvestorID;

        private NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["ProudSourceDB"].ConnectionString);

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

                        // greet our Investor user
                        lbl_UserName.Text = UserName;
                    }
                    else
                    {
                        Response.Redirect("UserLogin.aspx");
                    }
                }

                // This page also requires that the user is logged in as an investor account, check to make sure that they are if not take them back to the user dashboard.
                if (Session["InvestorAccountID"] != null)
                {
                    InvestorID = (int)Session["InvestorAccountID"];

                    // load PROCS that this investor has agrred to.
                    load_PROCS();

                    // load Investor data to display on our web page
                    load_InvestorData();

                    // greet our accoutn user
                    lbl_Name.Text = InvestorName;
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
        /// This function will make a query for our investor's information based off of the investorID and will then attempt to dispaly this information onto the page for our client browser.
        /// </summary>
        private void load_InvestorData()
        {
            /// <SQL> Easy to read sql string
            /// SELECT IM.investor_profile_name, IMGM.image_file, IM.investor_public
            /// FROM investor_master IM
            /// LEFT OUTER JOIN investor_image_xref IMXREF 
            ///     ON IMXREF.investor_master_id = IM.investor_master_id
            /// LEFT OUTER JOIN image_master IMGM 
            ///     ON IMGM.image_master_id = IMXREF.image_master_id
            /// WHERE IM.investor_master_id = @InvestorID
            /// </SQL>
            string query = "SELECT IM.investor_profile_name, IMGM.image_file, IM.investor_public FROM investor_master IM LEFT OUTER JOIN investor_image_xref IMXREF ON IMXREF.investor_master_id = IM.investor_master_id  LEFT OUTER JOIN image_master IMGM ON IMGM.image_master_id = IMXREF.image_master_id WHERE IM.investor_master_id = @InvestorID";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            da.SelectCommand.Parameters.AddWithValue("@InvestorID", InvestorID);
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
            InvestorName = (string)ds.Tables[0].Rows[0]["investor_profile_name"];

            // get profile picture
            if (ds.Tables[0].Rows[0]["image_file"].ToString().Length > 0) // check to make sure that the field is not null before we try to do fancy byte array conversion into an image.
            {
                img_Investor.ImageUrl = string.Format(
                    "data:image/jpg;base64,{0}",
                    Convert.ToBase64String(
                        (byte[])ds.Tables[0].Rows[0]["image_file"]
                    )
                );
            }
            else // we need to make the picture to show be our default picture for empty profiles
            {
                img_Investor.ImageUrl = "../images/profile_empty_m.jpg";
            }

            // get whether or not the account is public
            if (ds.Tables[0].Rows[0]["investor_public"].ToString() == "True")
            {
                lbl_ProfilePublic.Text = "Yes";
            }
            else
            {
                lbl_ProfilePublic.Text = "No";
            }
        }

        /// <summary>
        /// This method will load all records for this investor that exist on our Projects_Entrepreneur_Investor_XREF table, because records on that table are what model this relationship.
        /// </summary>
        private void load_PROCS()
        {
            // make query to get a dataset from our Data Base
            string query = "SELECT PIX.proc_investor_xref_id, PM.proc_master_id, PIX.create_date_time, PIX.investor_accepted, PM.proc_begin_date, PM.proc_end_date, PM.profit_percentage, PM.project_accepted FROM proc_investor_xref PIX JOIN proc_master PM ON PIX.proc_master_id = PM.proc_master_id WHERE PIX.investor_master_id = @InvestorID";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            da.SelectCommand.Parameters.AddWithValue("InvestorID", InvestorID);
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

            // if our query resturned no rows we should probably inform the user instead of making an empty table
            try
            {
                if (ds.Tables[0].Rows.Count < 0)
                {
                    PROCResults = "<h3>No PROCS were found for your account... perhaps you should find some projects and start investing</h3>";
                }
                else // start building what we will render onto the page by placing an open table bracket and table head to be shown
                {
                    PROCResults += "<h3>Projects</h3><br />";
                    PROCResults += "<table id='tableProjectAccounts' class='table table-hover table-striped table-condensed'>";
                    PROCResults += "<thead><tr><th>Date Created</th><th>% of net Profit</th><th>Project Accepted</th><th>Investor Accepted</th><th>Begin Date</th><th>End Date</th></tr></thead>";
                    PROCResults += "<tbody>";
                    foreach (DataRow i in ds.Tables[0].Rows)
                    {
                        PROCResults += "<tr>";
                        PROCResults += "<td><a href='" + "ViewPROC/" + i["proc_master_id"].ToString() + "'>" + i["create_date_time"].ToString() +"</a></td>";
                        PROCResults += "<td>" + i["profit_percentage"].ToString() +"</td>";
                        try {
                            if ((bool)i["project_accepted"])
                            {
                                PROCResults += "<td>Accepted</td>";
                            }
                            else
                            {
                                PROCResults += "<td>Denied</td>";
                            }
                        }
                        catch (InvalidCastException e)
                        {
                            PROCResults += "<td>Denied</td>";
                        }
                        try
                        {
                            if ((bool)i["investor_accepted"])
                            {
                                PROCResults += "<td>Accepted</td>";
                            }
                            else
                            {
                                PROCResults += "<td>Denied</td>";
                            }
                        }
                        catch (InvalidCastException e)
                        {
                            PROCResults += "<td>Denied</td>";
                        }
                        PROCResults += "<td>" + i["proc_begin_date"].ToString() + "</td>";
                        PROCResults += "<td>" + i["proc_end_date"].ToString() + "</td>";
                        PROCResults += "</tr>";
                    }
                    PROCResults += "</tbody>";
                    PROCResults += "</table>";
                }
            }
            catch (Exception e)
            {
                PROCResults = "<h3><Something went wrong attempting to get PROC tied to your profile!/h3>";
            }
            // loop over results set of our query for each row add a row to our html table

            // after loop, now add the closing table tags
        }

        /// <summary>
        /// This method will get called when the client side pushes the button to update thier profile with new data.
        /// This will update the current record that was used to load this page in the first place.
        /// After Update the page will be reloaded so that the user can see thier changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Update_Click(object sender, EventArgs e)
        {
            // instantiate a new oredered dictionary object
            OrderedDictionary OD = new OrderedDictionary();
            OD.Add("investor_profile_name", Tbox_Name.Text);
            OD.Add("investor_public", RButton_Public.Checked);
            OD.Add("investor_image", FileUp_Image.FileBytes);

            // call our function that will update the profile.
            update_Investor(InvestorID, OD);

            // reload the page.
            Response.Redirect(Request.RawUrl);
        }

        /// <summary>
        /// This method will be what is responsible for pushing the values submited by the client to our Data Base.
        /// This will be acomplished with the useof a stored procedure.
        /// </summary>
        /// <StoredProcedure name="update_investor(INTEGER, CHARACTER VARYING, BOOLEAN, BYTEA)"></StoredProcedure>
        /// <note>
        /// It will replace the first iamge that is related to this account instead of adding new images.
        /// </note>
        /// <param name="InvestorID"></param>
        /// <param name="Update_Dictionary"></param>
        private void update_Investor(int InvestorID, OrderedDictionary Update_Dictionary)
        {
            // we are going to call a stored procedure that will handle all of the logic for us of when to insert what fields and to know whether they are empty or not.
            string query = "update_investor";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            da.SelectCommand.Parameters.AddWithValue("ATinvestor_master_id", InvestorID);
            da.SelectCommand.Parameters.AddWithValue("ATinvestor_profile_name", Update_Dictionary["investor_profile_name"]);
            da.SelectCommand.Parameters.AddWithValue("ATinvestor_public", Update_Dictionary["investor_public"]);
            da.SelectCommand.Parameters.AddWithValue("ATinvestor_image", Update_Dictionary["investor_image"]);
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