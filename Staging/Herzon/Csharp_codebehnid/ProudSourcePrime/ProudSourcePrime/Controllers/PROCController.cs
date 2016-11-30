using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProudSourcePrime.Controllers
{
    [AllowAnonymous]
    public class PROCController : Controller
    {
        // GET: PROC/Details/{int}
        [HttpGet]
        public ActionResult Details(int id)
        {
            int entrepreneur_id = 0;
            int investor_id = 0;
            if(Session["Entrepreneur_Id"] != null)
            {
                entrepreneur_id = int.Parse(Session["Entrepreneur_Id"].ToString());
            }
            if(Session["Investor_Id"] != null)
            {
                investor_id = int.Parse(Session["Investor_Id"].ToString());
            }
            Models.PROCDetailsModel proc = new Models.PROCDetailsModel(id, entrepreneur_id, investor_id);
            return View(proc);
        }

        // POST: PROC/Details/{int}
        [HttpPost]
        [Authorize]
        public ActionResult Details(int id, FormCollection formCollection)
        {
            int entrepreneur_id = 0;
            int investor_id = 0;
            if (Session["Entrepreneur_Id"] != null)
            {
                entrepreneur_id = int.Parse(Session["Entrepreneur_Id"].ToString());
            }
            if (Session["Investor_Id"] != null)
            {
                investor_id = int.Parse(Session["Investor_Id"].ToString());
            }
            Dictionary<string, string> objDict = new Dictionary<string, string>();
            foreach(string key in formCollection)
            {
                objDict.Add(key, formCollection.GetValue(key).AttemptedValue);
            }

            // Hackey fix for the acceptance of the proc
            if(objDict.ContainsKey("User_Accepts_PROC"))
            {
                if(objDict["User_Accepts_PROC"] == "True" || objDict["User_Accepts_PROC"] == "False")
                {
                    objDict.Remove("User_Accepts_PROC");
                }
            }
            else if(!objDict.ContainsKey("User_Accepts_PROC"))
            {
                objDict.Add("User_Accepts_PROC", "off");
            }
            if (new ServiceReference1.Service1Client().update_PROC(id, objDict, entrepreneur_id, investor_id))
            {
                return Redirect(string.Format("/PROC/Details/{0}", id));
            }
            else
            {
                // do something i guess.
                return Redirect(string.Format("/PROC/Details/{0}", id));
            }
        }

        // This is meant for Jquery
        [HttpPost]
        [Authorize]
        public JsonResult accepted_by_User()
        {
            bool status = false;
            string exception_message = string.Empty;
            string details = string.Empty;
            int entrepreneur_id = 0;
            int investor_id = 0;
            int proc_id = 0;
            if (Session["Entrepreneur_Id"] != null)
            {
                entrepreneur_id = int.Parse(Session["Entrepreneur_Id"].ToString());
            }
            if (Session["Investor_Id"] != null)
            {
                investor_id = int.Parse(Session["Investor_Id"].ToString());
            }
            try
            {
                System.IO.Stream req = Request.InputStream;
                req.Seek(0, System.IO.SeekOrigin.Begin);
                string[] KeyValues = new System.IO.StreamReader(req).ReadToEnd().Split(new string[] { "&" }, StringSplitOptions.None);
                List<string[]> KeyValues_List = new List<string[]>();
                for (int i = 0; i < KeyValues.Length; i++)
                {
                    KeyValues_List.Add(KeyValues[i].Split(new string[] { "=" }, StringSplitOptions.None));
                }
                if (KeyValues_List.Exists(x => x[0] == "PROC_Id"))
                {
                    proc_id = int.Parse(KeyValues_List.Find(x => x[0] == "PROC_Id")[1]);
                }
                Dictionary<string, string> objDict = new Dictionary<string, string>();
                if (KeyValues_List.Exists(x => x[0] == "User_Accepts_PROC"))
                {
                    if (KeyValues_List.Find(x => x[0] == "User_Accepts_PROC")[1] == "true")
                    {
                        objDict.Add("User_Accepts_PROC", "on");
                    }
                    else if (KeyValues_List.Find(x => x[0] == "User_Accepts_PROC")[1] == "false")
                    {
                        objDict.Add("User_Accepts_PROC", "off");
                    }
                }
                status = new ServiceReference1.Service1Client().update_PROC(proc_id, objDict, entrepreneur_id, investor_id);
            }
            catch (Exception e)
            {
                ;
            }
            if(status)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, code = "failure to update PROC", message = "update failed" });
            }
        }

        // This is meant for Jquery
        // POST: PROC/mutualyAcceptPROC
        [HttpPost]
        [Authorize]
        public JsonResult mutualyAcceptPROC()
        {
            bool status = false;
            bool? investor_mutually_accepts = null;
            string exception_message = string.Empty;
            string details = string.Empty;
            int entrepreneur_id = 0;
            int investor_id = 0;
            int proc_id = 0;
            if (Session["Entrepreneur_Id"] != null)
            {
                entrepreneur_id = int.Parse(Session["Entrepreneur_Id"].ToString());
            }
            if (Session["Investor_Id"] != null)
            {
                investor_id = int.Parse(Session["Investor_Id"].ToString());
            }
            try
            {
                System.IO.Stream req = Request.InputStream;
                req.Seek(0, System.IO.SeekOrigin.Begin);
                string[] KeyValues = new System.IO.StreamReader(req).ReadToEnd().Split(new string[] { "&" }, StringSplitOptions.None);
                List<string[]> KeyValues_List = new List<string[]>();
                for (int i = 0; i < KeyValues.Length; i++)
                {
                    KeyValues_List.Add(KeyValues[i].Split(new string[] { "=" }, StringSplitOptions.None));
                }
                if(KeyValues_List.Exists(x => x[0] == "PROC_Id"))
                {
                    proc_id = int.Parse(KeyValues_List.Find(x => x[0] == "PROC_Id")[1]);
                }
                if(KeyValues_List.Exists(x => x[0] == "Investor_Mutually_Accepts"))
                {
                    if(KeyValues_List.Find(x => x[0] == "Investor_Mutually_Accepts")[1] == "on")
                    {
                        investor_mutually_accepts = true;
                    }
                    else if(KeyValues_List.Find(x => x[0] == "Investor_Mutually_Accepts")[1] == "off")
                    {
                        investor_mutually_accepts = false;
                    }
                }
                if (investor_mutually_accepts == true && entrepreneur_id != 0 && investor_id != 0 && proc_id != 0)
                {
                    status = new ServiceReference1.Service1Client().alter_PROC_MutualyAccepted(proc_id, (bool)investor_mutually_accepts, entrepreneur_id, investor_id);
                }
                else if(investor_mutually_accepts == false && entrepreneur_id != 0 && investor_id != 0 && proc_id != 0)
                {
                    status = new ServiceReference1.Service1Client().recant_PROC_MutualAcceptance(proc_id, false, entrepreneur_id, investor_id);
                }
                else
                {
                    exception_message += "Failed to request update.";
                    if (investor_mutually_accepts == null)
                    {
                        details += "Investor request to have PROC mutually accepted was invalid.";
                    }
                    if (entrepreneur_id == 0)
                    {
                        details += "Entrepreneur_Id value is invalid.";
                    }
                    if (investor_id == 0)
                    {
                        details += "Investor_Id value is invalid";
                    }
                    if (proc_id == 0)
                    {
                        details += "PROC_Id value is invalid";
                    }
                }
            }
            catch (Exception e)
            {
                exception_message = e.Message;
                details = e.InnerException.Message;
            }
            if(status)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, code = exception_message, message = details });
            }
        }
    }
}