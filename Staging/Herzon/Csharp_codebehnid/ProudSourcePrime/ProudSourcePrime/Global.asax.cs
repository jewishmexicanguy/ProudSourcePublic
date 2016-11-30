using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;

namespace ProudSourcePrime
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            /// This line will apply Authorization filters to all controls unless they explicitly define anonymous auth behaviour.
            Security.FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            /// This line will register the bundles of javascript and styles that will be provided as a base to all the pages in this web app. This happens because it is applied in the _Layout file that views implement.
            App_Start.BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
