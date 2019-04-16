using Book_Management_System.Common;
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
    [AuthorizeUser]
    public class PaymentController : BaseController
    {
        private UserLogin CurrentUserId
        {
            get { return (UserLogin)HttpContext.Session[Constants.USER_SESSION]; }
        }
        public int CheckBook(Cart cart)
        {
            var cartItem = DB.CartItems.Where(x => x.IdCard == cart.Id).ToList();
            foreach(var item in cartItem)
            {
                if(item.Book.Quantity == 0 || item.Book.IsActive == false || item.Book.BookCategory.IsActive == false)
                {
                    return 0;
                }
            }
            return 1;
        }
        // GET: Payment
        Model DB = new Model();
        public ActionResult Index()
        {
            var IdUser = CurrentUserId.UserId;

            var Cart = from c in DB.Carts.ToList()
                       where c.IdUser == IdUser && c.IsActive == true
                       select c;
            var Carts = Cart.FirstOrDefault();
            if (Carts != null && CheckBook(Carts) == 1)
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

                    foreach(var item in CartItem.CartItems)
                    {
                        var itemBook = DB.Books.Find(item.IdBook);
                        itemBook.Quantity -= item.Quantity;
                        DB.Entry(itemBook).State = EntityState.Modified;
                        DB.SaveChanges();
                    }

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