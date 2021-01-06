using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace W1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // untuk mengatur data yang dikirim dari WEB api ke Client hanya dalam Format JSON saja.
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            // Web API routes
            config.MapHttpAttributeRoutes();


            //config.Routes.MapHttpRoute(
            //    name: "Route1Paramaters",
            //    routeTemplate: "Radsoft/{controller}/{param1}/{param2}/{param3}",
            //    defaults: new { param1 = RouteParameter.Optional, param2 = RouteParameter.Optional, param3 = RouteParameter.Optional }
            //);
            //config.Routes.MapHttpRoute(
            //    name: "Route2Paramaters",
            //    routeTemplate: "Radsoft/{controller}/{action}/{param1}/{param2}/{param3}/{param4}",
            //    defaults: new { param1 = RouteParameter.Optional, param2 = RouteParameter.Optional, param3 = RouteParameter.Optional, param4 = RouteParameter.Optional}
            //);
            config.Routes.MapHttpRoute(
                name: "Route3Paramaters",
                routeTemplate: "Radsoft/{controller}/{action}/{param1}/{param2}/{param3}/{param4}/{param5}/{param6}/{param7}/{param8}",
                defaults: new { param1 = RouteParameter.Optional, param2 = RouteParameter.Optional, param3 = RouteParameter.Optional, param4 = RouteParameter.Optional, param5 = RouteParameter.Optional, param6 = RouteParameter.Optional, param7 = RouteParameter.Optional, param8 = RouteParameter.Optional }
            );
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "Radsoft/{controller}/{param1}/{param2}",
            //    defaults: new { param1 = RouteParameter.Optional, param2 = RouteParameter.Optional }
            //);

        }
    }
}
