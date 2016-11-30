using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProudSourcePrime.Controllers
{
    [AllowAnonymous]
    public class SearchController : Controller
    {
        // GET: Search/Results/{KeyArg}
        [HttpGet]
        public ActionResult Results()
        {
            return View();
        }

        // This is meant for Jquery
        // POST: Search/Results     
        [HttpPost]
        public JsonResult JsonSearch()
        {
            bool status = false;
            string exception_message = string.Empty;
            string details = string.Empty;
            Dictionary<string, string>[] results = new Dictionary<string, string>[0];
            try
            {
                System.IO.Stream req = Request.InputStream;
                req.Seek(0, System.IO.SeekOrigin.Begin);
                string[] KeyValue = new System.IO.StreamReader(req).ReadToEnd().Split(new string[] { "=" }, StringSplitOptions.None);
                string key = KeyValue[0];
                string arg = KeyValue[1];
                results = new ServiceReference1.Service1Client().ajax_SearchResults(arg);
                status = true;
            }
            catch (Exception e)
            {
                exception_message = e.Message;
                details = e.InnerException.Message;
            }
            if (status)
            {
                return Json(new { success = true, response = results });
            }
            else
            {
                return Json(new { success = false, code = exception_message, message = details });
            }
        }
    }
}