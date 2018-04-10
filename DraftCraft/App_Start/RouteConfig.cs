using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DraftCraft
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ProizvodiCreate",
                url: "Proizvodi/Create",
                defaults: new {controller = "Proizvodi", action = "Create"}
                );

            routes.MapRoute(
                name: "ProizvodipoKategorijipoStranici",
                url: "Proizvodi/{kategorija}/Page{page}",
                defaults: new {controller = "Proizvodi", action = "Index"}
                );

            routes.MapRoute(
                name: "ProizvodipoStranici",
                url: "Proizvodi/Page{page}",
                defaults: new {controller = "Proizvodi", action = "Index"}
                );

            routes.MapRoute(
                name: "ProizvodipoKategoriji",
                url: "Proizvodi/{kategorija}",
                defaults: new {controller = "Proizvodi", action = "Index"}
                );

            routes.MapRoute(
                name: "ProizvodiIndex",
                url: "Proizvodi",
                defaults: new { controller = "Proizvodi", action = "Index"}
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
