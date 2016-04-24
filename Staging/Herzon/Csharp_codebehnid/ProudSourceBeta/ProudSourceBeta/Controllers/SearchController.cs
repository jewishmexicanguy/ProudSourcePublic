using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProudSourceBeta.Controllers
{
    [AllowAnonymous]
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            Models.SearchIndexViewModel trendingProjects = new Models.SearchIndexViewModel();
            return View(trendingProjects);
        }

        // GET: Search/KeyArg/{string argument}
        public ActionResult KeyArg()
        {
            string[] holding = Request.RawUrl.Split(new char[] { '/' });
            string keyarg = holding[holding.Count() - 1];
            Models.SearchKeyArgViewModel searchAccounts = new Models.SearchKeyArgViewModel(keyarg);
            return View(searchAccounts);
        }
    }
}