using Excercise3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Excercise3.Controllers
{
    public class HomeController : Controller
    {
        private bool ValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
            // 1. single display
            // 2. animation display (infinite)
            // 3. animation display (for x seconds) [save]
            // 4. load an animation, and show it until it ends.
        }

        // GET: Dispaly
        public ActionResult NormalDisplay(string param1, int param2)
        {
            if (ValidateIPv4(param1))
            {
                ViewBag.ip = param1;
                ViewBag.port = param2;
                ViewBag.state = "single";
                return View("MapView");
            }

            ViewBag.filename = param1;
            ViewBag.freq = param2;
            return View("ExistingRoute");
        }

        // GET: Animation Dispaly
        public ActionResult AnimationDisplay(string ip, int port, uint freq)
        {
            ViewBag.state = "multiple";
            ViewBag.ip = ip;
            ViewBag.port = port;
            ViewBag.freq = freq;
            return View("MapView");
        }

        // GET: Dispaly
        public ActionResult SavePath(string ip, int port, uint freq, uint duration, string filename)
        {
            ViewBag.state = "save";
            ViewBag.ip = ip;
            ViewBag.port = port;
            ViewBag.freq = freq;
            ViewBag.duration = duration;
            ViewBag.filename = filename;
            return View("MapView");
        }

        [HttpGet]
        public string GetLocation()
        {
            string ip = Request["ip"];
            int port = int.Parse(Request["port"]);
            Location loc = InfoModel.GetLocation(ip, port);
            return loc.ToJSON();
        }

        [HttpPost]
        public ActionResult SavePlaneRoute()
        {
            // url?filename=newfile
            string filename = Request["filename"];
            // filename cannot contain slashes. and cannot contain double dots (.. turns to .) just security thing.
            // and filename cannot be empty.
            if (filename == null || filename.Length <= 0)
            {
                return new HttpUnauthorizedResult("Missing filename");
            }

            if (filename.Contains("..") || filename.Contains("/"))
            {
                return new HttpUnauthorizedResult("Invalid characters in filename");
            }
            // ~/RecordedFiles/../Scripts/DrawModule.js
            string filepath = Server.MapPath("~/RecordedFiles/" + filename);
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }

            Request.InputStream.Position = 0;
            using (var fileStream = System.IO.File.Create(filepath))
            {
                Request.InputStream.CopyTo(fileStream);
            }
            return new HttpStatusCodeResult(200);
        }

        [HttpGet]
        public string GetPlaneRoute()
        {
            // url?filename=newfile
            string filename = Request["filename"];
            // filename cannot contain slashes. and cannot contain double dots (.. turns to .) just security thing.
            // and filename cannot be empty.
            Response.ContentType = "text/plain";
            if (filename == null || filename.Length <= 0)
            {
                Response.StatusCode = 401;
                return "Missing filename";
            }
            if (filename.Contains("..") || filename.Contains("/"))
            {
                Response.StatusCode = 401;
                return "Invalid characters in filename";
            }

            // check that the file exist
            string filepath = Server.MapPath("~/RecordedFiles/" + filename);
            if (!System.IO.File.Exists(filepath))
            {
                Response.StatusCode = 404;
                return "File not found";
            }

            string fileContent;
            using (var streamReader = new StreamReader(filepath))
            {
                fileContent = streamReader.ReadToEnd();
            }
            Response.ContentType = "application/json";
            return fileContent;
        }
    }
}