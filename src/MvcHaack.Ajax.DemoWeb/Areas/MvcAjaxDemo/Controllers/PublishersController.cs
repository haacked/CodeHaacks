using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using MvcHaack.Ajax.Sample.Models;

namespace MvcHaack.Ajax.Sample.Areas.AjaxDemo.Controllers {
    public class PublishersController : JsonController {
        private ComicContext db = new ComicContext();

        public IEnumerable<Publisher> List() {
            return from p in db.Publishers.ToList()
                   select new Publisher { Id = p.Id, Name = p.Name };
        }


        public ViewResult Details(int id) {
            Publisher publisher = db.Publishers.Find(id);
            return View(publisher);
        }

        [HttpPost]
        public bool Create(Publisher publisher) {
            if (ModelState.IsValid) {
                db.Publishers.Add(publisher);
                db.SaveChanges();
            }

            return true;
        }

        //
        // GET: /Publishers/Edit/5

        public ActionResult Edit(int id) {
            Publisher publisher = db.Publishers.Find(id);
            return View(publisher);
        }

        //
        // POST: /Publishers/Edit/5

        [HttpPost]
        public ActionResult Edit(Publisher publisher) {
            if (ModelState.IsValid) {
                db.Entry(publisher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(publisher);
        }

        //
        // GET: /Publishers/Delete/5

        public ActionResult Delete(int id) {
            Publisher publisher = db.Publishers.Find(id);
            return View(publisher);
        }

        //
        // POST: /Publishers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id) {
            Publisher publisher = db.Publishers.Find(id);
            db.Publishers.Remove(publisher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}