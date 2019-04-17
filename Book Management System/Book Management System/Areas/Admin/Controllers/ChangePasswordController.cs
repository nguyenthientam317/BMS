using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Book_Management_System.Models;
using System.Security.Cryptography;
using System.Text;
using Book_Management_System.Common;

namespace Book_Management_System.Areas.Admin.Controllers
{
    [AuthorizeUser]
    public class ChangePasswordController : BaseAdminController
    {
        private Model db = new Model();

        public string ToMD5(string str)
        {
            string result = "";
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            for (int i = 0; i < buffer.Length; i++)
            {
                result += buffer[i].ToString("x2");
            }
            return result;
        }

        // GET: Admin/ChangePassword/Edit/5
        [ChildActionOnly]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            account.Password = string.Empty;
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRole = new SelectList(db.Roles, "Id", "RoleName", account.IdRole);
            ViewBag.Id = new SelectList(db.Users, "Id", "FirstName", account.Id);
            return PartialView(account);
        }

        // POST: Admin/ChangePassword/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Password,IdRole,IsActive")] Account account)
        {
            if (ModelState.IsValid)
            {
                account.Password = ToMD5(account.Password);
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "ManageAccount");
            }
            ViewBag.IdRole = new SelectList(db.Roles, "Id", "RoleName", account.IdRole);
            ViewBag.Id = new SelectList(db.Users, "Id", "FirstName", account.Id);
            return View("Edit", "ManageAccount",new {id = account.Id });
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
