using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ListModelBindingWeb.Models;

namespace ListModelBindingWeb.Controllers
{
    public class HomeController : Controller
    {
        // Demo 1
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ICollection<int> numbers) {
            return View(numbers);
        }

        // Demo 2
        public ActionResult Sequential()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Sequential(ICollection<Book> books)
        {
            return View(books);
        }

        // Demo 3
        public ActionResult NonSequential()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NonSequential(ICollection<Book> books)
        {
            return View(books);
        }

        // Demo 4
        public ActionResult EditorTemplate()
        {
            var books = new Book[] { 
                new Book {Title = "Curious George", Author = "H.A. Rey", DatePublished = DateTime.Parse("1973/2/23")}, 
                new Book {Title = "Code Complete", Author = "H.A. Rey", DatePublished = DateTime.Parse("2004/6/9")},
                new Book {Title = "The Two Towers", Author = "H.A. Rey", DatePublished = DateTime.Parse("2005/6/1")},
                new Book {Title = "Homeland", Author = "RA Salvatore", DatePublished = DateTime.Parse("1990/9/19")},
                new Book {Title = "Moby Dick", Author = "Herman Melville", DatePublished = DateTime.Parse("2008/1/1")}
            };
            var model = new BookEditModel { Books = books.ToList() };
            
            return View(model);
        }

        [HttpPost]
        public ActionResult EditorTemplate(BookEditModel model)
        {
            return View(model);
        }

    }
}
