using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ProudSource
{
    public partial class Search : System.Web.UI.Page
    {
        public string EntrepreneurResults, ProjectResults;

        private DataSet SearchResults;

        protected void Page_Load(object sender, EventArgs e)
        {
            // load our search results from context data
            SearchResults = (DataSet)HttpContext.Current.Items["SearchResults"];

            // construct what will be rendered on the page by what is stored in our dataset given to us that is our results for what the user searched for.
            load_EntrepreneurResults();
            load_ProjectResults();

        }

        private void load_EntrepreneurResults()
        {
            // if the table are empty then we are going to inform the user of that fact
            try
            {
                if (SearchResults.Tables[0].Rows.Count < 1)
                {
                    EntrepreneurResults = "<h3>There where no Entreprneur results found...</h3>";
                    return;
                }
            }
            catch (NullReferenceException e)
            {
                EntrepreneurResults = "<h3>There where no Entreprneur results found...</h3>";
                return;
            }
            // Entrepreneur table is table[0]


            for (int i = 0; i < SearchResults.Tables[0].Rows.Count; i++)
            {
                if (i % 4 == 0)
                {
                    // add a row div class
                    EntrepreneurResults += "<div class=\"row\">";
                }
                // add an element thumbnail
                string img_data = string.Empty;
                try
                {
                    img_data = string.Format(
                        "data:image/jpg;base64,{0}",
                        Convert.ToBase64String(
                            (byte[])SearchResults.Tables[0].Rows[i]["image_file"]
                        )
                    );
                }
                catch (InvalidCastException e)
                {
                    // We tried casting a null value into image data in this case set the image data to point to our default empty prifile image
                    img_data = "../images/profile_empty_m.jpg";
                }
                EntrepreneurResults += "<div class=\"col-xs-6 col-md-3\"><a href=\"/ProudSource/ViewEntrepreneur/" + SearchResults.Tables[0].Rows[i]["entrepreneur_master_id"].ToString() + "\" class=\"thumbnail\"><img src=\"" + img_data + "\"></a></div>";
                if (i % 4 == 0)
                {
                    // add a closing row div class
                    EntrepreneurResults += "</div>";
                }
                if (i == SearchResults.Tables[1].Rows.Count - 1 && i % 4 != 0)
                {
                    // add a closing row div class
                    EntrepreneurResults += "</div>";
                }
            }
        }

        private void load_ProjectResults()
        {
            // if the table are empty then we are going to inform the user of that fact
            try
            {
                if (SearchResults.Tables[1].Rows.Count < 1)
                {// add another closing div where we have reached the end of our results and where mod 4 is not 0, because if it is then that closing div tag has already been added.
                    ProjectResults = "<h3>There Where no Project results found...</h3>";
                    return;
                }
            }
            catch (NullReferenceException e)
            {
                ProjectResults = "<h3>There where no Entreprneur results found...</h3>";
                return;
            }
            // Project table is table[1]

            for (int i = 0; i < SearchResults.Tables[1].Rows.Count; i++)
            {
                if (i % 4 == 0)
                {
                    // add a row div class
                    ProjectResults += "<div class=\"row\">";
                }
                // add an element thumbnail
                ProjectResults += "<div class=\"col-xs-6 col-md-3\"><a href=\"/ProudSource/ViewProject/" + SearchResults.Tables[1].Rows[i]["project_master_id"].ToString() + "\" class=\"thumbnail\"><h4>" + SearchResults.Tables[1].Rows[i]["project_description"].ToString() + "</h4></a></div>";
                if (i % 4 == 0)
                {
                    // add a closing row div class
                    ProjectResults += "</div>";
                }
                if (i == SearchResults.Tables[1].Rows.Count - 1 && i % 4 != 0) 
                {// add another closing div where we have reached the end of our results and where mod 4 is not 0, because if it is then that closing div tag has already been added.
                    // add a closing row div class
                    ProjectResults += "</div>";
                }
            }
        }
    }
}