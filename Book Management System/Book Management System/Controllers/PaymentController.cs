using Book_Management_System.Infrastructure;
using Book_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Book_Management_System.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        Model DB = new Model();
        public ActionResult Index()
        {
            var IdUser = "1";
            //var Cart = DB.Carts.Where(l => l.IdUser.Equals(IdUser) && l.IsActive.Equals(true)).FirstOrDefault(); can not use
            var Cart = from c in DB.Carts.ToList()
                       where c.IdUser == IdUser && c.IsActive == true
                       select c;
            var Carts = Cart.FirstOrDefault();
            if (Carts != null)
            {
                return View(Carts);
            }
            else
            {
                return Redirect(ConstantDefine.HOME_URL);
            }
            
        }
        public string FindNextId()
        {
            var ListId = DB.Orders.ToList().Select(l => l.Id);
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
        [HttpPost]
        public JsonResult CompletePayment(string orderJson)
        {
            using (var Transaction = DB.Database.BeginTransaction())
            {
                bool check = false;
                try
                {
                    var OrderItem = CommonMethod.JsonDeserialize<Order>(orderJson);
                    OrderItem.Id = FindNextId();
                    OrderItem.CreateDate = DateTime.Now;
                    OrderItem.Status = ConstantDefine.PROCESSING;
                    DB.Orders.Add(OrderItem);
                    DB.SaveChanges();

                    var CartItem = DB.Carts.Find(OrderItem.IdCard);
                    CartItem.IsActive = false;
                    DB.Entry(CartItem).State = EntityState.Modified;
                    DB.SaveChanges();

                    Transaction.Commit();
                    check = true;
                    
                }
                catch(Exception ex)
                {
                    Transaction.Rollback();
                    
                }
                return Json(new
                {
                    status = check
                });

            }
        }
    }
}