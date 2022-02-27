using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                name: "Home",
                url:"",
                defaults: new { controller="Home",action="Index"});

            routes.MapRoute(
                name: "Category",
                url:"{CategoryName}",
                defaults: new { controller="Home", action= "CategoryPostList", CategoryName=UrlParameter.Optional
                });

            routes.MapRoute(
                name:"PostDetail",
                url:"{CategoryName}/{SeoLink}/{ID}",
                defaults: new { controller="Home", action= "PostDetail", CategoryName=UrlParameter.Optional, SeoLink=UrlParameter.Optional, ID=UrlParameter.Optional}
                );
           
        }
    }
}
