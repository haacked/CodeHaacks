using System.Web.Mvc;

namespace MvcHaack.ControllerInspector.DemoWeb.Controllers
{
    public class TypoControler : Controller
    {
        //
        // GET: /TypoControler/

        public string Index()
        {
            return "Test";
        }

        public class BadNestedController : Controller
        {
        }
    }

    internal class InternalController : Controller
    {
    }
}