using System.Collections;
using System.Web.Mvc;
using MvcHaack.Ajax.Sample.Models;

namespace MvcHaack.Ajax.Sample.Areas.AjaxDemo.Controllers {
    public class ComicsDemoController : JsonController {
        public IEnumerable List() {
            return new[] {
              new {Id = 1, Title = "Groo"},
              new {Id = 1, Title = "Batman"},
              new {Id = 1, Title = "Spiderman"}
            };
        }

        public object Save(ComicBook book) {
            return new { message = "Saved!", comic = book };
        }

        [ValidateJsonAntiForgeryToken]
        public object SaveSecure(ComicBook book) {
            return new { message = "Saved!", comic = book };
        }
    }
}
