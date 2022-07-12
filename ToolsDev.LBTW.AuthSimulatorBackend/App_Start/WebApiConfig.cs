using System.Web.Http;
using ToolsDev.LBTW.AuthSimulatorBackend.Filters;

namespace ToolsDev.LBTW.AuthSimulatorBackend
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務
            config.Filters.Add(new AuthenticationAttribute());

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = RouteParameter.Optional, id = RouteParameter.Optional }
            );
        }
    }
}
