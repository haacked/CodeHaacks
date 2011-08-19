using System.Web.Mvc;

namespace MvcHaack.ControllerInspector {
    public class InspectorActionInvoker : ControllerActionInvoker {
        IActionInvoker _invoker;

        public InspectorActionInvoker(IActionInvoker invoker) {
            _invoker = invoker;
        }

        public override bool InvokeAction(ControllerContext controllerContext, string actionName) {
            var httpContext = controllerContext.HttpContext;
            var controllerActionInvoker = _invoker as ControllerActionInvoker;
            if (controllerActionInvoker != null) {
                var detailer = new ControllerDetailer();
                httpContext.Response.Write(detailer.GetControllerDetails(GetControllerDescriptor(controllerContext)));
                return true;
            }

            return _invoker.InvokeAction(controllerContext, actionName);
        }
    }
}
