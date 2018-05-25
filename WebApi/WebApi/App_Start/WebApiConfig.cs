using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes(); 
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                 routeTemplate: "{controller}/{action}",
                defaults: new { }
                //routeTemplate: "api/{controller}/{action}/{id}",
                //defaults: new { id = RouteParameter.Optional }
            );
           // config.Routes.MapHttpRoute(
           //    name: "User",
           //    routeTemplate: "{controller}/{action}/",
           //    defaults: new { controller = "User", action = "Login"}
           //);
           // // config.Routes.MapHttpRoute(
           // //    name: "User",
           // //    routeTemplate: "{controller}/{action}/{name}",
           // //    defaults: new { controller = "User", action = "GetFarmbrief", name = "yugeng" }
           // //);
           // config.Routes.MapHttpRoute(
           //    name: "Fields",
           //    routeTemplate: "{controller}/{action}/",
           //    defaults: new { controller = "Fields", action = "GetFields", farmid = 1 }
           // );
        }
    }
}
