using Book_Management_System.Common;
using Book_Management_System.Infrastructure;
using Book_Management_System.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book_Management_System.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        Model DB = new Model();
        public ActionResult Index()
        {
            string IdUser = "1";
            //OrderItemViewModel ListOrder = new OrderItemViewModel();
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

    }
}