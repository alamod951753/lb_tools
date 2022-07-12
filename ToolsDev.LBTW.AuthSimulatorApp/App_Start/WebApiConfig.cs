using System.Web.Http;

namespace ToolsDev.LBTW.AuthSimulatorApp
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務
            //config.Filters.Add(new AuthenticationAttribute());

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ControllerAndActionOnly",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { },
                constraints: new { action = @"^[a-zA-Z]+([\s][a-zA-Z]+)*$" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
