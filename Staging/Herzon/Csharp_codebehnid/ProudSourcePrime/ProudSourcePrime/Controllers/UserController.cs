using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace ProudSourcePrime.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Index()
        {
            Models.UserIndexModel userModel = new Models.UserIndexModel(HttpContext.GetOwinContext().Authentication.User.FindFirst(ClaimTypes.Sid).Value);
            Session.Add("Entrepreneur_Id", userModel.userIndexData.EntrepreneurProfile["Entrepreneur_Id"]);
            Session.Add("Investor_Id", userModel.userIndexData.InvestorProfile["Investor_Id"]);
            return View(userModel);
        }

        // POST: User
        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            Dictionary<string, string> objDict = new Dictionary<string, string>();
            foreach(string key in formCollection)
            {
                objDict.Add(key, formCollection.GetValue(key).AttemptedValue);
            }
            // This if statement will handle cases where the checkbox form fields fails to give us enough information to know when to update whaether a profile is public or not.
            //
            // It exploits the difference in checkboxes when true being "on" and the value that comes form our data base which is either "True" or "False" 
            if(objDict.ContainsKey("userIndexData.EntrepreneurProfile[Profile_Public]"))
            {
                if(objDict["userIndexData.EntrepreneurProfile[Profile_Public]"] == "True" || objDict["userIndexData.EntrepreneurProfile[Profile_Public]"] == "False")
                {
                    objDict.Remove("userIndexData.EntrepreneurProfile[Profile_Public]");
                }
            }
            else if(!objDict.ContainsKey("userIndexData.EntrepreneurProfile[Profile_Public]"))
            {
                objDict.Add("userIndexData.EntrepreneurProfile[Profile_Public]", "off");
            }
            // This if statement will handle cases where the checkbox form fields fails to give us enough information to know when to update whaether a profile is public or not.
            //
            // It exploits the difference in checkboxes when true being "on" and the value that comes form our data base which is either "True" or "False" 
            if (objDict.ContainsKey("userIndexData.InvestorProfile[Profile_Public]"))
            {
                if(objDict["userIndexData.InvestorProfile[Profile_Public]"] == "True" || objDict["userIndexData.InvestorProfile[Profile_Public]"] == "False")
                {
                    objDict.Remove("userIndexData.InvestorProfile[Profile_Public]");
                }
            }
            else if(!objDict.ContainsKey("userIndexData.InvestorProfile[Profile_Public]"))
            {
                objDict.Add("userIndexData.InvestorProfile[Profile_Public]", "off");
            }
            // validate phone number
            if(objDict.ContainsKey("userIndexData.UserProfile[PhoneNumber]"))
            {
                objDict["userIndexData.UserProfile[PhoneNumber]"] = objDict["userIndexData.UserProfile[PhoneNumber]"].Replace("-", "");
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"([a-zA-Z])");
                System.Text.RegularExpressions.Match match = regex.Match(objDict["userIndexData.UserProfile[PhoneNumber]"]);
                if(match.Success)
                {
                    ModelState.AddModelError("invalid_phonenumber", "Phone number was not properly formated, use only numbers");
                }
            }
            if (ModelState.Count > 0)
            {
                Models.UserIndexModel userModel = new Models.UserIndexModel(HttpContext.GetOwinContext().Authentication.User.FindFirst(ClaimTypes.Sid).Value);
                return View(userModel);
            }
            else
            {
                new ServiceReference1.Service1Client().update_ProfileData(objDict["userIndexData.UserProfile[Id]"].ToString(), objDict);
                return Redirect("/User/Index");
            }
        }
    }
}