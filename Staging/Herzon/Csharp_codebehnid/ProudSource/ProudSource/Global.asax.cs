using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;

namespace ProudSource
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // This line will register URL routes
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        void RegisterRoutes(RouteCollection routes)
        {
            // Register a route for ViewEntrepreneur/{EntrepreneurID}
            routes.Add(
                "View Entrepreneur",
                new Route("ViewEntrepreneur/{EntrepreneurID}", 
                    new EntrepreneurRouteHandler()
                )
            );

            // Register a route for ViewProject/{ProjectID}
            routes.Add(
                "View Project",
                new Route("ViewProject/{ProjectID}",
                    new ProjectRouteHandeler()
                )
            );

            // Register a route for ViewPROC/{PROC_ID}
            routes.Add(
                "View PROC",
                new Route("ViewPROC/{PROC_ID}",
                    new PROCRouteHandeler()
                )
            );

            // Register a route for Search/{arg}
            routes.Add(
                "Search",
                new Route("Search/{Arg}",
                    new SearchRouteHandeler()
                )
            );
        }
    }
}