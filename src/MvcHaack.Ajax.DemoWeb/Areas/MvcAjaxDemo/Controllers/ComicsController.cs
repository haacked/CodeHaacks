using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using MvcHaack.Ajax.Sample.Models;

namespace MvcHaack.Ajax.Sample.Areas.AjaxDemo.Controllers
{
    public class ComicsController : JsonController
    {
        private ComicContext db = new ComicContext();

        public IEnumerable<ComicBook> List()
        {
            return db.ComicBooks.ToList();
        }

        [HttpPost]
        public ComicBook Create(ComicBook comicbook)
        {
            if (ModelState.IsValid)
            {
                db.ComicBooks.Add(comicbook);
                db.SaveChanges();
            }
            return comicbook;
        }

        [HttpPost]
        public void Edit(ComicBook comicbook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comicbook).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        [HttpPost]
        public void Delete(int id)
        {
            ComicBook comicbook = db.ComicBooks.Find(id);
            db.ComicBooks.Remove(comicbook);
            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}