using System.Web.Mvc;

namespace JsonHijackDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "JSON Hijack Demo";

            return View();
        }

        [Authorize]
        public JsonResult AdminBalances()
        {
            var balances = new[]
            {
                new {Id = 1, Balance = 3.14},
                new {Id = 2, Balance = 2.72},
                new {Id = 3, Balance = 1.62}
            };
            return Json(balances, JsonRequestBehavior.AllowGet);
        }
    }
}