using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using Microsoft.ApplicationServer.Http.Dispatcher;

namespace WebApiHaack
{
    public class AuthOperationHandler : HttpOperationHandler<HttpRequestMessage, HttpRequestMessage>
    {
        RequireAuthorizationAttribute _authorizeAttribute;

        public AuthOperationHandler(RequireAuthorizationAttribute authorizeAttribute)
            : base("response")
        {
            _authorizeAttribute = authorizeAttribute;
        }

        protected override HttpRequestMessage OnHandle(HttpRequestMessage input)
        {
            IPrincipal user = HttpContext.Current.User;
            if (!user.Identity.IsAuthenticated)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (_authorizeAttribute.Roles == null)
            {
                return input;
            }

            var roles = _authorizeAttribute.Roles.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (roles.Any(role => user.IsInRole(role)))
            {
                return input;
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
    }
}
