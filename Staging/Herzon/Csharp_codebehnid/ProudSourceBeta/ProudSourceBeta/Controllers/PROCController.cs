using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace ProudSourceBeta.Controllers
{
    public class PROCController : Controller
    {
        private struct PROCAuth
        {
            public int Entrepreneur_ID { get; set; }

            public int Investor_ID { get; set; }

            public int Project_ID { get; set; }

            public int PROC_ID { get; set; }

            public bool IsEntrepreneurOwner { get; set; }

            public bool IsInvestorOwner { get; set; }

            public bool IsRegisteredUser { get; set; }

            public bool IsFloater { get; set; }
        }

        private PROCAuth check_ClientRelation(int PROC_ID)
        {
            PROCAuth cred = new PROCAuth();
            cred.PROC_ID = PROC_ID;
            if (Session["Entrepreneur_ID"] != null)
                cred.Entrepreneur_ID = (int)Session["Entrepreneur_ID"];

            if (Session["Investor_ID"] != null)
                cred.Investor_ID = (int)Session["Investor_ID"];

            if (Session["Project_ID"] != null)
                cred.Project_ID = (int)Session["Project_ID"];

            if(cred.Entrepreneur_ID != 0 && cred.Project_ID != 0)
                cred.IsEntrepreneurOwner = new Models.PROCEntrepreneurViewModel(User.Identity.GetUserId(), cred.Entrepreneur_ID, cred.Project_ID, cred.PROC_ID).Valid;
            if (!cred.IsEntrepreneurOwner)
            {
                if (cred.Investor_ID != 0)
                    cred.IsInvestorOwner = new Models.PROCInvestorViewModel(User.Identity.GetUserId(), cred.Investor_ID, cred.PROC_ID).Valid;
            }

            cred.IsRegisteredUser = User.Identity.IsAuthenticated;

            if (!cred.IsEntrepreneurOwner)
            {
                if (!cred.IsInvestorOwner)
                {
                    if (!cred.IsRegisteredUser)
                    {
                        cred.IsFloater = true;
                    }
                }
            }
            else
            {
                cred.IsFloater = false;
            }
            return cred;
        }

        // GET: PROC/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////
            // we need to be able to tell here what kind of a user this is. There are a few possible outcomes.
            //      1 : The Entrepreneur owner of the Project this PROC is tied to.
            //      2 : The Investor owner of this PROC.
            //      ------------------------------------------------------------------------------------------
            //      3 : A logged in user who is neither the Investor owner or the Entrepreneur owner.
            //      4 : An anonymous person who is viewing the PROC
            //////////////////////////////////////////////////////////////////////////////////////////////////
            // Athenticate using our private authentication method.
            PROCAuth cred = check_ClientRelation(id);
            Models.PROCInitializeViewModel model;
            if (cred.IsEntrepreneurOwner)
                model = new Models.PROCEntrepreneurViewModel(User.Identity.GetUserId(), cred.Entrepreneur_ID, cred.PROC_ID, cred.PROC_ID);
            else if (cred.IsInvestorOwner)
                model = new Models.PROCInvestorViewModel(User.Identity.GetUserId(), cred.Investor_ID, cred.PROC_ID);
            else
            {
                model = new Models.PROCFloaterViewModel(id);
            }
            return View(model);
        }

        // POST: PROC/Details/5
        [HttpPost]
        public ActionResult Details(int id, FormCollection collection) 
        {
            // we need to know right away whether it was an entrepreneur or an investor that just posted, we will do that by looking for a specific form name from our form collection.
            string[] keys = collection.AllKeys;
            if(keys.Contains("Investor_Accepted"))
            {
                // we know the model to update with is an entrepreneur model.
                PROCAuth cred = check_ClientRelation(id);
                Models.PROCInvestorViewModel updateModel = new Models.PROCInvestorViewModel(User.Identity.GetUserId(), cred.Investor_ID, cred.PROC_ID);

                decimal investmentAmmount = 0.0m;
                investmentAmmount = decimal.Parse(collection.Get("Investment_Amount"));
                updateModel.Investment_ammount = investmentAmmount;

                bool investorAccepted = false;
                if (collection.Get("Investor_Accepted") == "on")
                    investorAccepted = true;
                else
                    investorAccepted = false;
                updateModel.Investor_Accepted = investorAccepted;

                DateTime termsBeginDate = DateTime.MinValue;
                termsBeginDate = DateTime.Parse(collection.Get("Performance_BeginDate"));
                updateModel.Performance_BeginDate = termsBeginDate;

                DateTime termsEndDate = DateTime.MinValue;
                termsEndDate = DateTime.Parse(collection.Get("Performance_EndDate"));
                updateModel.Performance_EndDate = termsEndDate;

                decimal revenuePercentage = decimal.Parse(collection.Get("Revenue_Percentage"));
                updateModel.Revenue_Percentage = revenuePercentage;

                if (updateModel.Valid)
                    updateModel.update_PROC();
            }
            else if (keys.Contains("Project_Accepted"))
            {
                // we know the model to update is with an investor model.
                PROCAuth cred = check_ClientRelation(id);
                Models.PROCEntrepreneurViewModel updateModel = new Models.PROCEntrepreneurViewModel(User.Identity.GetUserId(), cred.Entrepreneur_ID, cred.Project_ID, cred.PROC_ID);
                bool projectAccepted = false;
                if (collection.Get("Project_Accepted") == "on")
                    projectAccepted = true;
                else
                    projectAccepted = false;
                updateModel.Project_Accepted = projectAccepted;

                DateTime termsBeginDate = DateTime.MinValue;
                termsBeginDate = DateTime.Parse(collection.Get("Performance_BeginDate"));
                updateModel.Performance_BeginDate = termsBeginDate;

                DateTime termsEndDate = DateTime.MinValue;
                termsEndDate = DateTime.Parse(collection.Get("Performance_EndDate"));
                updateModel.Performance_EndDate = termsEndDate;

                decimal revenuePercentage = decimal.Parse(collection.Get("Revenue_Percentage"));
                updateModel.Revenue_Percentage = revenuePercentage;

                if (updateModel.Valid)
                    updateModel.update_PROC();
            }
            else
            {
                return Redirect("/403"); // who knows what just happend the client just asked for something that they are not allowed to do. TODO : redirect to a custom error page.
            }
            return Redirect(string.Format("/PROC/Details/{0}", id));
        }

        // GET: PROC/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PROC/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
