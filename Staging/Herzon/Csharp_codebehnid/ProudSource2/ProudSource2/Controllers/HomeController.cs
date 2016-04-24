using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProudSource2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact ProudSource.us";
            ViewBag.MessageBoxlbl = "We would love to hear from you";
            /// TempData[] is a dictionary that can be used if we need to pass data to view the client has requested.
            /// TempData["key"] = '\0';
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string message)
        {
            // TODO : send message to suport@proudsource.us
            ViewBag.Message = "Contact ProudSource.us";
            ViewBag.MessageBoxlbl = "Thank you for your message";

            return View();
        }
    }
}