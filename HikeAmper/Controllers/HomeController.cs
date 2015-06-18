using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HikeAmper.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("MyHikeDetails", "Hike");
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}