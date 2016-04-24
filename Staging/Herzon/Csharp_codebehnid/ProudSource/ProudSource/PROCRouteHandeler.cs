using System;
using System.Web;
using System.Web.Compilation;
using System.Web.Routing;
using System.Web.UI;
using System.Data;
using System.Configuration;
using Npgsql;

namespace ProudSource
{
    public class PROCRouteHandeler : IRouteHandler
    {
        private DataSet PROC_DS = new DataSet();

        private NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["ProudSourceDB"].ConnectionString);

        public PROCRouteHandeler()
        {
            VirtualPath = "~/ViewPROC.aspx";
        }

        public string VirtualPath { get; private set; }

        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            int ProcID;

            try
            {
                ProcID = Convert.ToInt32(requestContext.RouteData.Values["PROC_ID"].ToString());
            }
            catch (Exception e)
            {
                throw new HttpException(404, "Not found");
            }

            if (ProcID < 0)
            {
                throw new HttpException(404, "Not found");
            }

            try
            {
                get_PROC(ProcID);
            }
            catch (Exception e)
            {
                throw new HttpException(404, "Not found");
            }

            if (PROC_DS.Tables[0].Rows.Count < 1)
            {
                throw new HttpException(404, "Not found");
            }

            HttpContext.Current.Items.Add("PROC", PROC_DS);

            var page = BuildManager.CreateInstanceFromVirtualPath(VirtualPath, typeof(Page)) as IHttpHandler;
            return page;
        }

        /// <summary>
        /// This method will make a query to our Data Base and collect data that is related to this proc along with data related to thr project, investor and investor_proc_xref table relations.
        /// </summary>
        /// <param name="ID"></param>
        private void get_PROC(int ID)
        {
            ///<SQL> select statement to be used
            /// SELECT PROCM.proc_master_id, PROCM.proc_begin_date, PROCM.proc_end_date, 
            /// PROCM.profit_percentage, PROCM.create_date_time, PROCM.successful, 
            /// PROCM.expired, PROCM.amount_invested, PROCM.project_accepted, 
            /// PM.project_description, PM.project_master_id, PIX.investor_accepted, 
            /// IM.investor_profile_name, imgM.image_file, IM.investor_master_id 
            /// FROM proc_master PROCM 
            /// JOIN project_master PM 
            ///     ON PM.project_master_id = PROCM.project_master_id 
            /// JOIN proc_investor_xref PIX 
            ///     ON PROCM.proc_master_id = PIX.proc_master_id 
            /// JOIN investor_master IM 
            ///     ON IM.investor_master_id = PIX.investor_master_id 
            /// JOIN investor_image_xref IimgXREF 
            ///     ON IimgXREF.investor_master_id = IM.investor_master_id 
            /// JOIN image_master imgM 
            ///     ON imgM.image_master_id = IimgXREF.image_master_id 
            /// WHERE PROCM.proc_master_id = @PROC_ID
            /// </SQL>
            //string query = "SELECT PROCM.proc_master_id, PROCM.proc_begin_date, PROCM.proc_end_date, PROCM.profit_percentage, PROCM.create_date_time, PROCM.successful, PROCM.expired, PROCM.amount_invested, PROCM.project_accepted, PM.project_description, PM.project_master_id, PIX.investor_accepted, IM.investor_profile_name, IM.investor_image, IM.investor_master_id FROM proc_master PROCM JOIN project_master PM ON PM.project_master_id = PROCM.project_master_id JOIN proc_investor_xref PIX ON PROCM.proc_master_id = PIX.proc_master_id JOIN investor_master IM ON IM.investor_master_id = PIX.investor_master_id WHERE PROCM.proc_master_id = @PROC_ID";
            string query = "SELECT PROCM.proc_master_id, PROCM.proc_begin_date, PROCM.proc_end_date, PROCM.profit_percentage, PROCM.create_date_time, PROCM.successful, PROCM.expired, PROCM.amount_invested, PROCM.project_accepted, PM.project_description, PM.project_master_id, PIX.investor_accepted, IM.investor_profile_name, imgM.image_file, IM.investor_master_id FROM proc_master PROCM JOIN project_master PM ON PM.project_master_id = PROCM.project_master_id JOIN proc_investor_xref PIX ON PROCM.proc_master_id = PIX.proc_master_id JOIN investor_master IM ON IM.investor_master_id = PIX.investor_master_id JOIN investor_image_xref IimgXREF ON IimgXREF.investor_master_id = IM.investor_master_id JOIN image_master imgM ON imgM.image_master_id = IimgXREF.image_master_id WHERE PROCM.proc_master_id = @PROC_ID";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            da.SelectCommand.Parameters.AddWithValue("@PROC_ID", ID);
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
            PROC_DS = ds;
        }
    }
}