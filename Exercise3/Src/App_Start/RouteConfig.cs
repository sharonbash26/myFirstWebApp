using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Excercise3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "NormalDisplay",
                url: "display/{param1}/{param2}",
                defaults: new { controller = "Home", action = "NormalDisplay" }
            );

            routes.MapRoute(
                name: "AnimationDisplay",
                url: "display/{ip}/{port}/{freq}",
                defaults: new { controller = "Home", action = "AnimationDisplay" }
            );

            routes.MapRoute(
                name: "SavePath",
                url: "save/{ip}/{port}/{freq}/{duration}/{filename}",
                defaults: new { controller = "Home", action = "SavePath" }
            );

            routes.MapRoute(
                 name: "Default",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
             );
            
        }
    }
}
