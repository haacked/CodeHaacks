using System.Web.Mvc;

namespace MvcHaack.Ajax.Sample.Areas.AjaxDemo {
    public class AjaxDemoAreaRegistration : AreaRegistration {
        public override string AreaName {
            get {
                return "MvcAjaxDemo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            context.Routes.Add(new JsonRoute("json/{controller}"));

            context.MapRoute(
                "AjaxDemo_default",
                "AjaxDemo/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
