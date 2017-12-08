using System.Web.Http;
using System.Web.Http.Cors;

namespace Insureme.WebApis
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // New code
            var cors = new EnableCorsAttribute("*", "*", "*"); //GET,POST,PUT,DELETE
            config.EnableCors(cors);

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
