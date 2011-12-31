using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcHaack.Ajax
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ValidateJsonAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var httpContext = new JsonAntiForgeryHttpContextWrapper(HttpContext.Current);
            AntiForgery.Validate(httpContext, Salt ?? string.Empty);
        }

        public string Salt
        {
            get;
            set;
        }

        private class JsonAntiForgeryHttpContextWrapper : HttpContextWrapper
        {
            readonly HttpRequestBase _request;
            public JsonAntiForgeryHttpContextWrapper(HttpContext httpContext)
                : base(httpContext)
            {
                _request = new JsonAntiForgeryHttpRequestWrapper(httpContext.Request);
            }

            public override HttpRequestBase Request
            {
                get
                {
                    return _request;
                }
            }
        }

        private class JsonAntiForgeryHttpRequestWrapper : HttpRequestWrapper
        {
            readonly NameValueCollection _form;

            public JsonAntiForgeryHttpRequestWrapper(HttpRequest request)
                : base(request)
            {
                _form = new NameValueCollection(request.Form);
                if (request.Headers["__RequestVerificationToken"] != null)
                {
                    _form["__RequestVerificationToken"] = request.Headers["__RequestVerificationToken"];
                }
            }

            public override NameValueCollection Form
            {
                get
                {
                    return _form;
                }
            }
        }
    }
}
