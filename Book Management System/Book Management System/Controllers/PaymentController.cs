using Book_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book_Management_System.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        Model db = new Model();
        public ActionResult Index()
        {
            var IdUser = "1";
            var Cart = db.Carts.Where(l => l.IdUser.Equals(IdUser)).FirstOrDefault();
            return View(Cart);
        }
    }
}