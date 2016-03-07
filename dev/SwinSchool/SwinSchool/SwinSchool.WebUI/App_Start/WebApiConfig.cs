using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SwinSchool.WebUI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // only support Json Formater
            if (config.Formatters.Contains(config.Formatters.XmlFormatter))
                config.Formatters.Remove(config.Formatters.XmlFormatter);
            if (!config.Formatters.Contains(config.Formatters.JsonFormatter))
                config.Formatters.Add(config.Formatters.JsonFormatter);
        }
    }
}
