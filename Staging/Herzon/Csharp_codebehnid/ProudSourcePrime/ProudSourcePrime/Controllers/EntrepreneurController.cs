using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;

namespace ProudSourcePrime.Controllers
{
    [Authorize]
    public class EntrepreneurController : Controller
    {
        // GET: Entrepreneur/Index
        [HttpGet]
        public ActionResult Index()
        {
            if(Session["Entrepreneur_Id"] != null)
            {
                string user_id = HttpContext.GetOwinContext().Authentication.User.FindFirst(ClaimTypes.Sid).Value;
                int ent_id = int.Parse(Session["Entrepreneur_Id"].ToString());
                Models.EntrepreneurIndexModel entrepreneurModel = new Models.EntrepreneurIndexModel(user_id, ent_id);
                return View(entrepreneurModel);
            }
            else
            {
                return Redirect("/User/Index");
            }
        }

        // POST: Entrepreneur/Index
        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            Dictionary<string, string> objDict= new Dictionary<string, string>();
            foreach(string key in formCollection)
            {
                objDict.Add(key, formCollection.GetValue(key).AttemptedValue);
            }
            string user_id = HttpContext.GetOwinContext().Authentication.User.FindFirst(ClaimTypes.Sid).Value;
            int ent_id = int.Parse(Session["Entrepreneur_Id"].ToString());
            if (objDict.ContainsKey("Project_Name") && objDict.ContainsKey("Project_Description") && objDict.ContainsKey("Investment_Amount"))
            {
                int project_id = new ServiceReference1.Service1Client().create_Project(user_id, ent_id, objDict);
                return Redirect(string.Format("/Project/Index/{0}", project_id));
            }
            else
            {
                ModelState.AddModelError("Project_Create_error", new Exception("Failed to include a project name a project description or an investment ammount"));
                return View(new Models.EntrepreneurIndexModel(user_id, ent_id));
            }
        }

        // GET: Entrepreneur/Details/{int}
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            Models.EntrepreneurDetailsModel entrepreneur = new Models.EntrepreneurDetailsModel(id);
            return View(entrepreneur);
        }
    }
}