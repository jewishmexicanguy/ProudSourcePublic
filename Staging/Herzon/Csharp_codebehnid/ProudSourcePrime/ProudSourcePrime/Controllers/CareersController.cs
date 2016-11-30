using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProudSourcePrime.Controllers
{
    public class CareersController : Controller
    {
        // GET: Careers
        public ActionResult Index()
        {
            return View();
        }

        // GET: Carrers/Engineering
        public ActionResult Engineering()
        {
            return View();
        }

        // GET: Carrers/Strategy
        public ActionResult Strategy()
        {
            return View();
        }
    }
}