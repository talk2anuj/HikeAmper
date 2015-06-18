using System.Web.Http;
using HikeService.StorageModule;

namespace HikeService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "HikesApi",
                routeTemplate: "hikes/{user}",
                defaults: new { controller = "hikes", type = Constants.HikesType }
            );					
		}
    }
}
