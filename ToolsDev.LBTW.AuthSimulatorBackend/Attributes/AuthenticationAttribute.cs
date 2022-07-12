using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ToolsDev.Service.Service;

namespace ToolsDev.LBTW.AuthSimulatorBackend.Filters
{
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            if (context == null)
            {
                context.Response = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
                return;
            }

            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            if (authorization == null)
            {
                context.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                return;
            }

            if (authorization.Scheme != "Bearer")
            {
                context.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                return;
            }
            
            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                context.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                return;
            }

            if (!TokenService.IsValidToken(authorization.Parameter))
            {
                context.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                return;
            }
        }
    }
}