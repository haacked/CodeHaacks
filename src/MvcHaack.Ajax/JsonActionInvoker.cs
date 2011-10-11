using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MvcHaack.Ajax {
    public class JsonActionInvoker : ControllerActionInvoker {
        protected override ActionResult CreateActionResult(ControllerContext controllerContext, ActionDescriptor actionDescriptor, object actionReturnValue) {
            return new JsonResult { Data = actionReturnValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public override bool InvokeAction(ControllerContext controllerContext, string actionName) {
            if (actionName == "Internal::Proxy") {
                return RenderJavaScriptProxyScript(controllerContext);
            }
            if (actionName == "Internal::ProxyDefinition") {
                return RenderJavaScriptProxyDescription(controllerContext);
            }
            return base.InvokeAction(controllerContext, actionName);
        }

        private bool RenderJavaScriptProxyScript(ControllerContext controllerContext) {
            var controllerDescriptor = GetControllerDescriptor(controllerContext);
            var actions = controllerDescriptor.GetCanonicalActions();
            var actionNames = from action in actions select action.ActionName;
            var serializer = new JavaScriptSerializer();
            var actionMethodNames = serializer.Serialize(actionNames);

            string proxyScript = @"
if (typeof $mvc === 'undefined') {{
    $mvc = {{}};
}}
$mvc.{0} = [];

if (!window.JSON)
    throw 'JsonActionInvoker: No JSON parser found. Please ensure json2.js is referenced before the script tag pointing to the controller to support clients without native JSON parsing support, e.g. IE 8 or less.';

$.each({1}, function(action) {{
    var action = this;
    
    $mvc.{0}[this] = function(obj, includeAntiForgeryToken) {{
        var headers = {{'x-mvc-action': action}};
        if (includeAntiForgeryToken) {{
            headers['__RequestVerificationToken'] = $('input[name=""__RequestVerificationToken""]').val();
        }}

        return $.ajax({{
            cache: false,
            dataType: 'json',
            type: 'POST',
            headers: headers,
            data: window.JSON.stringify(obj),
            contentType: 'application/json; charset=utf-8',
            url: '{2}&action=' + action
        }});
    }};
}});
";
            string serviceUrl = controllerContext.HttpContext.Request.RawUrl;
            int proxyIndex = serviceUrl.IndexOf("?json", StringComparison.OrdinalIgnoreCase);
            if (proxyIndex > -1) {
                serviceUrl = serviceUrl.Substring(0, proxyIndex) + "?invoke";
            }

            proxyScript = String.Format(proxyScript,
                controllerDescriptor.ControllerName,
                actionMethodNames,
                serviceUrl);

            controllerContext.HttpContext.Response.ContentType = "text/javascript";
            controllerContext.HttpContext.Response.Write(proxyScript);
            return true;
        }

        private bool RenderJavaScriptProxyDescription(ControllerContext controllerContext) {
            var controllerDescriptor = GetControllerDescriptor(controllerContext);

            var response = controllerContext.HttpContext.Response;

            const string html = @"<!DOCTYPE html>
<html>
<head>
    <title>Controller: {0} JavaScript Proxy Definition</title>
    <style
        table {{
            font-family: calibri;
            font-size: 1em;
            border-collapse: collapse;
        }}

        table td {{
            border: solid 1px;
            padding: 6px;
        }}
    </style>
</head>
<body>
    <header>
        <h1>JavaScript Proxy for controller: {0}</h1>
    </header>
    <div id=""main"">
        <table>
            <tr>
                <th>Action</th>
                <th>Code</th>
            </tr>
            {1}
        </table>
    </div>
</body>
</html>";
            string actionRows = "";

            foreach (var action in controllerDescriptor.GetCanonicalActions()) {
                const string row = @"<tr>
<td>{0}</td>
<td><code>$mvc.{1}.{0}({2})</code></td>
</tr>";
                var parameters = from p in action.GetParameters()
                                 select p.ParameterType.Name + " " + p.ParameterName;
                string parametersText = String.Join(",", parameters);
                actionRows += String.Format(row, action.ActionName, controllerDescriptor.ControllerName, parametersText);
            }

            response.Write(String.Format(html, controllerDescriptor.ControllerName, actionRows));

            return true;
        }
    }
}