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
            var ListOrder = from o in DB.Orders.ToList()
                            where o.Cart.IdUser == IdUser
                            select o;

            return View(ListOrder);
        }
        [HttpPost]
        public JsonResult ViewDetail(string id)
        {
            var CartItems = from o in DB.CartItems.ToList()
                            where o.IdCard == id
                            select o;
            var JsonListCartItems = CommonMethod.JsonSerialize<CartItem>(CartItems.ToList());
            return Json(new
            {
                data = JsonListCartItems,
                status = true

            });

        }
        [HttpPost]
        public JsonResult DeleteOrder(string idCart)
        {
            try
            {
                var item = DB.Orders.Where(l => l.IdCard.Equals(idCart)).FirstOrDefault();
                item.Status = "Cancel";
                DB.Entry(item).State = EntityState.Modified;
                DB.SaveChanges();

                return Json(new
                {
                    status = true
                });
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    status = false
                });
            }
            
        }

    }
}