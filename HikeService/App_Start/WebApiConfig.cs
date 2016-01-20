using System.Web.Http;

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
                routeTemplate: "hikes/{userName}",
                defaults: new { controller = "hikes" }
            );

            config.Routes.MapHttpRoute(
                name: "UsersApi",
                routeTemplate: "user/{userName}",
                defaults: new { controller = "user" }
            );
        }
    }
}