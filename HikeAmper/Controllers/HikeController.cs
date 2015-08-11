using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using CommonModels.Hike;
using HikeAmper.Models;
using Newtonsoft.Json;

namespace HikeAmper.Controllers
{
    [RequireHttps]
    public class HikeController : Controller
    {
        public ActionResult Add(FormCollection form)
        {
            if (!String.IsNullOrEmpty(User.Identity.Name))
            {
                var userName = getUserName(User.Identity.Name);
                WebRequest request =
                    WebRequest.Create("http://hikeservice.azurewebsites.net/hikes/" + userName);
                request.Method = "POST";
                request.ContentType = "application/json";
                var writer = new StreamWriter(request.GetRequestStream());
                var hikeUrl = form["hikeUrl"];
                var data = "{\"Value\":\"" + hikeUrl + "\"";
                writer.Write(data);
                writer.Flush();
                request.GetResponse();
                return RedirectToAction("MyHikeDetails", "Hike");
            }
            else
            {
                return View("Login");
            }
        }

        public ActionResult Delete(FormCollection form)
        {
            if (!String.IsNullOrEmpty(User.Identity.Name))
            {
                var userName = getUserName(User.Identity.Name);
                WebRequest request =
                    WebRequest.Create("http://hikeservice.azurewebsites.net/hikes/" + userName);
                request.Method = "DELETE";
                request.ContentType = "application/json";
                var writer = new StreamWriter(request.GetRequestStream());
                var hikeUrl = form["hikeUrl"];
                var data = "{\"Value\":\"" + hikeUrl + "\"";
                writer.Write(data);
                writer.Flush();
                request.GetResponse();
                return RedirectToAction("MyHikeDetails", "Hike");
            }
            return View("Login");
        }

        public ActionResult MyHikeDetails(FormCollection form)
        {
            if (!String.IsNullOrEmpty(User.Identity.Name))
            {
                var userName = getUserName(User.Identity.Name);
                WebRequest request =
                    WebRequest.Create("http://hikeservice.azurewebsites.net/hikes/" + userName);
                request.Method = "GET";
                request.ContentType = "application/json";
                var response = request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());
                var responseData = reader.ReadToEnd();
                HikeSummary[] myHikesData = JsonConvert.DeserializeObject<HikeSummary[]>(responseData);
                return View("../Home/Index", myHikesData.ToList());
            }
            return View("Login");
        }

        public class UserData
        {
            public string value;
        }

        private string getUserName(string name)
        {
            var regExp = "[^a-zA-Z0-9]";
            return Regex.Replace(name, regExp, "");
        }
    }
}