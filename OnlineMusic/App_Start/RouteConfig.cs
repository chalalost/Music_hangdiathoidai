using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineMusic
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*routes.MapRoute(
                name: "Blog Detail",
                url: "blog/{metatitle}-{id}",
                defaults: new { controller = "Blog1", action = "Blog_Detail", id = UrlParameter.Optional }
            );*/
            routes.MapRoute(
                name: "Register",
                url: "dang-ky",
                defaults: new { controller = "Customer", action = "Register", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "LogIn",
               url: "dang-nhap",
               defaults: new { controller = "Customer", action = "LogIn", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "LogOut",
               url: "thoat",
               defaults: new { controller = "Customer", action = "LogOut", id = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Seo Product",
                url: "product",
                defaults: new { controller = "Product1", action = "Product", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Seo Cart",
                url: "cart",
                defaults: new { controller = "Cart", action = "Cart", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Payment",
                url: "payment",
                defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Seo About",
                url: "about",
                defaults: new { controller = "Main", action = "About", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Seo Contact",
                url: "contact",
                defaults: new { controller = "Main", action = "Contact", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Seo Blog",
                url: "blog",
                defaults: new { controller = "Main", action = "Blog", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Seo Blog Detail",
                url: "blog-id",
                defaults: new { controller = "Blog1", action = "Blog_Detail", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Product Detail",
                url: "chitiet/{metatitle}-{id}",
                defaults: new { controller = "Product1", action = "Product_Detail", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Search",
                url: "tim-kiem",
                defaults: new { controller = "Product1", action = "Search", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Add To Cart",
                url: "them-vao-gio",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "PayPal",
                url: "paypal",
                defaults: new { controller = "Cart", action = "PaymentWithPayPal", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Success",
                url: "hoan-thanh",
                defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Main", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
