using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Contacts.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "pages",
                url: "ContactInfo/Index/{page}/{sort}/{type}",
                defaults: new { controller = "ContactInfo", action = "Index",
                                page = UrlParameter.Optional,
                                sort = UrlParameter.Optional,
                                type = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "ContactInfo", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
