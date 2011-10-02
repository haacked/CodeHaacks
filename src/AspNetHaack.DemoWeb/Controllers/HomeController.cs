using System.Web.Mvc;

namespace AspNetHaack.DemoWeb.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        [Authorize]
        public ActionResult About() {
            return Json(new { some = "thing" });
        }
    }
}
