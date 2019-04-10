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
            var ListOrder = from o in DB.Orders.ToList()
                            where o.Cart.IdUser == IdUser
                            select o;

            return View(ListOrder);
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
        public JsonResult DeleteOrder(string idCart)
        {
            var check = false;
            try
            {
                var item = DB.Orders.Where(l => l.IdCard.Equals(idCart)).FirstOrDefault();
                item.Status = "Cancel";
                DB.Entry(item).State = EntityState.Modified;
                DB.SaveChanges();
                check = true;
            }
            catch (Exception ex)
            {

            }
            return Json(new
            {
                status = check
            });

        }

    }
}