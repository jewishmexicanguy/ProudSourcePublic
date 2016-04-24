using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace ProudSourceBeta.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        /// <summary>
        /// Private struct used to pass authentication around in this object more easily. 
        /// </summary>
        private struct ProjectAuth
        {
            /// <summary>
            /// Accessor that holds the ID of the user that is logged in and viewing this project.
            /// </summary>
            public int User_ID { get; set; }
            /// <summary>
            /// Accessor that holds the ID of the user if they are logged in as an Entrepreneur.
            /// </summary>
            public int Entrepreneur_ID { get; set; }
            /// <summary>
            /// Accessor that holds the ID of the project in question, this is gathered from the HTTP request emanted from the client's browser.
            /// </summary>
            public int Project_ID { get; set; }
            /// <summary>
            /// Accessor that holds the ID of the user if they are logged in as an investor.
            /// </summary>
            public int Investor_ID { get; set; }
            /// <summary>
            /// Accessor that holds whether or not the User ID, Entrepreneur ID and Project ID are a related entity.
            /// </summary>
            public bool Valid { get; set; }
        }
        /// <summary>
        /// Private method that will fill our auth struct with values we need to guide controller actions.
        /// </summary>
        /// <param name="project_id">The id captured from the client's http request which is the project Id requested.</param>
        /// <returns>An authorization object containing Ids.</returns>
        private ProjectAuth check_clientRelation(int project_id)
        {
            ProjectAuth cred = new ProjectAuth();
            cred.Project_ID = project_id;
            // get our Investor_ID from Session if it exists.
            if (Session["Investor_ID"] != null)
                cred.Investor_ID = (int)Session["Investor_ID"];
            // get our Entrepreneur_ID from Session if it exists.
            if (Session["Entrepreneur_ID"] != null)
                cred.Entrepreneur_ID = (int)Session["Entrepreneur_ID"];
            else
                cred.Valid = false;
            Models.ProjectInitializeViewModel authModel = new Models.ProjectInitializeViewModel(User.Identity.GetUserId(), cred.Entrepreneur_ID, project_id);
            cred.User_ID = authModel.User_Id;
            cred.Valid = authModel.Valid;
            return cred;
        }
        /// <summary>
        /// Private method used to add/update the client's session variable for the project account they are logged in as.
        /// </summary>
        /// <param name="cred"></param>
        private void manageClient_Sessions(ProjectAuth cred)
        {
            int project_id = 0;
            if (Session["Project_ID"] != null)
                project_id = (int)Session["Project_ID"];

            if (project_id != 0)
            {
                if(project_id != cred.Project_ID)
                    Session["Project_ID"] = cred.Project_ID;
            }
            else if (project_id == 0)
                Session.Add("Project_ID", cred.Project_ID);
        }

        // GET: Project/Index/5
        /// <summary>
        /// This method will show project details to the project owner.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(int id)
        {
            ProjectAuth cred = check_clientRelation(id);
            if (!cred.Valid)
                return Redirect("/User/Index"); // relation not valid return to user index screen.
            manageClient_Sessions(cred);
            Models.ProjectIndexViewModel projectModel = new Models.ProjectIndexViewModel(User.Identity.GetUserId(), cred.Entrepreneur_ID, id);
            return View(projectModel);
        }

        // GET: Project/Details/5
        /// <summary>
        /// This method will show project details to floaters and people who do not own this Project account if the porject is public
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Details(int id)
        {
            Models.ProjectIndexViewModel projectAccount = new Models.ProjectIndexViewModel(id);
            projectAccount.display_Project_ID = id;
            if (User.Identity.IsAuthenticated)
                projectAccount.IsRegisteredViewer = true;
            else
                projectAccount.IsRegisteredViewer = false;
            projectAccount.get_ProjectData(id);
            if (projectAccount.Profile_Public)
                return View(projectAccount);
            else
            {
                /// in this way we will display a project profile to the owner of the project in case they want to see what the profile will look like before making it public.
                ProjectAuth cred = check_clientRelation(id);
                projectAccount.IsRegisteredViewer = false; /// do not display a post message field to the project owner.
                if (cred.Valid)
                {
                    Models.ProjectIndexViewModel ownerAccount = new Models.ProjectIndexViewModel(User.Identity.GetUserId(), cred.Entrepreneur_ID, cred.Project_ID);
                    manageClient_Sessions(cred);
                    ownerAccount.display_Project_ID = cred.Project_ID;
                    return View(ownerAccount);
                }
                else /// The client user is not the owner and this project is set to not be viewable.
                    return Redirect("/User/Index"); /// TODO: Replace this redirect to something more helpful, perhpas one of the serach pages once it is built.
            }
        }

        // POST: Project/Details/5
        /// <summary>
        /// This method will catch message posts that will be sent by registered users of the site.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Details(int id, string Project_Message)
        {
            ProjectAuth cred = check_clientRelation(id);
            Models.ProjectIndexViewModel detailsModel = new Models.ProjectIndexViewModel(id);
            detailsModel.get_ProjectData(cred.Project_ID);
            if (cred.Investor_ID != 0 || cred.Entrepreneur_ID != 0 || cred.User_ID != 0)
            {
                if (cred.Investor_ID != 0)
                    detailsModel.message_ProjectOwner(cred.Project_ID, cred.Investor_ID, 3, Project_Message);
                else if (cred.Entrepreneur_ID != 0)
                    detailsModel.message_ProjectOwner(cred.Project_ID, cred.Entrepreneur_ID, 4, Project_Message);
                else if (cred.User_ID != 0)
                    detailsModel.message_ProjectOwner(cred.Project_ID, cred.User_ID, 1, Project_Message);
            }
            return View(detailsModel);
        }

        // GET: Project/Edit/5
        /// <summary>
        /// The method gets the infoe that can be edited for this account that belongs to the project owner.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ProjectAuth cred = check_clientRelation(id);
            if (!cred.Valid)
                return Redirect("/User/Index");
            Models.ProjectEditViewModel projectModel = new Models.ProjectEditViewModel(User.Identity.GetUserId(), cred.Entrepreneur_ID, id);
            return View(projectModel);
        }

        // POST: Project/Edit/5
        /// <summary>
        /// The method that accepts incoming changes to a project owner's and updates the account.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, Models.ProjectEditViewModel updateModel)
        {
            ProjectAuth cred = check_clientRelation(id);
            if(!cred.Valid)
                return Redirect("/User/Index");
            try
            {
                Models.ProjectEditViewModel projectModel = new Models.ProjectEditViewModel(User.Identity.GetUserId(), cred.Entrepreneur_ID, id);
                projectModel.Name = updateModel.Name;
                projectModel.Description = updateModel.Description;
                projectModel.Investment_Goal = updateModel.Investment_Goal;
                projectModel.Profile_Public = updateModel.Profile_Public;
                projectModel.update_Project();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return View();
            }
            return Redirect(string.Format("/Project/Index/{0}", id));
        }

        // GET: Project/UploadImage/5
        /// <summary>
        /// This method exposes a facility to display a from that will allow the owner of this project to upload an image for this project.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UploadImage(int id)
        {
            ProjectAuth cred = check_clientRelation(id);
            if(!cred.Valid)
            {
                Redirect("/User/Index/");
            }
            Models.ProjectUploadImageViewModel updateModel = new Models.ProjectUploadImageViewModel(User.Identity.GetUserId(), cred.Entrepreneur_ID, cred.Project_ID);
            return View(updateModel);
        }

        // POST: Project/UploadImage/5
        /// <summary>
        /// This method exposes an method that allows the client user to actually post an image file that will be uploaded and associated to this project.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="upload_Image"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadImage(int id, HttpPostedFileBase upload_Image)
        {
            ProjectAuth cred = check_clientRelation(id);
            if (!cred.Valid)
            {
                Redirect("/User/Index/");
            }
            Models.ProjectUploadImageViewModel updateModel = new Models.ProjectUploadImageViewModel(User.Identity.GetUserId(), cred.Entrepreneur_ID, cred.Project_ID);
            try
            {
                // code to process a posted image file.
                byte[] picture_bytes = null;
                if (ModelState.IsValid)
                {
                    if (upload_Image != null)
                    {
                        if (upload_Image.ContentLength > 0)
                        {
                            int MaxContentLength = 1024 * 1024 * 3;
                            string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf" };

                            if (!AllowedFileExtensions.Contains(upload_Image.FileName.Substring(upload_Image.FileName.LastIndexOf('.'))))
                            {
                                ModelState.AddModelError("upload_Image", "Please only use file types: " + string.Join(", ", AllowedFileExtensions));
                            }
                            else if (upload_Image.ContentLength > MaxContentLength)
                            {
                                ModelState.AddModelError("upload_Image", string.Format("Your file is too large, maximum file size is: {0} Bytes.", MaxContentLength));
                            }
                            else
                            {
                                picture_bytes = new byte[upload_Image.ContentLength];
                                upload_Image.InputStream.Read(picture_bytes, 0, upload_Image.ContentLength);
                            }
                        }
                    }
                }

                updateModel.Profile_Image = picture_bytes;
                updateModel.upload_Image();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return View();
            }
            return Redirect(string.Format("/Project/Details/{0}", cred.Project_ID));
        }

        // GET: Project/RemoveImage/5
        /// <summary>
        /// This method exposes functionality to remove an image from a project account.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RemoveImage(int id)
        {
            // In order for this method to be called the client must be logged in as the project owner.
            if (Session["Project_ID"] == null)
                return Redirect("/User/Index");
            int project_id = (int)Session["Project_ID"];
            ProjectAuth cred = check_clientRelation(project_id);
            if (!cred.Valid)
                return Redirect("/User/Index");
            Models.ProjectDeleteImageViewModel modifyModel = new Models.ProjectDeleteImageViewModel(User.Identity.GetUserId(), cred.Entrepreneur_ID, project_id);
            modifyModel.Image_ID = id;
            modifyModel.get_Image();
            return View(modifyModel);
        }

        // POST: Project/RemoveImage/5
        /// <summary>
        /// This method is what acceptes the user confimration to remove an image from being associated with the project account in question.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="confirm_CheckBox"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveImage(int id, string confirm_CheckBox)
        {
            if (Session["Project_ID"] == null)
                throw new Exception("What the hell, the client just posted to remove an image without a project id in session.");
            int project_id = (int)Session["Project_ID"];
            ProjectAuth cred = check_clientRelation(project_id);
            if (!cred.Valid)
                return Redirect("/User/Index");
            Models.ProjectDeleteImageViewModel modifyModel = new Models.ProjectDeleteImageViewModel(User.Identity.GetUserId(), cred.Entrepreneur_ID, project_id);
            modifyModel.Image_ID = id;
            if(confirm_CheckBox == "on") // on is the equivalent of true from a checkbox element.
                modifyModel.delete_Image();
            return Redirect(string.Format("/Project/Details/{0}", project_id));
        }

        // GET: Project/UploadDocument/5
        /// <summary>
        /// Method that exposes facility to upload a document and relate it to this account.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UploadDocument(int id)
        {
            int project_id = id;
            ProjectAuth cred = check_clientRelation(project_id);
            if (!cred.Valid)
                return Redirect("/User/Index");
            Models.ProjectUploadDocumentViewModel uploadModel = new Models.ProjectUploadDocumentViewModel(User.Identity.GetUserId(), cred.Entrepreneur_ID, cred.Project_ID);
            return View(uploadModel);
        }

        // POST: Project/UploadDocument/5
        /// <summary>
        /// Method that actually uploads a posted document to the project account in question.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="upload_Document"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadDocument(int id, HttpPostedFileBase upload_Document)
        {
            ProjectAuth cred = check_clientRelation(id);
            if (!cred.Valid)
                return Redirect("/User/Index");
            Models.ProjectUploadDocumentViewModel uploadModel = new Models.ProjectUploadDocumentViewModel(User.Identity.GetUserId(), cred.Entrepreneur_ID, cred.Project_ID);
            uploadModel.File_Name = upload_Document.FileName;
            uploadModel.Mime_Type = upload_Document.ContentType;
            byte[] hold = new byte[upload_Document.ContentLength];
            upload_Document.InputStream.Read(hold, 0, upload_Document.ContentLength);
            uploadModel.Binary_File = hold;
            uploadModel.upload_file();
            return Redirect(string.Format("/Project/Details/{0}", id));
        }

        // GET: Project/RemoveDocument/5
        /// <summary>
        /// Exposes functionality to the user to remove documents uploaded to thia ccount.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RemoveDocument(int id)
        {
            // This method will need that the user be logged in as a project owner so we can get the project id from session.
            if (Session["Project_ID"] == null)
                return Redirect("/User/Index");
            int project_id = (int)Session["Project_ID"];
            ProjectAuth cred = check_clientRelation(project_id);
            if (!cred.Valid)
                return Redirect("/User/Index");
            Models.ProjectDeleteDocumentViewModel modifyModel = new Models.ProjectDeleteDocumentViewModel(User.Identity.GetUserId(), cred.Entrepreneur_ID, cred.Project_ID);
            modifyModel.Document_ID = id;
            modifyModel.get_DocumentName();
            return View(modifyModel);
        }

        // POST: Project/RemoveDocument/5
        /// <summary>
        /// Accepts the client's confirmation to remove a document from this account.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="confirm_CheckBox"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveDocument(int id, string confirm_CheckBox)
        {
            // This method will need that the user be logged in as a project owner so we can get the project id from session.
            if (Session["Project_ID"] == null)
                return Redirect("/User/Index");
            int project_id = (int)Session["Project_ID"];
            ProjectAuth cred = check_clientRelation(project_id);
            if (!cred.Valid)
                return Redirect("/User/Index");
            Models.ProjectDeleteDocumentViewModel modifyModel = new Models.ProjectDeleteDocumentViewModel(User.Identity.GetUserId(), cred.Entrepreneur_ID, cred.Project_ID);
            modifyModel.Document_ID = id;
            if (confirm_CheckBox == "on")
                modifyModel.delete_document();
            return Redirect(string.Format("/Project/Details/{0}", cred.Project_ID));
        }

        // GET: Project/CreatePROC/5
        [HttpGet]
        public ActionResult CreatePROC(int id)
        {
            ProjectAuth cred = check_clientRelation(id);
            Models.ProjectCreatePROCViewModel createPROC = new Models.ProjectCreatePROCViewModel();
            return View(createPROC);
        }

        // POST: Project/CreatePROC/5
        [HttpPost]
        public ActionResult CreatePROC(int id, Models.ProjectCreatePROCViewModel newPROC)
        {
            ProjectAuth cred = check_clientRelation(id);
            Models.ProjectCreatePROCViewModel createPROC = new Models.ProjectCreatePROCViewModel();
            // TODO: We need to validate this data, some one may POST data that is not good or designed to cause server exceptions.
            createPROC.Investor_ID = cred.Investor_ID;
            createPROC.Project_ID = id;
            createPROC.Performance_Begin_DateTime = newPROC.Performance_Begin_DateTime;
            createPROC.Performance_End_DateTime = newPROC.Performance_End_DateTime;
            createPROC.Revenue_Percentage = newPROC.Revenue_Percentage;
            createPROC.Investment_Amount = newPROC.Investment_Amount;
            int newPROC_id = 0;
            if (!cred.Valid) // this will fire if you are not the project owner.
                newPROC_id = createPROC.create_PROC();
            else
                return Redirect("/User/Index"); // TODO: do something else but this is good enough for now, if they are not authenticated this will kick the client all the way back to the home screen.
            
            return Redirect(string.Format("/PROC/Details/{0}", newPROC_id));
        }
        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            /// TODO: implement things to show before deleting here and a confirmation button to click.
            return View();
        }

        // POST: Project/Delete/5
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
