using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace MvcHaack.ControllerInspector
{
    public class InspectorControllerFactory : IControllerFactory
    {
        readonly IControllerFactory _controllerFactory;
        public InspectorControllerFactory(IControllerFactory controllerFactory)
        {
            _controllerFactory = controllerFactory;
        }

        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            var controller = _controllerFactory.CreateController(requestContext, controllerName);

            if (IsInspectorRequest(requestContext.HttpContext.Request))
            {
                var normalController = controller as Controller;
                if (normalController != null)
                {
                    var invoker = normalController.ActionInvoker;
                    normalController.ActionInvoker = new InspectorActionInvoker(invoker);
                }
            }
            return controller;
        }

        private static bool IsInspectorRequest(HttpRequestBase httpRequest)
        {
            return httpRequest.IsLocal
                && httpRequest.QueryString.Keys.Count > 0
                && httpRequest.QueryString.GetValues(null) != null
                && httpRequest.QueryString.GetValues(null).Contains("inspect");
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return _controllerFactory.GetControllerSessionBehavior(requestContext, controllerName);
        }

        public void ReleaseController(IController controller)
        {
            _controllerFactory.ReleaseController(controller);
        }
    }
}
