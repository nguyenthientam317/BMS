using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Book_Management_System.Common;
using Book_Management_System.Models;

namespace Book_Management_System.Areas.Admin.Controllers
{
    [AuthorizeUser]
    public class ManageBookController : BaseAdminController
    {
        private Model db = new Model();

        // GET: Admin/ManageBook
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Author).Include(b => b.BookCategory).Include(b => b.Publisher);
            return View(books.ToList());
        }

        // GET: Admin/ManageBook/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        public ActionResult Detail(string id)
        {
            var model = db.Books.Find(id);
            return PartialView("Detail", model);
        }

        // GET: Admin/ManageBook/Create
        public ActionResult Create()
        {
            ViewBag.IdAuthor = new SelectList(db.Authors, "Id", "Name");
            ViewBag.IdCategory = new SelectList(db.BookCategories, "Id", "CateName");
            ViewBag.IdPublisher = new SelectList(db.Publishers, "Id", "Name");
            return View();
        }

        // POST: Admin/ManageBook/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Summary,ImageURL,ISBN,Price,Quantity,IdAuthor,IdPublisher,IdCategory,CreateDate,ModifiedDate,IsActive")] Book book)
        {
            if (ModelState.IsValid)
            {
                var IdNext = FindNextId();
                book.Id = IdNext;
                string link = Server.MapPath(@"~\Assets\book-image\" + book.Id);
                if (!Directory.Exists(link))
                {
                    Directory.CreateDirectory(Server.MapPath(@"~\Assets\book-image\" + book.Id));
                }
                var f = Request.Files["ImageURL"];
                if (f != null && f.ContentLength > 0)
                {
                    string fullPath = link + f.FileName;
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    var path = link + @"\" + f.FileName;
                    f.SaveAs(path);
                    book.ImageURL = @"\Assets\book-image\" + book.Id + @"\" + f.FileName;
                }
               
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAuthor = new SelectList(db.Authors, "Id", "Name", book.IdAuthor);
            ViewBag.IdCategory = new SelectList(db.BookCategories, "Id", "CateName", book.IdCategory);
            ViewBag.IdPublisher = new SelectList(db.Publishers, "Id", "Name", book.IdPublisher);
            return View(book);
        }

        // GET: Admin/ManageBook/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            //book.ImageURL = "/Assets/book-image/" + book.Id + "/" + Path.GetFileName(book.ImageURL);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAuthor = new SelectList(db.Authors, "Id", "Name", book.IdAuthor);
            ViewBag.IdCategory = new SelectList(db.BookCategories, "Id", "CateName", book.IdCategory);
            ViewBag.IdPublisher = new SelectList(db.Publishers, "Id", "Name", book.IdPublisher);
            return View(book);
        }

        // POST: Admin/ManageBook/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Summary,ImageURL,ISBN,Price,Quantity,IdAuthor,IdPublisher,IdCategory,CreateDate,ModifiedDate,IsActive")] Book book)
        {
            if (ModelState.IsValid)
            {
                string link = Server.MapPath(@"~\Assets\book-image\" + book.Id);
                var oldBook = db.Books.Where(x => x.Id == book.Id).Select(x => x.ImageURL).FirstOrDefault();
                if (!Directory.Exists(link))
                {
                    Directory.CreateDirectory(Server.MapPath(@"~\Assets\book-image\" + book.Id));
                }
                var f = Request.Files["ImageURL"];
                if (f != null && f.ContentLength > 0)
                {
                    string fullPath = link + f.FileName;
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    var path = link  + @"\" + f.FileName;
                    f.SaveAs(path);
                    book.ModifiedDate = DateTime.Now;
                    book.ImageURL = @"\Assets\book-image\" + book.Id +@"\"+ f.FileName;
                    db.Entry(book).State = EntityState.Modified;
                }
                else
                {
                    book.ModifiedDate = DateTime.Now;
                    book.ImageURL = oldBook;
                    db.Entry(book).State = EntityState.Modified;
                }
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAuthor = new SelectList(db.Authors, "Id", "Name", book.IdAuthor);
            ViewBag.IdCategory = new SelectList(db.BookCategories, "Id", "CateName", book.IdCategory);
            ViewBag.IdPublisher = new SelectList(db.Publishers, "Id", "Name", book.IdPublisher);
            return View(book);
        }

        // GET: Admin/ManageBook/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Admin/ManageBook/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public string FindNextId()
        {
            var ListId = db.Books.ToList().Select(l => l.Id);
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
    }
}
