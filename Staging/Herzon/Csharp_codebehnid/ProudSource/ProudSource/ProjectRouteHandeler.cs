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
    /// This class will handle routing request to view Project profiles.
    /// </summary>
    public class ProjectRouteHandeler : IRouteHandler
    {
        /// <summary>
        /// Define a place to store what will be our Project results by side effect.
        /// </summary>
        private DataSet ProjectDS = new DataSet();

        /// <summary>
        /// Define a connection object that is private to this object.
        /// </summary>
        private NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["ProudSourceDB"].ConnectionString);

        /// <summary>
        /// Class constructor that sets the virtual path that this class will ne making an object of later to transfer the clients page request.
        /// </summary>
        public ProjectRouteHandeler()
        {
            VirtualPath = "~/ViewProject.aspx";
        }

        /// <summary>
        /// The virtual string path that this class will return as the page object to route to.
        /// </summary>
        public string VirtualPath { get; private set; }

        /// <summary>
        /// This method is implimented explicitly to satisfy the requirment of needed to handle routing to our page ViewProject.aspx.
        /// This method deals with gathering the data neccesary for the project in question.
        /// Where the data realtion does not exist, is bad data or where the project is not public a 404 exception is thrown.
        /// </summary>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            int ProjectID;

            // check to make sure the parameter is an integer or can be converted into an integer
            try
            {
                ProjectID = Convert.ToInt32(requestContext.RouteData.Values["ProjectID"].ToString());
            }
            catch (Exception e) // if something goes wrong return a 404
            {
                throw new HttpException(404, "Not found");
            }

            // if ProjectID is negative they are trying to pull something funny, throw 404
            if (ProjectID < 0)
            {
                throw new HttpException(404, "Not found");
            }

            // now attempt to query for data to show on our Project Profile
            try
            {
                get_ProjectData(ProjectID);
            }
            catch (Exception e) // if something goes wrong return a 404
            {
                throw new HttpException(404, "Not found");
            }

            // Check that our data set actually returned something
            if (ProjectDS.Tables[0].Rows.Count < 1)
            {
                throw new HttpException(404, "Not found");
            }

            // store our Entrepreneur DataSet
            HttpContext.Current.Items.Add("Project", ProjectDS);

            // return our page
            var page = BuildManager.CreateInstanceFromVirtualPath(VirtualPath, typeof(Page)) as IHttpHandler;
            return page;
        }

        /// <summary>
        /// This method will make a query to our data base and return place a dataset into the resident for this object where the project_public for this record is true.
        /// </summary>
        /// <param name="ID"></param>
        private void get_ProjectData(int ID)
        {
            string query = "SELECT project_master_id, project_description, create_date_time, project_public, investment_goal, entrepreneur_master_id FROM project_master WHERE project_master_id = @project_master_id AND project_public  = TRUE";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
            da.SelectCommand.Parameters.AddWithValue("@project_master_id", ID);
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
            ProjectDS = ds;
        }
    }
}