using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace MvcHaack.Ajax
{
    /// <summary>
    /// Note: this version of the code only compiles against MVC4 where Controller implements IAsyncController.
    /// </summary>
    public class JsonController : Controller
    {
        protected override IActionInvoker CreateActionInvoker()
        {
            return new JsonActionInvoker();
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {

            var queryString = HttpContext.Request.QueryString;
            if (queryString.Keys.Count > 0 && String.Equals(queryString.GetValues(0).First(), "json", StringComparison.OrdinalIgnoreCase))
            {
                ActionInvoker.InvokeAction(ControllerContext, "Internal::Proxy");
                return new CompletedAsyncResult() { AsyncState = state };
            }

            // This is where the proxy places the action.
            string action = HttpContext.Request.Headers["x-mvc-action"] ?? HttpContext.Request.QueryString["action"];

            if (!RouteData.Values.ContainsKey("action") && !String.IsNullOrEmpty(action))
            {
                RouteData.Values.Add("action", action);
            }

            if (RouteData.Values.ContainsKey("action"))
            {
                return base.BeginExecuteCore(callback, state);
            }
            else
            {
                ActionInvoker.InvokeAction(ControllerContext, "Internal::ProxyDefinition");
                return new CompletedAsyncResult() { AsyncState = state };
            }
        }

        protected override void EndExecuteCore(IAsyncResult asyncResult)
        {
            if (asyncResult is CompletedAsyncResult)
            {
                return;
            }
            else
            {
                base.EndExecuteCore(asyncResult);
            }
        }

        protected override void ExecuteCore()
        {
            EndExecuteCore(BeginExecuteCore(new AsyncCallback((IAsyncResult x) => {}), null));
        }

        class CompletedAsyncResult : IAsyncResult 
        {
            public object AsyncState
            {
                get;
                set;
            }

            public WaitHandle AsyncWaitHandle
            {
                get { throw new NotSupportedException("CompletedAsyncResult.AsyncWaitHandle"); }
            }

            public bool CompletedSynchronously
            {
                get { return true; }
            }

            public bool IsCompleted
            {
                get { return true; }
            }
        }
    }
}