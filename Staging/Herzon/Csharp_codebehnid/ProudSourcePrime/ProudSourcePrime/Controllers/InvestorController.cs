using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ProudSourcePrime.Controllers
{
    [Authorize]
    public class InvestorController : Controller
    {
        // GET: Investor/Index
        [HttpGet]
        public ActionResult Index()
        {
            string user_id = HttpContext.GetOwinContext().Authentication.User.FindFirst(ClaimTypes.Sid).Value;
            int investor_id = 0;
            if (Session["Investor_Id"] == null)
            {
                return Redirect("/User/Index");
            }
            investor_id = int.Parse(Session["Investor_Id"].ToString());
            Models.InvestorIndexModel investorIndex = new Models.InvestorIndexModel(user_id, investor_id);
            return View(investorIndex);
        }

        // GET: Investor/Details/{int}
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            Models.InvestorDetailsModel investor = new Models.InvestorDetailsModel(id);
            return View(investor);
        }
    }
}