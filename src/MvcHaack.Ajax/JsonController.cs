using System;
using System.Linq;
using System.Web.Mvc;

namespace MvcHaack.Ajax {
    public class JsonController : Controller {
        protected override IActionInvoker CreateActionInvoker() {
            return new JsonActionInvoker();
        }

        protected override void ExecuteCore() {
            var queryString = HttpContext.Request.QueryString;
            if (queryString.Keys.Count > 0 && String.Equals(queryString.GetValues(0).First(), "json", StringComparison.OrdinalIgnoreCase)) {
                ActionInvoker.InvokeAction(ControllerContext, "Internal::Proxy");
                return;
            }

            // This is where the proxy places the action.
            string action = HttpContext.Request.Headers["x-mvc-action"] ?? HttpContext.Request.QueryString["action"];

            if (!RouteData.Values.ContainsKey("action") && !String.IsNullOrEmpty(action)) {
                RouteData.Values.Add("action", action);
            }

            if (RouteData.Values.ContainsKey("action")) {
                base.ExecuteCore();
                return;
            }

            ActionInvoker.InvokeAction(ControllerContext, "Internal::ProxyDefinition");
        }
    }
}