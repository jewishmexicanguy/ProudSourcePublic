using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ProudSourcePrime.Controllers
{
    [Authorize]
    public class FinancialController : Controller
    {
        // GET: Financial/Dashboard
        [HttpGet]
        public ActionResult Dashboard(int id)
        {
            Models.FinancialAccountModel account = new Models.FinancialAccountModel(HttpContext.GetOwinContext().Authentication.User.FindFirst(ClaimTypes.Sid).Value, id);
            return View(account);
        }

        // POST : Finacial/Dashboard
        [HttpPost]
        public ActionResult Dashboard(int id, FormCollection formCollection)
        {
            int investor_id = 0;
            if (Session["Investor_Id"] != null)
            {
                investor_id = int.Parse(Session["Investor_Id"].ToString());
            }
            Dictionary<string, string> objDict = new Dictionary<string, string>();
            foreach (string key in formCollection)
            {
                objDict.Add(key, formCollection.GetValue(key).AttemptedValue);
            }
            new ProudSourceAccountingLibrary.ProudSourceBrainTree(id).make_InboundPayment(objDict["payment_method_nonce"], decimal.Parse(objDict["Amount"]), id, "BrainTree"); 
            return Redirect(string.Format("/Financial/Dashboard/{0}", id));
        }
    }
}