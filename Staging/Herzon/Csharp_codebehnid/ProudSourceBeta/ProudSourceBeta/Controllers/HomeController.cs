using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProudSourceBeta.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "ProudSource.us";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us";
            
            return View();
        }
    }
}