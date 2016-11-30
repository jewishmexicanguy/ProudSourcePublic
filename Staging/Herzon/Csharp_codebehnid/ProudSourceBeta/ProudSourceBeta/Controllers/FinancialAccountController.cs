using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProudSourceAccountingLibrary.BrainTree;

namespace ProudSourceBeta.Controllers
{
    [Authorize]
    public class FinancialAccountController : Controller
    {
        // GET: FinancialAccount
        [HttpGet]
        public ActionResult Index(int id)
        {
            Models.FinancialAccountDetails model = new Models.FinancialAccountDetails(id);
            ViewBag.Message = null;
            return View(model);
        }

        // POST: FinancialAccount
        [HttpPost]
        public ActionResult Index(int id, FormCollection collection)
        {
            if(collection.AllKeys.Contains("Amount"))
            {
                if (!string.IsNullOrEmpty(collection.Get("Amount")))
                {
                    if (collection.AllKeys.Contains("payment_method_nonce"))
                    {
                        BTInboundPayment payment = new BTInboundPayment(collection.Get("payment_method_nonce"), decimal.Parse(collection.Get("Amount")), id, "BrainTree");
                    }
                    else
                    {
                        ViewBag.Message = "An error occured while processing your request.";
                    }
                }
                else
                {
                    ViewBag.Message = "Please put in a value to be processed";
                }
            }
            Models.FinancialAccountDetails model = new Models.FinancialAccountDetails(id);
            return View(model);
        }
    }
}
