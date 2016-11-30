using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ProudSourcePrime.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        // GET: Project/Index/{project_id}
        [HttpGet]
        public ActionResult Index(int id)
        {
            string user_id = HttpContext.GetOwinContext().Authentication.User.FindFirst(ClaimTypes.Sid).Value;
            int entrepreneur_id = 0;
            if(Session["Entrepreneur_Id"] == null)
            {
                return Redirect("/User/Index");
            }
            entrepreneur_id = int.Parse(Session["Entrepreneur_Id"].ToString());
            Models.ProjectIndexModel projectModel = new Models.ProjectIndexModel(user_id, entrepreneur_id, id);
            return View(projectModel);
        }

        // POST: Project/Index/{project_id}
        [HttpPost]
        public ActionResult Index(int id, FormCollection formCollection)
        {
            string user_id = HttpContext.GetOwinContext().Authentication.User.FindFirst(ClaimTypes.Sid).Value;
            int entrepreneur_id = 0;
            if (Session["Entrepreneur_Id"] == null)
            {
                return Redirect("/User/Index");
            }
            entrepreneur_id = int.Parse(Session["Entrepreneur_Id"].ToString());
            Dictionary<string, string> objDict = new Dictionary<string, string>();
            foreach (string key in formCollection)
            {
                objDict.Add(key, formCollection.GetValue(key).AttemptedValue);
            }
            // Fix the check box
            //
            // this work around is needed because when the user de checks the value on the check box it just does not push any value that we can work with.
            if (objDict.ContainsKey("Project_Public"))
            {
                if (objDict["Project_Public"] == "True" || objDict["Project_Public"] == "False")
                {
                    objDict.Remove("Project_Public");
                }
            }
            else if (!objDict.ContainsKey("Project_Public"))
            {
                objDict.Add("Project_Public", "off");
            }
            new ServiceReference1.Service1Client().update_ProjectProfileData(user_id, entrepreneur_id, id, objDict);
            return Redirect(string.Format("/Project/Index/{0}", id));
        }

        // GET: Project/Details/{0}
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            Models.ProjectDetailsModel project = new Models.ProjectDetailsModel(id);
            if (project.project.ProjectDetails == null)
            {
                return new HttpNotFoundResult("The project you requested does not exist or is not public");
            }
            else
            {
                return View(project);
            }
        }

        // POST: Project/Details/{0}
        [HttpPost]
        public ActionResult Details(int id, FormCollection formCollection)
        {
            Dictionary<string, string> objDictionary = new Dictionary<string, string>();
            Models.ProjectDetailsModel project = new Models.ProjectDetailsModel(id);
            foreach(string key in formCollection)
            {
                objDictionary.Add(key, formCollection.GetValue(key).AttemptedValue);
            }

            // make sure revenue exists
            if (!string.IsNullOrEmpty(objDictionary["Revenue_Percentage"]))
            {
                // make sure revenue is positive and non-zero
                if (decimal.Parse(objDictionary["Revenue_Percentage"]) <= 0)
                {
                    ModelState.AddModelError("Negative_Revenue_Return", "Please specify a positive non-zero value for revenue return");
                }
            }
            else
            {
                ModelState.AddModelError("Revenue_Return_NULL", "Please enter a revenue percentage to purchase with this PROC");
            }

            // make sure investment exists
            if (!string.IsNullOrEmpty(objDictionary["Investment_Amount"]))
            {
                // make sure investment is positive and nom-zero
                if (decimal.Parse(objDictionary["Investment_Amount"]) <= 0)
                {
                    ModelState.AddModelError("Negative_Investment", "Please enter a positive non-zero value for investment");
                }
            }
            else
            {
                ModelState.AddModelError("Investment_Amount_NULL", "Please enter a value for investment in this PROC");
            }

            // make sure we have a begin date
            if(string.IsNullOrEmpty(objDictionary["date_begin"]))
            {
                ModelState.AddModelError("begin_date_NULL", "please enter a start date");
            }

            // make sure we have an end date
            if(string.IsNullOrEmpty(objDictionary["date_end"]))
            {
                ModelState.AddModelError("end_date_NULL", "please enter an end date");
            }

            // make sure the dates make sense
            if (!string.IsNullOrEmpty(objDictionary["date_begin"]) && !string.IsNullOrEmpty(objDictionary["date_end"]))
            {
                if (DateTime.Parse(objDictionary["date_begin"]) > DateTime.Parse(objDictionary["date_end"]))
                {
                    ModelState.AddModelError("invalid_dates", "Please make sure the start date is before the end date for the PROC aggrement");
                }
            }

            // if no errors are present then submit the PROC
            if(!ModelState.Values.Any(x => x.Errors.Count > 0))
            {
                int new_PROC_Id = new ServiceReference1.Service1Client().create_PROC(
                    HttpContext.GetOwinContext().Authentication.User.FindFirst(ClaimTypes.Sid).Value,
                    //int.Parse(objDictionary["Investor_Id"]), 
                    int.Parse(Session["Investor_Id"].ToString()),
                    int.Parse(objDictionary["Project_Id"]),
                    objDictionary
                    );
                return Redirect(string.Format("/PROC/Details/{0}", new_PROC_Id));
            }
            else
            {
                return View(project);
            }
        }
    }
}