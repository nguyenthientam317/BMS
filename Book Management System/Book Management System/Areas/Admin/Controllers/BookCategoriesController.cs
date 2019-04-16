using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Book_Management_System.Common;
using Book_Management_System.Models;

namespace Book_Management_System.Areas.Admin.Controllers
{
    [AuthorizeUser]
    public class BookCategoriesController : BaseAdminController
    {
        private Model db = new Model();

        // GET: Admin/BookCategories
        public ActionResult Index()
        {
            return View(db.BookCategories.ToList());
        }

        // GET: Admin/BookCategories/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCategory bookCategory = db.BookCategories.Find(id);
            if (bookCategory == null)
            {
                return HttpNotFound();
            }
            return View(bookCategory);
        }

        // GET: Admin/BookCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BookCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CateName,IsActive,Description")] BookCategory bookCategory)
        {
            if (ModelState.IsValid)
            {
                var IdNext = FindNextId();
                bookCategory.Id = IdNext;
                db.BookCategories.Add(bookCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookCategory);
        }

        // GET: Admin/BookCategories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCategory bookCategory = db.BookCategories.Find(id);
            if (bookCategory == null)
            {
                return HttpNotFound();
            }
            return View(bookCategory);
        }


        // POST: Admin/BookCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CateName,IsActive,Description")] BookCategory bookCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookCategory);
        }

        public ActionResult Detail(string id)
        {
            var model = db.BookCategories.Find(id);
            return PartialView("Detail", model);
        }

        public ActionResult Disable(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCategory bookCategory = db.BookCategories.Find(id);
            if (bookCategory == null)
            {
                return HttpNotFound();
            }
            return View(bookCategory);
        }

        [HttpPost, ActionName("Disable")]
        [ValidateAntiForgeryToken]
        public ActionResult DisableConfirmed(string id)
        {
            BookCategory bookCategory = db.BookCategories.Find(id);
            bookCategory.IsActive = false;
            db.Entry(bookCategory).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/BookCategories/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCategory bookCategory = db.BookCategories.Find(id);
            if (bookCategory == null)
            {
                return HttpNotFound();
            }
            return View(bookCategory);
        }

        // POST: Admin/BookCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BookCategory bookCategory = db.BookCategories.Find(id);
            db.BookCategories.Remove(bookCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // Find next ID of book caterogy
        public string FindNextId()
        {
            var ListId = db.BookCategories.ToList().Select(l => l.Id);
            int i = 1;
            var Date = DateTime.Now;
            string NewId = $"{Date.Month}{Date.Year.ToString().Substring(2)}_{i}";
            while (ListId.Where(p => p.Equals(NewId)).Count() > 0)
            {
                i++;
                NewId = $"{Date.Month}{Date.Year.ToString().Substring(2)}_{i}";
            }
            return NewId;

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
