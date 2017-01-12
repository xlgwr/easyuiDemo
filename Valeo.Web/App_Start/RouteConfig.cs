using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Valeo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Globalization", // 路由名称
                "{lang}/{controller}/{action}/{id}", // 带有参数的 URL
                new { lang = "zh", controller = "Login", action = "Index", id = UrlParameter.Optional }, // 参数默认值
                new { lang = "^[a-zA-Z]{2}(-[a-zA-Z]{2})?$" }    //参数约束
            );

            routes.MapRoute(
                            name: "Default",
                            url: "{controller}/{action}/{id}",
                            defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
                        );
            routes.MapRoute(
                          name: "Default2",
                          url: "{controller}/{action}/{id}/{id2}",
                          defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional, id2 = UrlParameter.Optional }
                      );
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}