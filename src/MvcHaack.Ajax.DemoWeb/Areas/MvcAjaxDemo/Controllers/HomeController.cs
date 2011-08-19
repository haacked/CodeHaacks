using System.Web.Mvc;

namespace MvcHaack.Ajax.Sample.Areas.AjaxDemo.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult ComicsDemo() {
            return View();
        }

        public ActionResult ComicsPostDemo() {
            return View();
        }

        public ActionResult KnockoutDemo() {
            return View();
        }
    }
}
