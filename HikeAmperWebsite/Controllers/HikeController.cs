using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using CommonModels.Hike;
using Newtonsoft.Json;

namespace HikeAmperWebsite.Controllers
{
    [RequireHttps]
    public class HikeController : Controller
    {
        public ActionResult Add(FormCollection form)
        {
            if (String.IsNullOrEmpty(User.Identity.Name))
            {
                return View("Login");
            }
            var userName = GetUserName(User.Identity.Name);
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

        public ActionResult Delete(FormCollection form)
        {
            if (String.IsNullOrEmpty(User.Identity.Name))
            {
                return View("Login");
            }
            var userName = GetUserName(User.Identity.Name);
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

        public ActionResult MyHikeDetails(FormCollection form)
        {
            if (String.IsNullOrEmpty(User.Identity.Name))
            {
                return View("Login");
            }
            var userName = GetUserName(User.Identity.Name);
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

        public class UserData
        {
            public string Value;
        }

        private string GetUserName(string name)
        {
            var regExp = "[^a-zA-Z0-9]";
            return Regex.Replace(name, regExp, "");
        }
    }
}