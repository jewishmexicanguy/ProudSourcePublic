using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Npgsql;
using System.Configuration;

namespace ProudSource
{
    public partial class ViewPROC : System.Web.UI.Page
    {
        private int ProcID, ProjectID, InvestorID;

        private bool ProjectIsOwner, InvestorIsOwner;

        /// <summary>
        /// Define a connection object that is private to this object.
        /// </summary>
        private NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["ProudSourceDB"].ConnectionString);

        private DataSet ProcDS;

        protected void Page_Load(object sender, EventArgs e)
        {
            // save Data from context
            ProcDS = (DataSet)HttpContext.Current.Items["PROC"];

            // save ProcID from context
            try
            {
                ProcID = (int)ProcDS.Tables[0].Rows[0]["proc_master_id"];
            }
            catch (Exception k)
            {
                throw new HttpException(404, "Not found");
            }

            // save ProjectID from context
            try
            {
                ProjectID = (int)ProcDS.Tables[0].Rows[0]["project_master_id"];
            }
            catch (Exception k)
            {

            }

            // save InvestorID from context
            try
            {
                InvestorID = (int)ProcDS.Tables[0].Rows[0]["investor_master_id"];
            }
            catch (Exception k)
            {
                
            }

            // check to see if logged in Project owner is logged in and looking at page
            if (Session["ProjectID"] != null)
            {
                if ((int)Session["ProjectID"] == ProjectID)
                {
                    ProjectIsOwner = true;
                }
                else
                { 
                    ProjectIsOwner = false;
                }
            }
            else
            {
                ProjectIsOwner = false;
            }

            // check to see if Investor owner is logged in an looking at page
            if (Session["InvestorID"] != null)
            {
                if ((int)Session["InvestorID"] == InvestorID)
                {
                    InvestorIsOwner = true;
                }
                else
                {
                    InvestorIsOwner = false;
                }
            }
            else
            {
                InvestorIsOwner = false;
            }

            // load contents of controls using data save from context
            load_PROC_details();

            // activate controls that are for Project owner of this PROC
            load_Project_Owner_Controls();

        }

        private void load_PROC_details()
        {
            lbl_BeginDate.Text = ProcDS.Tables[0].Rows[0]["proc_begin_date"].ToString();
            lbl_EndDate.Text = ProcDS.Tables[0].Rows[0]["proc_end_date"].ToString();
            lbl_PROC_Percentage.Text = ProcDS.Tables[0].Rows[0]["profit_percentage"].ToString();
            try {
                if ((bool)ProcDS.Tables[0].Rows[0]["successful"])
                {
                    lbl_PROC_Active.Text = "Yes";
                }
                else
                {
                    lbl_PROC_Active.Text = "No";
                }
            }
            catch (InvalidCastException k)
            {
                lbl_PROC_Active.Text = "No";
            }
            lbl_AmountInvested.Text = ProcDS.Tables[0].Rows[0]["amount_invested"].ToString();
            if ((bool)ProcDS.Tables[0].Rows[0]["project_accepted"])
            {
                lbl_ProjectAccepted.Text = "Yes";
            }
            else
            {
                lbl_ProjectAccepted.Text = "No";
            }
            if ((bool)ProcDS.Tables[0].Rows[0]["investor_accepted"])
            {
                lbl_InvestorAccepted.Text = "Yes";
            }
            else
            {
                lbl_InvestorAccepted.Text = "No";
            }
            lbl_InvestorName.Text = ProcDS.Tables[0].Rows[0]["investor_profile_name"].ToString();
            if (ProcDS.Tables[0].Rows[0]["image_file"].ToString().Length > 0)
            {
                img_Investor.ImageUrl = string.Format(
                    "data:image/jpg;base64,{0}",
                    Convert.ToBase64String(
                        (byte[])ProcDS.Tables[0].Rows[0]["image_file"]
                    )
                );
            }
            else
            {
                img_Investor.ImageUrl = "../ProudSource/images/profile_empty_m.jpg";
            }
            lbl_ProjectDescription.Text = ProcDS.Tables[0].Rows[0]["project_description"].ToString();

        }

        private void load_Project_Owner_Controls()
        {
            if (ProjectIsOwner)
            {
                lbl_Project_AcceptPROC.Visible = true;
                Btn_Project_AcceptPROC.Visible = true;
            }
            else
            {
                lbl_Project_AcceptPROC.Visible = false;
                Btn_Project_AcceptPROC.Visible = false;
            }
        }

        /// <summary>
        /// Event fires when the Project owner confirms that they accept the PROC terms.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_Project_AcceptPROC_Click(object sender, EventArgs e)
        {
            // If this button was clicked then we need to update the record that is this proc and update the column project accepted with a TRUE value.
            string query = "UPDATE proc_master SET project_accepted = @accepted WHERE proc_master_id = @PROC_ID";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            da.SelectCommand.Parameters.AddWithValue("@PROC_ID", ProcID);
            da.SelectCommand.Parameters.AddWithValue("@accepted", true);
            try
            {
                conn.Open();
                da.SelectCommand.ExecuteNonQuery();
            }
            catch (Exception k)
            {

            }
            finally
            {
                conn.Close();
            }
        }
    }
}