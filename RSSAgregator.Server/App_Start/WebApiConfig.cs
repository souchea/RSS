using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;



namespace RSSAgregator.Server
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Add",
                routeTemplate: "api/Category/Add/{userid}/{param}",
                defaults: new {action= "Add", controller= "Category" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
