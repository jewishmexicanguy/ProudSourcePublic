using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace ProudSourceBeta.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // GET: User/Index
        [HttpGet]
        public ActionResult Index()
        {
            Models.UserIndexViewModel userAccount = new Models.UserIndexViewModel(User.Identity.GetUserId());
            Session.Add("User_ID", userAccount.User_Id);
            Session.Add("Entrepreneur_ID", userAccount.Entrepreneur_ID);
            Session.Add("Investor_ID", userAccount.Investor_ID);
            return View(userAccount);
        }

        // GET: User/Edit
        [HttpGet]
        public ActionResult Edit()
        {
            Models.UserEditViewModel userAccount = new Models.UserEditViewModel(User.Identity.GetUserId());
            return View(userAccount);
        }

        // POST: User/Edit
        [HttpPost]
        public ActionResult Edit(Models.UserEditViewModel updateModel, HttpPostedFileBase update_Profile_Image)
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

            // code to update user account
            Models.UserEditViewModel user_to_update = new Models.UserEditViewModel(User.Identity.GetUserId());
            Models.UserEditViewModel updateUser = new Models.UserEditViewModel() { User_Email = updateModel.User_Email, User_Image = picture_bytes, Image_Id = user_to_update.Image_Id };
            updateUser.updateUser(user_to_update.User_Id);
            return Redirect("/User/Index");
        }

        //// GET: User/createEntrepreneur
        //[HttpGet]
        //public ActionResult createEntrepreneur()
        //{
        //    Models.UserCreateEntrepreneurViewModel userAccount = new Models.UserCreateEntrepreneurViewModel(User.Identity.GetUserId());
        //    return View(userAccount);
        //}

        //// POST: User/createEntrepreneur
        //[HttpPost]
        //public ActionResult createEntrepreneur(Models.UserCreateEntrepreneurViewModel newAccount)
        //{
        //    Models.UserCreateEntrepreneurViewModel clientUser = new Models.UserCreateEntrepreneurViewModel(User.Identity.GetUserId());
        //    int EntrepreneurId = newAccount.createEntrepreneur(clientUser.User_Id);
        //    return Redirect(string.Format("/Entrepreneur/Index/{0}", EntrepreneurId));
        //}

        //// GET: User/createInvestor
        //[HttpGet]
        //public ActionResult createInvestor()
        //{
        //    Models.UserCreateInvestorViewModel userAccount = new Models.UserCreateInvestorViewModel(User.Identity.GetUserId());
        //    return View(userAccount);
        //}

        //// POST user/createInvestor
        //[HttpPost]
        //public ActionResult createInvestor(Models.UserCreateInvestorViewModel newAccount)
        //{
        //    Models.UserCreateInvestorViewModel clientUser = new Models.UserCreateInvestorViewModel(User.Identity.GetUserId());
        //    int investorId = newAccount.createInvestor(clientUser.User_Id);
        //    /// TODO: for some reason the url we give back to the client is faster than what will be served up to display the new investor account that has been created.
        //    /// Proved it because using the same url that causes the 404 in the browser works a half second after the error appears.
        //    return View(string.Format("/Investor/Index/{0}", investorId));
        //}
    }
}