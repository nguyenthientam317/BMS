using Book_Management_System.Infrastructure;
using Book_Management_System.Models;
using System;
using System.Collections.Generic;
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
            var ListCartItems = CommonMethod.JsonSerialize<CartItem>(CartItems.ToList());
            return Json(new
            {
                status = true
            });
        }
    }
}