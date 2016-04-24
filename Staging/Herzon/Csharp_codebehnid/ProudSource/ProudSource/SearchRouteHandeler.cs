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
    public class SearchRouteHandeler : IRouteHandler
    {
        private DataSet SearchDS = new DataSet();

        /// <summary>
        /// Define a connection object that is private to this object.
        /// </summary>
        private NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["ProudSourceDB"].ConnectionString);

        public SearchRouteHandeler()
        {
            VirtualPath = "~/Search.aspx";
        }

        public string VirtualPath { get; private set; }

        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            // check to make sure the para meter is safe, since we will be making a query based on a string that we will be reading in from the URL
            string Argument = requestContext.RouteData.Values["Arg"].ToString();

            if (Argument.Contains("DROP") | Argument.Contains("drop") | Argument.Contains(";") | Argument.Contains("DELETE") | Argument.Contains("delete") | Argument.Length > 255)
            {
                throw new HttpException(403, "Unauthorized Parameter");
            }

            try
            {
                get_SearchResults(Argument);
            }
            catch (Exception e)
            {
                throw new HttpException(403, "Unauthorized Parameter");
            }

            // store our results in context data
            HttpContext.Current.Items.Add("SearchResults", SearchDS);

            // return our page
            var page = BuildManager.CreateInstanceFromVirtualPath(VirtualPath, typeof(Page)) as IHttpHandler;
            return page;
        }

        public void get_SearchResults(string arg)
        {
            // Define string array to hold both of our queries
            string[] queries = new string[2];

            /// <SQL> Easy to read sql string
            /// SELECT EM.entrepreneur_master_id, EM.entrepreneur_profile_name, IMGM.image_file
            /// FROM entrepreneur_master EM
            /// LEFT OUTER JOIN entrepreneur_image_xref EMXREF
            ///     ON EMXREF.entrepreneur_master_id = EM.entrepreneur_master_id
            /// LEFT OUTER JOIN image_master IMGM
            ///     ON IMGM.image_master_id = EMXREF.image_master_id
            /// WHERE EM.entrepreneur_profile_name LIKE @arg
            ///     AND EM.entrepreneur_public = TRUE
            /// </SQL>
            queries[0] = "SELECT EM.entrepreneur_master_id, EM.entrepreneur_profile_name, IMGM.image_file FROM entrepreneur_master EM LEFT OUTER JOIN entrepreneur_image_xref EMXREF ON EMXREF.entrepreneur_master_id = EM.entrepreneur_master_id LEFT OUTER JOIN image_master IMGM ON IMGM.image_master_id = EMXREF.image_master_id WHERE EM.entrepreneur_profile_name LIKE @arg AND EM.entrepreneur_public = TRUE";
            
            /// <SQL>
            /// SELECT project_master_id, project_description 
            /// FROM project_master 
            /// WHERE project_description LIKE @arg
            /// </SQL>
            queries[1] = "SELECT project_master_id, project_description FROM project_master WHERE project_description LIKE @arg";

            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(queries[0], conn);
            da1.SelectCommand.Parameters.AddWithValue("@arg", "%" + arg + "%");
            DataSet ds1 = new DataSet();
            try
            {
                conn.Open();
                da1.Fill(ds1);
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(queries[1], conn);
            da2.SelectCommand.Parameters.AddWithValue("@arg", string.Format("%{0}%", arg));
            DataSet ds2 = new DataSet();
            try
            {
                conn.Open();
                da2.Fill(ds2);
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(ds1.Tables[0].Copy());
            ds2.Tables[0].TableName = "Table1";
            ds.Tables.Add(ds2.Tables[0].Copy());

            SearchDS = ds;
        }
    }
}