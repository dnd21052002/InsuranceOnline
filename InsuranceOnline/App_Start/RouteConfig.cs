using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InsuranceOnline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Cart",
                url: "gio-hang",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "InsuranceOnline.Controllers" }
            );

            routes.MapRoute(
                name: "Register",
                url: "dang-ky",
                defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
                namespaces: new[] { "InsuranceOnline.Controllers" }
            );

            routes.MapRoute(
                name: "Login",
                url: "dang-nhap",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "InsuranceOnline.Controllers" }
            );

            routes.MapRoute(
                name: "Logout",
                url: "dang-xuat",
                defaults: new { controller = "User", action = "Logout", id = UrlParameter.Optional },
                namespaces: new[] { "InsuranceOnline.Controllers" }
            );

            routes.MapRoute(
                name: "ProductCategory",
                url: "bao-hiem/{alias}-{id}",
                defaults: new { controller = "Product", action = "ProductCategory", id = UrlParameter.Optional },
                namespaces: new[] { "InsuranceOnline.Controllers" }
            );

            routes.MapRoute(
                name: "ProductByCate",
                url: "goi-bao-hiem/{alias}-{id}",
                defaults: new { controller = "Product", action = "ProductByCate", id = UrlParameter.Optional },
                namespaces: new[] { "InsuranceOnline.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "InsuranceOnline.Controllers" }
            );
        }
    }
}
