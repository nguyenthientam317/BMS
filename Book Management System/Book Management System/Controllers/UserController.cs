﻿using Book_Management_System.Common;
using Book_Management_System.Infrastructure;
using Book_Management_System.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Book_Management_System.Controllers
{
    [AuthorizeUser]
    public class UserController : BaseController
    {
        private UserLogin CurrentUserId
        {
            get { return (UserLogin)HttpContext.Session[Constants.USER_SESSION]; }
        }
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
        // GET: User
        Model DB = new Model();
        public ActionResult Index()
        {

            string IdUser = CurrentUserId.UserId;

            var ListOrder = from o in DB.Orders.ToList()
                            where o.Cart.IdUser == IdUser 
                            select new OrderItemViewModel() {
                                Id = o.Id,
                                IdCard = o.IdCard,
                                Status = o.Status,
                                MethodPayment = o.MethodPayment,
                                CreateDate = o.CreateDate,
                                ListCartItem = o.Cart.CartItems,
                            };
            var ListOrders = ListOrder.ToList();
            foreach (var x in ListOrders)
            {
                foreach (var y in x.ListCartItem)
                {
                    x.TotalPrice += y.Quantity * y.Book.Price;
                }
            }

            return View(ListOrders);
        }
        //View detail order
        [HttpPost]
        public JsonResult ViewDetail(string id)
        {
            var check = false;
            var CartItems = from o in DB.CartItems.ToList()
                            where o.IdCard == id
                            select new
                            {
                                Name = o.Book.Title,
                                o.Quantity,
                                o.Book.Price
                            };

            var JsonListCartItems = JsonConvert.SerializeObject(CartItems.ToList(),
            Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

            if(JsonListCartItems != null)
            {
                check = true;
            }

            return Json(new
            {
                data = JsonListCartItems,
                status = check

            });
        }
        //Delete order
        [HttpPost]
        public JsonResult DeleteOrder(string id)
        {
            using (var Transaction = DB.Database.BeginTransaction())
            {
                bool check = false;
                try
                {
                    var item = DB.Orders.Find(id);
                    item.Status = ConstantDefine.CANCELLED;
                    DB.Entry(item).State = EntityState.Modified;
                    DB.SaveChanges();
                    
                    var ListCart = DB.Carts.Find(item.IdCard);
                    var ListCartItem = ListCart.CartItems;
                    foreach(var entity in ListCartItem)
                    {
                        var itemBook = DB.Books.Find(entity.IdBook);
                        itemBook.Quantity += entity.Quantity;
                        DB.Entry(itemBook).State = EntityState.Modified;
                        DB.SaveChanges();
                    }
                    Transaction.Commit();
                    check = true;

                }
                catch (Exception ex)
                {
                    check = false;
                    Transaction.Rollback();
                }
                return Json(new
                {
                    status = check
                });
            }

           
            

        }
        //GET: Change information
        public ActionResult EditInfo(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = DB.Users.Find(id);
            user.Avatar = "/Assets/user-avatar/" + user.Id + "/" + Path.GetFileName(user.Avatar);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(DB.Accounts, "Id", "UserName", user.Id);
            return View(user);
        }
        //POST: Change information
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInfo([Bind(Include = "Id,FirstName,LastName,Birthday,Email,Address,Gender,IsActive,Avatar")] User user)
        {
            if (ModelState.IsValid)
            {

                using (DbContextTransaction tran = DB.Database.BeginTransaction())
                {
                    try
                    {
                        string link = Server.MapPath(@"~\Assets\user-avatar\" + user.Id);
                        var model = DB.Accounts.Find(user.Id);
                        model.IsActive = user.IsActive;
                        user.Birthday = Convert.ToDateTime(user.Birthday);
                        var oldUser = DB.Users.Where(x => x.Id == user.Id).Select(x => x.Avatar).FirstOrDefault();
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
                            user.Avatar = (@"\Assets\user-avatar\" + user.Id + @"\" + f.FileName);

                            DB.Entry(user).State = EntityState.Modified;
                        }
                        else
                        {
                            user.Avatar = oldUser;
                            DB.Entry(user).State = EntityState.Modified;
                        }
                        DB.Entry(model).State = EntityState.Modified;
                        DB.SaveChanges();
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
        //GET: Change password
        public ActionResult ChangePassword(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = DB.Accounts.Find(id);
            account.Password = string.Empty;
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRole = new SelectList(DB.Roles, "Id", "RoleName", account.IdRole);
            ViewBag.Id = new SelectList(DB.Users, "Id", "FirstName", account.Id);
            return PartialView(account);
        }

        //POST: Change password
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = "Id,UserName,Password,IdRole,IsActive")] Account account)
        {
            if (ModelState.IsValid)
            {
                account.Password = ToMD5(account.Password);
                DB.Entry(account).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("Index", "User");
            }
            ViewBag.IdRole = new SelectList(DB.Roles, "Id", "RoleName", account.IdRole);
            ViewBag.Id = new SelectList(DB.Users, "Id", "FirstName", account.Id);
            return View("Index", "User", new { id = account.Id });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}