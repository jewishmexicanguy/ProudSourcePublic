using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProudSourcePrime.Controllers
{
    [AllowAnonymous]
    public class WelcomeController : Controller
    {
        // GET: Welcome
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Legal()
        {
            return View();
        }

        /// <summary>
        /// This will be the ProudSource parking spac until we are reformed with a new glorious direction.
        /// </summary>
        /// <returns></returns>
        public ActionResult ParkingSpace()
        {
            return View();
        }
    }
}