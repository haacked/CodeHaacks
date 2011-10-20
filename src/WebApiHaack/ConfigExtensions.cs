using System.Linq;
using Microsoft.ApplicationServer.Http;

namespace WebApiHaack
{
    public static class ConfigExtensions
    {
    public static void AppendAuthorizationRequestHandlers(this WebApiConfiguration config)
    {
        var requestHandlers = config.RequestHandlers;
        config.RequestHandlers = (c, e, od) =>
        {
            if (requestHandlers != null)
            {
                requestHandlers(c, e, od); // Original request handler
            }
            var authorizeAttribute = od.Attributes.OfType<RequireAuthorizationAttribute>().FirstOrDefault();
            if (authorizeAttribute != null)
            {
                c.Add(new AuthOperationHandler(authorizeAttribute));
            }
        };
    }
    }
}
