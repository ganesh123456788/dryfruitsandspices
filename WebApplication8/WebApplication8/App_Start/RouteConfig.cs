using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication8
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
<<<<<<< HEAD
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
=======
                defaults: new { controller = "Home", action = "ddryfruitsandspices", id = UrlParameter.Optional }
>>>>>>> 8ba5cf9f26c3da9b84a089ecd20bdeb7ccfa61f1
            );
        }
    }
}
