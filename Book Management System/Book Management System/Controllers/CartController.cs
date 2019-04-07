using Book_Management_System.Common;
using Book_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book_Management_System.Controllers
{
    public class CartController : Controller
    {

        Model db = new Model();
        // GET Current User's ID and Name
        private UserLogin CurrentUserId
        {
            get { return (UserLogin)HttpContext.Session[Constants.USER_SESSION]; }
        }
        private Cart CurrentCartId
        {
            get { return db.Carts.Where(x => x.Id == CurrentUserId.UserId).FirstOrDefault(); }
        }
        public string FindNextIdCart()
        {
            var ListId = db.Carts.ToList().Select(x => x.Id);
            int i = 1;
            var Date = DateTime.Now;
            string NewId = $"{Date.Month}{Date.Year.ToString().Substring(2)}_{i}";  // format for id MM/YYYY_Id
            while (ListId.Where(p => p.Equals(NewId)).Count() > 0)
            {
                i++;
                NewId = $"{Date.Month}{Date.Year.ToString().Substring(2)}_{i}";
            }
            return NewId;
        }
        public string FindNextIdCartItem()
        {
            var ListId = db.CartItems.ToList().Select(x => x.Id);
            int i = 1;
            var Date = DateTime.Now;
            string NewId = $"{Date.Month}{Date.Year.ToString().Substring(2)}_{i}";  // format for id MM/YYYY_Id
            while (ListId.Where(p => p.Equals(NewId)).Count() > 0)
            {
                i++;
                NewId = $"{Date.Month}{Date.Year.ToString().Substring(2)}_{i}";
            }
            return NewId;
        }

        // GET: Cart
        public ActionResult Index()
        {
            if (CurrentUserId != null)
            {
                var cart = db.Carts.Where(x => x.IdUser == CurrentUserId.UserId).FirstOrDefault();
                // Cart have already exist
                if (cart != null)
                {
                    var cartItem = db.CartItems.Where(x => x.IdCard == CurrentCartId.Id).ToList();
                    return View(new CartViewModel()
                    {
                        CartItems = cartItem,  // List CartItem
                        TotalQuantity = cartItem != null ? cartItem.Sum(x => x.Quantity) : 0,  // Sum Quantity all Item
                        TotalAmount = cartItem != null ? cartItem.Sum(x => x.Book.Price) : 0  // Sum Price all Price of Item
                    });
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddCart(string id, int quantity)
        {
            if(CurrentUserId != null)
            {
                var cart = db.Carts.Where(x => x.IdUser == CurrentUserId.UserId).FirstOrDefault();
                var product = db.Books.Where(x => x.Id == id).Select(x => x.Price).FirstOrDefault();

                // User's Cart hasn't already existed
                if (cart == null)
                {
                    cart = new Cart()
                    {
                        Id = FindNextIdCart(),
                        IdUser = CurrentUserId.UserId,
                        Total = product,
                        CreateDate = DateTime.Now,
                        IsActive = true
                    };
                    db.Carts.Add(cart);
                    db.SaveChanges();
                }

                var book = db.Books.Where(x => x.Id == id).FirstOrDefault();
                // Make sure this book already exist and on website
                if (book != null && book.IsActive == true)
                {
                    var cartItem = db.CartItems.Where(x => x.IdBook == id && x.IdCard == cart.Id).FirstOrDefault();
                    // If this book have already existed in user's cart , we have to increase Quantity . Else add new Item.
                    if (cartItem == null)
                    {
                        cartItem = new CartItem()
                        {
                            Id = FindNextIdCartItem(),
                            IdCard = cart.Id,
                            IdBook = id,
                           // Price = Convert.ToInt32(string.Format("{0:0,0}", book.Price)) , không sử dụng
                            Quantity = quantity
                        };
                        db.CartItems.Add(cartItem);
                    }
                    else
                    {
                        cartItem.Quantity += quantity;
                    }
                    db.SaveChanges();
                    return Json(new CartResponseModel()
                    {
                        Result = true,
                        Message = "Add to cart is successfully"
                    },JsonRequestBehavior.AllowGet);
                }
                return Json(new CartResponseModel()
                {
                    Result = false,
                    Message = "This book has been deleted !"
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new CartResponseModel()
            {
                Result = false,
                Message = "Please log in your account !"
            }, JsonRequestBehavior.AllowGet);
        }
        
        //Delete book
        [HttpPost]
        public ActionResult Delete(string id)
        {
            var model = db.CartItems.Where(x => x.Id == id && x.IdCard == CurrentCartId.Id).FirstOrDefault();
            if(model != null)
            {
                db.CartItems.Remove(model);
                db.SaveChanges();
                return Json(new CartResponseModel()
                {
                    Result = true,
                    Message = "Successfully !"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new CartResponseModel()
                {
                    Result = false,
                    Message = "An error is current !"
                }, JsonRequestBehavior.AllowGet);
            }
            
            
        }
    }
}