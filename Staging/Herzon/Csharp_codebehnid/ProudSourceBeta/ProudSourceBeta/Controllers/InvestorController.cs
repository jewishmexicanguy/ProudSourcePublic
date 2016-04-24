using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace ProudSourceBeta.Controllers
{
    [Authorize]
    public class InvestorController : Controller
    {
        /// <summary>
        /// Private struct to facilitate passing around authenticationin this object.
        /// </summary>
        private struct InvestorAuth
        {
            public bool Valid { get; set; }
            public int User_Id { get; set; }
            public int Investor_Id { get; set; }
        }

        // GET: Investor/Index/{investor_Id}
        [HttpGet]
        public ActionResult Index()
        {
            InvestorAuth cred = check_clientRelation();
            if (!cred.Valid)
            {
                return Redirect(@"/User/Index/");
            }
            manageClient_Sessions(cred);
            Models.InvestorIndexViewModel investorAccount = new Models.InvestorIndexViewModel(User.Identity.GetUserId(), cred.Investor_Id);
            return View(investorAccount);
        }

        // GET: Investor/Edit/{investor_Id}
        [HttpGet]
        public ActionResult Edit()
        {
            InvestorAuth cred = check_clientRelation();
            if (!cred.Valid)
            {
                return Redirect("@/User/Index/");
            }
            manageClient_Sessions(cred);
            Models.InvestorEditViewModel investorAccount = new Models.InvestorEditViewModel(User.Identity.GetUserId(), cred.Investor_Id);
            return View(investorAccount);
        }

        // POST: Investor/Edit/{investor_Id}
        [HttpPost]
        public ActionResult Edit(Models.InvestorEditViewModel updateModel, HttpPostedFileBase update_Profile_Image)
        {
            InvestorAuth cred = check_clientRelation();
            if (!cred.Valid)
            {
                return Redirect("@/User/Index/");
            }
            try
            {
                // code to process a posted image file.
                byte[] picture_bytes = null;
                if (ModelState.IsValid)
                {
                    if (update_Profile_Image != null)
                    {
                        if (update_Profile_Image.ContentLength > 0)
                        {
                            int MaxContentLength = 1024 * 1024 * 3;
                            string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf" };

                            if (!AllowedFileExtensions.Contains(update_Profile_Image.FileName.Substring(update_Profile_Image.FileName.LastIndexOf('.'))))
                            {
                                ModelState.AddModelError("profile_picture", "Please only use file types: " + string.Join(", ", AllowedFileExtensions));
                            }
                            else if (update_Profile_Image.ContentLength > MaxContentLength)
                            {
                                ModelState.AddModelError("profile_picture", string.Format("Your file is too large, maximum file size is: {0} Bytes.", MaxContentLength));
                            }
                            else
                            {
                                picture_bytes = new byte[update_Profile_Image.ContentLength];
                                update_Profile_Image.InputStream.Read(picture_bytes, 0, update_Profile_Image.ContentLength);
                            }
                        }
                    }
                }

                Models.InvestorEditViewModel authModel = new Models.InvestorEditViewModel(User.Identity.GetUserId(), cred.Investor_Id);
                authModel.Name = updateModel.Name;
                authModel.Profile_Public = updateModel.Profile_Public;
                authModel.Profile_Picture = picture_bytes;
                authModel.updateInvestor();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return View();
            }
            return Redirect("/User/Index");
        }

        /// <summary>
        ///  Method that displays data suitable for view by clients who are not this account owner.
        /// </summary>
        /// <returns></returns>
        // GET: Investor/Details/{investor_id}
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            Models.InvestorIndexViewModel investorModel;
            if (User.Identity.GetUserId() != null)
            {
                SessionUserAccessor session = get_SessionIDs();
                if (session.Entrepreneur_ID > 0)
                {
                    investorModel = new Models.InvestorIndexViewModel(User.Identity.GetUserId(), id, session.Entrepreneur_ID);
                    if (investorModel.Profile_Public)
                        return View(investorModel);
                    else
                        return Redirect("/Search/Index");
                }
                else if (session.Investor_ID > 0)
                {
                    investorModel = new Models.InvestorIndexViewModel(User.Identity.GetUserId(), id, session.Investor_ID);
                    if (investorModel.Profile_Public)
                        return View(investorModel);
                    else
                        return Redirect("/Search/Index");
                }
            }
            else
            {
                investorModel = new Models.InvestorIndexViewModel();
                investorModel.get_InvestorData(id);
                if (investorModel.Profile_Public)
                    return View(investorModel);
            }
                return Redirect("/Search/Index");
        }

        /// <summary>
        /// Method that accepts message posts and like votes from clients who are not the owner of this account.
        /// </summary>
        /// <param name="placeholder"></param>
        /// <returns></returns>
        // POST: Investor/Details/{investor_id}
        [HttpPost]
        [Authorize]
        public ActionResult Details(int id, string Investor_Message)
        {
            Models.InvestorIndexViewModel investorModel = new Models.InvestorIndexViewModel(User.Identity.GetUserId(), id);
            SessionUserAccessor session = get_SessionIDs();
            if (session.Entrepreneur_ID > 0)
                investorModel.message_Investor(id, session.Entrepreneur_ID, 4, Investor_Message);
            else if (session.Investor_ID > 0)
                investorModel.message_Investor(id, session.Investor_ID, 3, Investor_Message);
            else
                investorModel.message_Investor(id, investorModel.User_Id, 1, Investor_Message);
            return View(investorModel);
        }

        // GET: Investor/Delete/{investor_Id}
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Investor/Delete/{investor_Id}
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

        /// <summary>
        /// Private method that authenticates whether or not the user and investor are related.
        /// </summary>
        /// <returns></returns>
        private InvestorAuth check_clientRelation()
        {
            InvestorAuth cred = new InvestorAuth();
            // we have to get the investor id from the url pattern for this page.
            int investor_id = 0;
            string[] holding = Request.RawUrl.Split(new char[] { '/' });
            try
            {
                investor_id = Convert.ToInt32(holding[holding.Count<string>() - 1]);
            }
            catch 
            {
                System.Diagnostics.Debug.WriteLine("No Investor Id parameter was passed in.");
            }
            Models.InvestorIndexViewModel authModel = new Models.InvestorIndexViewModel(User.Identity.GetUserId(), investor_id);
            cred.Valid = authModel.Valid;
            cred.Investor_Id = investor_id;
            cred.User_Id = authModel.User_Id;
            return cred;
        }

        /// <summary>
        /// Private method that sets the client's session IDs for what investor account they are presently signes in as.
        /// </summary>
        /// <param name="cred"></param>
        private void manageClient_Sessions(InvestorAuth cred)
        {
            int getSession = 0;
            if (Session["Investor_ID"] != null)
                getSession = (int)Session["Investor_ID"];

            if (getSession == 0 )
            {
                Session.Add("Investor_ID", cred.Investor_Id);
            }
            else
            {
                if (getSession != cred.Investor_Id)
                {
                    Session["Investor_ID"] = cred.Investor_Id;
                }
                // else it is equal and nothing needs to be done.
            }
        }

        private struct SessionUserAccessor
        {
            public int Entrepreneur_ID { get; set; }
            public int Investor_ID { get; set; }
        }

        private SessionUserAccessor get_SessionIDs()
        {
            SessionUserAccessor session = new SessionUserAccessor();
            if (Session["Entrepreneur_ID"] != null)
                session.Entrepreneur_ID = (int)Session["Entrepreneur_ID"];
            if (Session["Investor_ID"] != null)
                session.Investor_ID = (int)Session["Investor_ID"];
            return session;
        }
    }
}
