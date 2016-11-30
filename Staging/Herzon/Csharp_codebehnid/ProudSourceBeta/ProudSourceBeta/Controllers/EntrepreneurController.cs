using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace ProudSourceBeta.Controllers
{
    [Authorize]
    public class EntrepreneurController : Controller
    {
        /// <summary>
        /// Private struct to make it easy to pass authentication around in this class.
        /// </summary>
        private struct EntrepreneurAuth
        {
            public bool Valid { get; set; }
            public int User_Id { get; set; }
            public int Entrepreneur_ID { get; set; }
        }

        // GET: Entrepreneur/Index/{entrepreneur_id}
        [HttpGet]
        public ActionResult Index()
        {
            EntrepreneurAuth cred = check_clientRelation();
            if (!cred.Valid)
            {
                return Redirect(@"/User/Index");
            }
            manageClient_Sessions(cred);
            Models.EntrepreneurIndexViewModel entrepreneurAccount = new Models.EntrepreneurIndexViewModel(cred.Entrepreneur_ID, User.Identity.GetUserId());
            return View(entrepreneurAccount);
        }

        [HttpGet]
        public ActionResult Projects()
        {
            EntrepreneurAuth cred = check_clientRelation();
            if (!cred.Valid)
            {
                return Redirect(@"/User/Index");
            }
            manageClient_Sessions(cred);
            Models.EntrepreneurIndexViewModel entrepreneurAccount = new Models.EntrepreneurIndexViewModel(cred.Entrepreneur_ID, User.Identity.GetUserId());
            return View(entrepreneurAccount);
        }

        // GET: Entrepreneur/Edit/{entrepreneur_id}
        [HttpGet]
        public ActionResult Edit()
        {
            EntrepreneurAuth cred = check_clientRelation();
            if(!cred.Valid)
            {
                return Redirect(@"/User/Index");
            }
            manageClient_Sessions(cred);
            Models.EntrepreneurEditViewModel entrepreneurAccount = new Models.EntrepreneurEditViewModel(cred.Entrepreneur_ID, User.Identity.GetUserId());
            return View(entrepreneurAccount);
        }

        // POST: Entrepreneur/Edit/{entrepreneur_id}
        [HttpPost]
        public ActionResult Edit(Models.EntrepreneurEditViewModel updateModel, HttpPostedFileBase update_Profile_Image)
        {
            EntrepreneurAuth cred = check_clientRelation();
            if (!cred.Valid)
            {
                return Redirect("/User/Index");
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

                Models.EntrepreneurEditViewModel authModel = new Models.EntrepreneurEditViewModel(cred.Entrepreneur_ID, User.Identity.GetUserId());
                authModel.Name = updateModel.Name;
                authModel.Profile_Public = updateModel.Profile_Public;
                authModel.Profile_Picture = picture_bytes;
                authModel.updateEntrepreneur();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return View();
            }
            return Redirect("/User/Index/");
        }

        // GET: Entrepreneur/createProject/{enrepreneur_id}
        [HttpGet]
        public ActionResult createProject()
        {
            EntrepreneurAuth cred = check_clientRelation();
            if(!cred.Valid)
            {
                Redirect("/User/Index"); // kick out of entrepreneur control to the user control.
            }
            manageClient_Sessions(cred);
            Models.EntrepreneurCreateProjectViewModel entrepreneurAccount = new Models.EntrepreneurCreateProjectViewModel(cred.Entrepreneur_ID, User.Identity.GetUserId());
            return View(entrepreneurAccount);
        }

        // POST: Entrepreneur/createProject/{entrepreneur_id}
        [HttpPost]
        public ActionResult createProject(Models.EntrepreneurCreateProjectViewModel newProject)
        {
            EntrepreneurAuth cred = check_clientRelation();
            if(!cred.Valid)
            {
                Redirect("/User/Index"); // kick out of entrepreneur control to the user control.
            }
            Models.EntrepreneurCreateProjectViewModel authenticated_newProject = new Models.EntrepreneurCreateProjectViewModel(cred.Entrepreneur_ID, User.Identity.GetUserId());
            authenticated_newProject.Name = newProject.Name;
            authenticated_newProject.Description = newProject.Description;
            authenticated_newProject.Investment_Goal = newProject.Investment_Goal;
            int newPRoject_Id = authenticated_newProject.createProject();
            if(newPRoject_Id == 0)
            {
                // TODO: return error to client user on page.
                ModelState.AddModelError("", "Project account not created.");
                return View(newProject);
            }
            return Redirect(string.Format("/Project/Index/{0}", newPRoject_Id));
        }

        /// <summary>
        /// Method that displays data for clients who are not this account owner.
        /// </summary>
        /// <returns></returns>
        // GET: Entrepreneur/Details/{entrepreneur_id}
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            Models.EntrepreneurIndexViewModel entrepreneurModel;
            if (User.Identity.GetUserId() != null)
            {
                SessionUserAccessor session = get_SessionIDs();
                if (session.Entrepreneur_ID > 0)
                {
                    entrepreneurModel = new Models.EntrepreneurIndexViewModel(id, User.Identity.GetUserId(), session.Entrepreneur_ID);
                    if (entrepreneurModel.Profile_Public)
                        return View(entrepreneurModel);
                    else
                        return Redirect("/Search/Index");
                }
                else if (session.Investor_ID > 0)
                {
                    entrepreneurModel = new Models.EntrepreneurIndexViewModel(id, User.Identity.GetUserId(), session.Investor_ID);
                    if (entrepreneurModel.Profile_Public)
                        return View(entrepreneurModel);
                    else
                        return Redirect("/Search/Index");
                }
            }
            else
            {
                entrepreneurModel = new Models.EntrepreneurIndexViewModel();
                entrepreneurModel._get_EntrepreneurData(id);
                if (entrepreneurModel.Profile_Public)
                    return View(entrepreneurModel);
            }
            return Redirect("/Search/Index");
        }

        /// <summary>
        /// Method that accepts messages and likes from clients who are not the account owner.
        /// </summary>
        /// <param name="placeholder"></param>
        /// <returns></returns>
        // POST: Entrepreneur/Details/{entrepreneur_id}
        [HttpPost]
        [Authorize]
        public ActionResult Details(int id, string Entrepreneur_Message)
        {
            Models.EntrepreneurIndexViewModel entrepreneurModel = new Models.EntrepreneurIndexViewModel(id, User.Identity.GetUserId());
            SessionUserAccessor session = get_SessionIDs();
            if (session.Entrepreneur_ID > 0)
                entrepreneurModel.message_Entrepreneur(id, session.Entrepreneur_ID, 4, Entrepreneur_Message);
            else if (session.Investor_ID > 0)
                entrepreneurModel.message_Entrepreneur(id, session.Investor_ID, 3, Entrepreneur_Message);
            else
                entrepreneurModel.message_Entrepreneur(id, entrepreneurModel.User_Id, 1, Entrepreneur_Message);
            return View(entrepreneurModel);
        }

        /// <summary>
        /// Private method that checks to make sure client user is actually related to this account.
        /// </summary>
        /// <returns></returns>
        private EntrepreneurAuth check_clientRelation()
        {
            EntrepreneurAuth cred = new EntrepreneurAuth { };
            string[] holding = Request.RawUrl.Split(new char[] { '/' }).ToArray<string>();
            int Entrepreneur_Id = 0;
            try
            {
                Entrepreneur_Id = Convert.ToInt32(holding[holding.Count<string>() - 1]);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("No Entrepreneur Id parameter was passed in.");
                cred.Valid = false;
                return cred;
            }

            // Get user id from application user id.
            Models.EntrepreneurInitialize authModel = new Models.EntrepreneurInitialize(Entrepreneur_Id, User.Identity.GetUserId());
            cred.Valid = authModel.valid;
            cred.User_Id = authModel.User_Id;
            cred.Entrepreneur_ID = authModel.Entrepreneur_ID;
            return cred;
        }

        /// <summary>
        /// Private method that manages user client session objects when steping onto pages within this Entrepreneur control.
        /// </summary>
        /// <param name="cred"></param>
        private void manageClient_Sessions(EntrepreneurAuth cred)
        {
            int getSession = 0;

            if (Session["Entrepreneur_ID"] != null)
                getSession = (int)Session["Entrepreneur_ID"];

            if (getSession == 0)
            {
                Session.Add("Entrepreneur_ID", cred.Entrepreneur_ID);
            }
            else
            {
                if (getSession != cred.Entrepreneur_ID)
                {
                    Session["Entrepreneur_ID"] = cred.Entrepreneur_ID;
                }
                // else it is the same and does not need to change
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
            if(Session["Entrepreneur_ID"] != null)
                session.Entrepreneur_ID = (int)Session["Entrepreneur_ID"];
            if (Session["Investor_ID"] != null)
                session.Investor_ID = (int)Session["Investor_ID"];
            return session;
        }
    }
}