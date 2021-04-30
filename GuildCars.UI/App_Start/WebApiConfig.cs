using System.Web.Http;
using System.Web.Http.Cors;

namespace GuildCars.UI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var corsSettings = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsSettings);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
               name: "InventoryController",
               routeTemplate: "api/{controller}/{action}/{searchTerm}/{minYear}/{maxYear}/{minPrice}/{maxPrice}",
               defaults: new
               {
                   searchTerm = RouteParameter.Optional,
                   minYear = RouteParameter.Optional,
                   maxYear = RouteParameter.Optional,
                   minPrice = RouteParameter.Optional,
                   maxPrice = RouteParameter.Optional
               }
               );

            config.Routes.MapHttpRoute(
              name: "DefaultApi",
              routeTemplate: "api/{controller}/{id}",
              defaults: new { id = RouteParameter.Optional }
          );
        }
    }
}
