using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProudSourceBeta.Controllers
{
    public class DocumentController : Controller
    {
        // GET: Document/Download
        public ActionResult Download(int id)
        {
            Models.FileDownloadResult d = new Models.FileDownloadResult(id);
            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = d.fileName,
                Inline = false,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());
            return File(d.fileData, d.content_type);
        }
    }
}