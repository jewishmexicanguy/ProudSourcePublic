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
    /// <summary>
    /// This class is what is used to handle requests to view Entrepreneur Profiles without having to be logged in as a user, in effect it also serves as a vanity page that may be imbedded else where as URLs.
    /// </summary>
    public class EntrepreneurRouteHandler : IRouteHandler
    {
        /// <summary>
        /// Define a place to store what will be our entrepreneur results by side effect.
        /// </summary>
        private DataSet EntrepreneurDS = new DataSet();

        /// <summary>
        /// Define a connection object that is private to this object.
        /// </summary>
        private NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["ProudSourceDB"].ConnectionString);

        /// <summary>
        /// Class constructor that sets the virtual path that this object will be making a page of. 
        /// It takes no parameters
        /// </summary>
        public EntrepreneurRouteHandler()
        {
            VirtualPath = "~/ViewEntrepreneur.aspx";
        }

        /// <summary>
        /// The virtual string path that this class will return as the page object to route to.
        /// </summary>
        public string VirtualPath { get; private set; }

        /// <summary>
        /// This method is implimented explicitly so that this class is able to serve as a URL handler for our EntrepreneurView page.
        /// It first makes sure that the parameters that is used in the URL that was used to try and get to our page is an integer else it will error out a 404.
        /// It then attempts to make the query to get the information for our entrepreneur, if it failes it will return a 404 error.
        /// If it detects that no user info was obtained then it will error our as a 404.
        /// </summary>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            int EntrepreneurID;

            // check to make sure the parameter is an integer or can be converted into an integer
            try
            {
                EntrepreneurID = Convert.ToInt32(requestContext.RouteData.Values["EntrepreneurID"].ToString());
            }
            catch (Exception e) // if something goes wrong return a 404
            {
                throw new HttpException(404, "Not found");
            }

            // if EntrepreneurID is negative they are trying to pull something funny, throw 404
            if (EntrepreneurID < 0)
            {
                throw new HttpException(404, "Not found");
            }

            // now attempt to query for data to show on our Entrepreneur Profile
            try
            {
                get_EntrepreneurData(EntrepreneurID);
            }
            catch (Exception e) // if something goes wrong return a 404
            {
                throw new HttpException(404, "Not found");
            }

            // Check that our data set actually returned something
            if (EntrepreneurDS.Tables[0].Rows.Count < 1)
            {
                throw new HttpException(404, "Not found");
            }

            // store our Entrepreneur DataSet
            HttpContext.Current.Items.Add("Entrepreneur", EntrepreneurDS);

            // return our page
            var page = BuildManager.CreateInstanceFromVirtualPath(VirtualPath, typeof(Page)) as IHttpHandler;
            return page;
        }

        /// <summary>
        /// Make the request from our DataBase and replace the default empty DataSet in this class with what we will be retriveing from our Data Base where entrepreneur_public for that record is true.
        /// </summary>
        private void get_EntrepreneurData(int ID)
        {
            /// <SQL>
            /// SELECT EM.entrepreneur_master_id, EM.entrepreneur_profile_name, IMGM.image_file, EM.entrepreneur_public 
            /// FROM entrepreneur_master EM 
            /// LEFT OUTER JOIN entrepreneur_image_xref EMXREF 
            ///     ON EMXREF.entrepreneur_master_id = EM.entrepreneur_master_id 
            /// LEFT OUTER JOIN image_master IMGM 
            ///     ON IMGM.image_master_id = EMXREF.image_master_id 
            /// WHERE EM.entrepreneur_master_id = @EntrepreneurID 
            ///     AND EM.entrepreneur_public = TRUE
            /// </SQL>
            string query = "SELECT EM.entrepreneur_master_id, EM.entrepreneur_profile_name, IMGM.image_file, EM.entrepreneur_public FROM entrepreneur_master EM LEFT OUTER JOIN entrepreneur_image_xref EMXREF ON EMXREF.entrepreneur_master_id = EM.entrepreneur_master_id LEFT OUTER JOIN image_master IMGM ON IMGM.image_master_id = EMXREF.image_master_id WHERE EM.entrepreneur_master_id = @EntrepreneurID AND EM.entrepreneur_public = TRUE";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            da.SelectCommand.Parameters.AddWithValue("@EntrepreneurID", ID);
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
            EntrepreneurDS = ds;
        }
    }
}