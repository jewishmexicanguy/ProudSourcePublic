using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProudSourcePrime.Controllers
{
    [Authorize]
    public class IOController : Controller
    {
        /// <summary>
        /// GET IO/Download/{document_Id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Download(int id)
        {
            Dictionary<string, string> documentDict = new Models.DocumentFileModel().DownloadFile(id);
            if (documentDict.Keys.Count > 0)
            {
                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = documentDict["File_Name"],
                    Inline = false,
                };
                Response.AppendHeader("Content-Disposition", cd.ToString());
                return File(Convert.FromBase64String(documentDict["Base64_Encoded_File"]), documentDict["Mime_Type"]);
            }
            else
            {
                return Redirect("/Error/404");
            }
        }

        [HttpPost]
        public JsonResult DeleteLink(int id)
        {
            bool status = false;
            string exception_message = string.Empty;
            string details = string.Empty;
            try
            {
                System.IO.Stream req = Request.InputStream;
                req.Seek(0, System.IO.SeekOrigin.Begin);
                string json = new System.IO.StreamReader(req).ReadToEnd();
                string[] parameters = json.Split(new string[] { "&" }, StringSplitOptions.None);
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                for (int i = 0; i < parameters.Count(); i++)
                {
                    string[] key_and_arg = parameters[i].Split(new string[] { "=" }, StringSplitOptions.None);
                    dictionary.Add(key_and_arg[0], key_and_arg[1]);
                }
                status = new ServiceReference1.Service1Client().delete_Link(id, int.Parse(dictionary["Profile_Id"]), int.Parse(dictionary["Profile_Type_Id"]));
            }
            catch (Exception e)
            {
                exception_message = e.Message;
                details = e.InnerException.Message;
            }
            if (status)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, code = exception_message, message = details });
            }
        }

        [HttpPost]
        public JsonResult DeleteImage()
        {
            bool status = false;
            string exception_message = string.Empty;
            string details = string.Empty;
            try
            {
                System.IO.Stream req = Request.InputStream;
                req.Seek(0, System.IO.SeekOrigin.Begin);
                string json = new System.IO.StreamReader(req).ReadToEnd();
                string[] parameters = json.Split(new string[] { "=" }, StringSplitOptions.None);
                if(parameters.Length == 2 && parameters[0] == "Image_Id")
                {
                    status = new ServiceReference1.Service1Client().delete_Image(int.Parse(parameters[1]));
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
                if(string.IsNullOrEmpty(exception_message))
                {
                    exception_message = "Failed to delete the image";
                }
                return Json(new { success = false, code = exception_message, message = details });
            }
        }

        [HttpPost]
        public JsonResult DeleteDocument()
        {
            bool status = false;
            string exception_message = string.Empty;
            string details = string.Empty;
            try
            {
                System.IO.Stream req = Request.InputStream;
                req.Seek(0, System.IO.SeekOrigin.Begin);
                string json = new System.IO.StreamReader(req).ReadToEnd();
                string[] parameters = json.Split(new string[] { "=" }, StringSplitOptions.None);
                if (parameters.Length == 2 && parameters[0] == "Document_Id")
                {
                    status = new ServiceReference1.Service1Client().delete_Document(int.Parse(parameters[1]));
                }
            }
            catch (Exception e)
            {
                exception_message = e.Message;
                details = e.InnerException.Message;
            }
            if (status)
            {
                return Json(new { success = true });
            }
            else
            {
                if (string.IsNullOrEmpty(exception_message))
                {
                    exception_message = "Failed to delete the document";
                }
                return Json(new { success = false, code = exception_message, message = details });
            }
        }

        [HttpPost]
        public ActionResult UploadImage(Models.ImageFileModel imageModel, HttpPostedFileBase image_upload)
        {
            string exception_message = string.Empty;
            string details = string.Empty;
            try
            {
                if (image_upload != null && image_upload.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 3;
                    string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf" };

                    if (!AllowedFileExtensions.Contains(image_upload.FileName.Substring(image_upload.FileName.LastIndexOf('.'))))
                    {
                        //ModelState.AddModelError("upload_Image", "Please only use file types: " + string.Join(", ", AllowedFileExtensions));
                        exception_message += "Please only use file types: " + string.Join(", ", AllowedFileExtensions);
                        details += "File was type: " + image_upload.FileName.Substring(image_upload.FileName.LastIndexOf('.'));
                    }
                    else if (image_upload.ContentLength > MaxContentLength)
                    {
                        //ModelState.AddModelError("upload_Image", string.Format("Your file is too large, maximum file size is: {0} Bytes.", MaxContentLength));
                        exception_message += string.Format("Your file is too large, maximum file size is: {0} Bytes.", MaxContentLength);
                        details += "File is too large";
                    }
                    else
                    {
                        byte [] picture_bytes = new byte[image_upload.ContentLength];
                        image_upload.InputStream.Read(picture_bytes, 0, image_upload.ContentLength);
                        imageModel.ImageData = picture_bytes;
                        imageModel.UploadImage();
                    }
                }
            }
            catch (Exception e)
            {

            }
            if (!string.IsNullOrEmpty(exception_message))
            {
                return Json(new { success = false, code = exception_message, message = details });
            }
            else
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
        }

        [HttpPost]
        public ActionResult UpdateImage(Models.ImageFileModel imageModel, HttpPostedFileBase image_update)
        {
            try
            {
                if (image_update != null && image_update.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 3;
                    string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf" };

                    if (!AllowedFileExtensions.Contains(image_update.FileName.Substring(image_update.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("update_Image", "Please only use file types: " + string.Join(", ", AllowedFileExtensions));
                    }
                    else if (image_update.ContentLength > MaxContentLength)
                    {
                        ModelState.AddModelError("update_Image", string.Format("Your file is too large, maximum file size is: {0} Bytes.", MaxContentLength));
                    }
                    else
                    {
                        byte[] picture_bytes = new byte[image_update.ContentLength];
                        image_update.InputStream.Read(picture_bytes, 0, image_update.ContentLength);
                        imageModel.ImageData = picture_bytes;
                        imageModel.UpdateImage();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult UploadDocument(Models.DocumentFileModel documentModel, HttpPostedFileBase document_upload)
        {
            try
            {
                byte[] hold = new byte[document_upload.ContentLength];
                document_upload.InputStream.Read(hold, 0, document_upload.ContentLength);
                documentModel.FileName = document_upload.FileName;
                documentModel.MimeType = document_upload.ContentType;
                documentModel.DocumentData = hold;
                documentModel.UploadFile();
            }
            catch (Exception e)
            {

            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult UpdateDocument(Models.DocumentFileModel documentModel, HttpPostedFileBase document_update)
        {
            try
            {
                byte[] hold = new byte[document_update.ContentLength];
                document_update.InputStream.Read(hold, 0, document_update.ContentLength);
                documentModel.FileName = document_update.FileName;
                documentModel.MimeType = document_update.ContentType;
                documentModel.DocumentData = hold;
                documentModel.UpdateFile();
            }
            catch (Exception e)
            {

            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult UploadLink(Models.LinkModel linkModel, FormCollection link_upload)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (string key in link_upload)
            {
                if (key == "SelectedOption")
                {
                    dictionary.Add("Link_Type", link_upload[key]);
                }
                else
                {
                    dictionary.Add(key, link_upload[key]);
                }
            }
            string errors = string.Empty;
            if(!dictionary.ContainsKey("Profile_Id"))
            {
                errors += "Invalid Profile Identity,";
            }
            if(!dictionary.ContainsKey("Is_Project"))
            {
                errors += " Boolean field is null,";
            }
            if(!dictionary.ContainsKey("Link"))
            {
                errors += " Link is null,";
            }
            if(!dictionary.ContainsKey("Link_Type"))
            {
                errors += " Please choose a link type";
            }
            if (errors != string.Empty)
            {
                return Json(new { success = false, message = errors });
            }
            else
            {
                new ServiceReference1.Service1Client().upload_Link(dictionary);
                return Redirect(Request.UrlReferrer.ToString());
            }
        }

        // This controller method will allow images be loaded from our data base without having to imbed images into the page.
        [HttpGet]
        [AllowAnonymous]
        public void GetImage(int id)
        {
            string base64image = new ServiceReference1.Service1Client().get_Image(id);
            if(string.IsNullOrEmpty(base64image))
            {
                //return new HttpNotFoundResult();
            }
            else
            {
                byte[] arr = Convert.FromBase64String(base64image);
                System.Drawing.Bitmap bi = new System.Drawing.Bitmap(new System.IO.MemoryStream(arr));
                System.Drawing.Bitmap trg = new System.Drawing.Bitmap(bi.Width, bi.Height);
                using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(trg))
                {
                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                    graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    graphics.DrawImage(bi, 0, 0, bi.Width, bi.Height);
                    using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                    {
                        trg.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        Response.BinaryWrite(memoryStream.GetBuffer());
                    }
                }
                Response.Flush();
                Response.End();
            }
        }
    }
}