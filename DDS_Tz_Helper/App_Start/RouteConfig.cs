﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DDS_Tz_Helper
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "NewHome", id = UrlParameter.Optional }
                //defaults: new { controller = "Home", action = "GateReport", id = UrlParameter.Optional }
            );
        }
    }
}
