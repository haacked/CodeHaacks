using Microsoft.ApplicationServer.Http;

namespace WebApiHaack.DemoWeb.AppStart
{
    public class CommentsConfiguration : WebApiConfiguration
    {
        public CommentsConfiguration()
        {
            EnableTestClient = true;

            RequestHandlers = (c, e, od) =>
            {
                // TODO: Configure request operation handlers
            };

            this.AppendAuthorizationRequestHandlers();
        }
    }
}