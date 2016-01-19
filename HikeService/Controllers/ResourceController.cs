using System.Web;
using System.Web.Http;

namespace HikeService.Controllers
{
    public class ResourceController : ApiController
    {
        public void Options()
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "POST, GET, DELETE, OPTIONS");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type");
        }
    }
}