using System;
using System.Web.Mvc;

namespace MvcHaack.ControllerInspector.DemoWeb.Controllers {
    [SomeFake]
    public class HomeController : Controller {
        public ActionResult Index(string id) {
            return View();
        }

        [HttpPost]
        [HttpPut]
        public ActionResult Index(string id, object something) {
            return View();
        }

        [Authorize]
        [ActionName("GetAll")]
        public ActionResult List(string id = "Some default value") {
            return View();
        }

        public string List(SomeModel model) {
            return "Test";
        }
    }

    public class SomeModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
    }

    [AttributeUsage(AttributeTargets.All)]
    public class SomeFakeAttribute : Attribute {
    }
}
