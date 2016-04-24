using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Npgsql;
using System.Data;
using System.Configuration;

namespace ProudSource
{
    public partial class CreatePROC : System.Web.UI.Page
    {
        // Private variables that will hold user IDs 
        private int UserID, InvestorID, ProjectID, ProcID;
        // PostGreSQLconnection object
        private NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["ProudSourceDB"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ProudSourceUser"] != null)
            {
                UserSessionAccessor Token = new UserSessionAccessor((object[])Session["ProudSourceUser"]);
                {
                    if (Token.UserAuthenticated)
                    {
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

            // This page also requires that the user is logged in as an investor account, check to make sure that they are if not take them back to the user dashboard.
            if (Session["InvestorAccountID"] != null)
            {
                InvestorID = (int)Session["InvestorAccountID"];
            }
            else
            {
                Response.Redirect("UserDashboard.aspx");
            }

            // This page requires that the user is logged in as an investor, check to make sure that they are or send them back to the dashboard.
            if (Session["ProjectID"] != null)
            {
                ProjectID = (int)Session["ProjectID"];
            }
            else
            {
                Response.Redirect("UserDashboard.aspx");
            }
        }

        /// <summary>
        /// This is the server side method that gets called when the client clicks the create PROC button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_CreatePROC_Click(object sender, EventArgs e)
        {
            // check to make sure that controls are not empty.
            if (Tbox_PROC_Percent.Text == string.Empty)
            {
                return;
            }

            // check to make sure that controls are not empty.
            if (Tbox_PROC_investment.Text == string.Empty)
            {
                return;
            }

            // check to make sure that controls are not empty.
            if (Cal_BeginDate.SelectedDate == DateTime.MinValue)
            {
                return;
            }

            // check to make sure that controls are not empty.
            if (Cal_EndDate.SelectedDate == DateTime.MinValue)
            {
                return;
            }

            OrderedDictionary Dict = new OrderedDictionary();
            Dict.Add("proc_begin_date", Cal_BeginDate.SelectedDate);
            Dict.Add("proc_end_date", Cal_EndDate.SelectedDate);
            Dict.Add("profit_percentage", Convert.ToInt32(Tbox_PROC_Percent.Text));
            Dict.Add("amount_invested", Convert.ToInt32(Tbox_PROC_investment.Text));

            // call method to make our PROC then relate that proc to our Investor
            create_PROC(UserID, InvestorID, ProjectID, Dict);

            // take our client to the page where they can see thier PROC
            Response.Redirect("ViewPROC/" + ProcID.ToString());            
        }

        /// <summary>
        /// This method will make two sql calls to our server and it will create new records in our proc_master table collect the ID created and insert a new record into our proc_investor_XREF table using the collected ID.
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="InvestorID"></param>
        /// <param name="ProjectID"></param>
        /// <param name="PROC_Dict"></param>
        private void create_PROC(int UserID, int InvestorID, int ProjectID, OrderedDictionary PROC_Dict)
        {
            string query = "INSERT INTO proc_master (create_user_master_id, proc_begin_date, proc_end_date, profit_percentage, payment_interval_id, create_date_time, amount_invested, project_master_id, project_accepted, expired, mod_user_master_id) VALUES (@UserID, @proc_begin_date, @proc_end_date, @profit_percentage, 1, now(), @amount_invested, @ProjectID, FALSE, FALSE, 2); SELECT currval(pg_get_serial_sequence('proc_master','proc_master_id'));";
            NpgsqlDataAdapter PROC_Insert = new NpgsqlDataAdapter(query, conn);
            PROC_Insert.SelectCommand.Parameters.AddWithValue("@UserID", UserID);
            PROC_Insert.SelectCommand.Parameters.AddWithValue("@ProjectID", ProjectID);
            PROC_Insert.SelectCommand.Parameters.AddWithValue("@proc_begin_date", PROC_Dict["proc_begin_date"]);
            PROC_Insert.SelectCommand.Parameters.AddWithValue("@proc_end_date", PROC_Dict["proc_end_date"]);
            PROC_Insert.SelectCommand.Parameters.AddWithValue("@profit_percentage", PROC_Dict["profit_percentage"]);
            PROC_Insert.SelectCommand.Parameters.AddWithValue("@amount_invested", PROC_Dict["amount_invested"]);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                PROC_Insert.Fill(ds);
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
            int PROC_ID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            ProcID = PROC_ID;
            query = "INSERT INTO proc_investor_xref (proc_master_id, investor_master_id, investor_accepted, mod_user_master_id) VALUES (@PROC_ID, @InvestorID, TRUE, 2)";
            NpgsqlCommand Insert_PROC_Investor_XREF = new NpgsqlCommand(query, conn);
            Insert_PROC_Investor_XREF.Parameters.AddWithValue("@PROC_ID", PROC_ID);
            Insert_PROC_Investor_XREF.Parameters.AddWithValue("@InvestorID", InvestorID);
            try
            {
                conn.Open();
                Insert_PROC_Investor_XREF.ExecuteNonQuery();
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