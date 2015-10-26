using System;
using System.Web.Mvc;

namespace HikeAmperWebsite.Controllers
{
    public class HomeController : Controller
    {
        [RequireHttps]
        public ActionResult Index()
        {
            if (String.IsNullOrEmpty(User.Identity.Name))
            {
                return View("Login");
            }
            return View("Index");
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}