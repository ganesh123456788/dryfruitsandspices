﻿using System;
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
<<<<<<< HEAD
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
=======
<<<<<<< HEAD
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
=======
<<<<<<< HEAD
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
=======
<<<<<<< HEAD
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
=======
                defaults: new { controller = "Home", action = "ddryfruitsandspices", id = UrlParameter.Optional }
>>>>>>> 8ba5cf9f26c3da9b84a089ecd20bdeb7ccfa61f1
>>>>>>> 54d77b7c45c4b7ef1f01ba38718b00b0a2655a7e
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
            );
        }
    }
}
