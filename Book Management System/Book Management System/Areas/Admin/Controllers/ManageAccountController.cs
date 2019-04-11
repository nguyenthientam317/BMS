using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Book_Management_System.Models;
using Book_Management_System.Common;

namespace Book_Management_System.Areas.Admin.Controllers
{
    [AuthorizeUser]
    public class ManageAccountController : Controller
    {
        private Model db = new Model();

        // GET: Admin/ManageUsers
        // Get list of accounts
        public ActionResult Index()
        {
            var model = db.Accounts.ToList();
            return View(model);
        }

        // automatically increate Id
        public string FindNextIdAccount()
        {
            var ListId = db.Accounts.ToList().Select(x => x.Id);
            int i = 1;
            var Date = DateTime.Now;
            string NewId = $"{Date.Month}{Date.Year.ToString().Substring(2)}_{i}";  // format for id MM/YYYY_Id
            while (ListId.Where(p => p.Equals(NewId)).Count() > 0)
            {
                i++;
                NewId = $"{Date.Month}{Date.Year.ToString().Substring(2)}_{i}";
            }
            return NewId;
        }
        [AllowAnonymous]
        // Find UserName is already exist
        public JsonResult IsUserNameUnique(string UserName)
        {
            var result = new AccountValidation();
            if (result.CheckUserName(UserName) != null)
            {
                var MyDictionary = new Dictionary<string, bool>();
                MyDictionary.Add("valid", false);
                return Json(MyDictionary, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var MyDictionary = new Dictionary<string, bool>();
                MyDictionary.Add("valid", true);
                return Json(MyDictionary, JsonRequestBehavior.AllowGet);
            }

        }
        [AllowAnonymous]
        // Find Email is already exist
        public JsonResult IsEmailUnique(string email,string id)
        {
            var result = new AccountValidation();
            if (result.CheckEmail(email) != null && !email.Equals(result.GyByEmail(id)))
            {
                var MyDictionary = new Dictionary<string, bool>();
                MyDictionary.Add("valid", false);
                return Json(MyDictionary, JsonRequestBehavior.AllowGet);
            }
            else 
            {
                var MyDictionary = new Dictionary<string, bool>();
                MyDictionary.Add("valid", true);
                return Json(MyDictionary, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Create()
        {
            ViewBag.IdRole = new SelectList(db.Roles, "Id", "RoleName");
            return View();
        }

        // POST: Admin/ManageAccount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Password,IdRole,IsActive")] Account account)
        {
            if (ModelState.IsValid)
            {
               using(DbContextTransaction tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        User user = new User();
                        user.Id = FindNextIdAccount();
                        user.IsActive = account.IsActive;
                        account.Id = user.Id;   // Auto create 1 new Id 
                        account.Password = Encryptor.ToMD5(account.Password);   // Encrypt Passord

                        db.Users.Add(user);
                        db.Accounts.Add(account);
                        db.SaveChanges();
                        tran.Commit();
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        tran.Rollback();
                       
                    }
                    
                }
               
            }

            ViewBag.IdRole = new SelectList(db.Roles, "Id", "RoleName", account.IdRole);
            ViewBag.Id = new SelectList(db.Users, "Id", "FirstName", account.Id);
            return View(account);
        }

        //GET: Admin/ManageUsers
        //Get detail a User
        public ActionResult Detail(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            user.Avatar = "/Assets/user-avatar/" + user.Id + "/" + Path.GetFileName(user.Avatar);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);  // return to a Detail partial View 
        }

        // Delete a User
        /*public ActionResult Delete(string id)
       {
           using(DbContextTransaction transaction = db.Database.BeginTransaction()) // Begin a transaction
           {
               try
               {
                   var model = db.Users.Find(id);
                   var account = db.Accounts.Find(id);

                   db.Users.Remove(model);
                   db.SaveChanges();
                   db.Accounts.Remove(account);
                   db.SaveChanges();
                   transaction.Commit();
                   return View("Index");
               }
               catch
               {
                   transaction.Rollback();  // Roll back when having an error
                   return View("Index");
               }
           }

       }*/
    

        // GET: Admin/ManageAccount/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            user.Avatar = "/Assets/user-avatar/" + user.Id + "/" + Path.GetFileName(user.Avatar);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Accounts, "Id", "UserName", user.Id);
            return View(user);
        }

        // POST: Admin/ManageAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Birthday,Email,Address,Gender,IsActive,Avatar")] User user)
        {
            if (ModelState.IsValid)
            {

                using (DbContextTransaction tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        string link = Server.MapPath(@"~\Assets\user-avatar\" + user.Id);
                        var model = db.Accounts.Find(user.Id);
                        if (!Directory.Exists(link))
                        {
                            Directory.CreateDirectory(Server.MapPath(@"~\Assets\user-avatar\" + user.Id));
                        }
                        var f = Request.Files["Avatar"];
                        
                        if (f != null && f.ContentLength > 0)
                        {
                            string fullPath = link + f.FileName;
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                            var path = (link + @"\" + f.FileName);
                            f.SaveAs(path);
                            user.Avatar = (link + @"\" + f.FileName);
                            model.IsActive = user.IsActive;
                            db.Entry(user).State = EntityState.Modified;
                            db.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            var oldUser = db.Users.Find(user.Id);
                            user.Avatar = oldUser.Avatar;
                            model.IsActive = user.IsActive;
                            db.Entry(model).State = EntityState.Modified;
                        }
                     
                        db.SaveChanges();
                        tran.Commit();
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        tran.Rollback();
                    }
                }
                   
            }
         
            return View(user);
        }

        // GET: Admin/ManageAccount/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/ManageAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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

       
    }
}
