using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Book_Management_System.Infrastructure;
using Book_Management_System.Models;

namespace Book_Management_System.Areas.Admin.Controllers
{
    public class OrdersController : BaseAdminController
    {
        private Model db = new Model();

        // GET: Admin/Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Cart);
            return View(orders.ToList());
        }

        // GET: Admin/Orders/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Admin/Orders/Create
        public ActionResult Create()
        {
            ViewBag.IdCard = new SelectList(db.Carts, "Id", "IdUser");
            return View();
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdCard,CreateDate,MethodPayment,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCard = new SelectList(db.Carts, "Id", "IdUser", order.IdCard);
            return View(order);
        }

        // GET: Admin/Orders/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCard = new SelectList(db.Carts, "Id", "IdUser", order.IdCard);
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdCard,CreateDate,MethodPayment,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCard = new SelectList(db.Carts, "Id", "IdUser", order.IdCard);
            return View(order);
        }
        public JsonResult SetDelivering(string id)
        {
            var Checked = false;
            var order = db.Orders.Find(id);
            try
            {
                order.Status = ConstantDefine.DELIVERING;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                Checked = true;
            }
            catch(Exception ex)
            {
            }
            
            return Json(new
            {
                status = Checked
            });
               
        }
        public JsonResult SetCompleted(string id)
        {
            var Checked = false;
            var order = db.Orders.Find(id);
            try
            {
                order.Status = ConstantDefine.COMPLETED;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                Checked = true;
            }
            catch (Exception ex)
            {
            }

            return Json(new
            {
                status = Checked
            });
        }
        public ActionResult SetCancelled(string id)
        {
            var Checked = false;
            var order = db.Orders.Find(id);
            try
            {
                order.Status = ConstantDefine.CANCELLED;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                Checked = true;
            }
            catch (Exception ex)
            {
            }

            return Json(new
            {
                status = Checked
            });
        }

        // GET: Admin/Orders/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
