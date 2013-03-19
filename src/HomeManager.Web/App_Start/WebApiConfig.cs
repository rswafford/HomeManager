using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using HomeManager.Web.Handlers;
using WebApiDoodle.Web.MessageHandlers;

namespace HomeManager.Web.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new RequireHttpsMessageHandler());
            config.MessageHandlers.Add(new HomeManagerAuthHandler());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{key}",
                defaults: new { key = RouteParameter.Optional }
            );
        }
    }
}