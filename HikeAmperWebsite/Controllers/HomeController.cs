using System.Web.Mvc;

namespace HikeAmperWebsite.Controllers
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