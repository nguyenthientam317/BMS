using Book_Management_System.Common;
using Book_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
            get { return db.Carts.Where(x => x.IdUser == CurrentUserId.UserId).FirstOrDefault(); }
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
        public ActionResult IndexTest()
        {
            if (CurrentUserId != null)
            {
                var cart = db.Carts.Where(x => x.IdUser == CurrentUserId.UserId).FirstOrDefault();
                // Cart have already exist
                if (cart != null)
                {
                    double total = 0;
                    var cartItem = db.CartItems.Where(x => x.IdCard == CurrentCartId.Id).ToList();                  
                    // Sum Price all Price of Item
                    foreach (var item in cartItem)
                    {
                        total += item.Book.Price * item.Quantity;
                    }
                    return PartialView(new CartViewModel()
                    {
                        CartItems = cartItem,  // List CartItem
                        TotalQuantity = cartItem != null ? cartItem.Count() : 0,  // Sum Quantity all Item
                        TotalAmount = cartItem != null ? total : 0
                    });
                }
            }
            return PartialView(new CartViewModel()
            {
                CartItems = null,  // List CartItem
                TotalQuantity =  0,  // Sum Quantity all Item
                TotalAmount =  0
            });
        }
        // GET: Cart
        [AuthorizeUser]
        public ActionResult Index()
        {

                return View();

        }
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
                            IsActive = true,
                           // Price = Convert.ToInt32(string.Format("{0:0,0}", book.Price)) , không sử dụng
                            Quantity = quantity,
       
                        };
                        db.CartItems.Add(cartItem);

                    }
                    else
                    {
                        cartItem.Quantity += quantity;
                    }
                    db.SaveChanges();

                    var listItem = db.CartItems.Where(x => x.IdCard == cart.Id).ToList();

                    double total = 0;
                    foreach (var item in listItem)
                    {
                        total += (item.Quantity * item.Book.Price); // sum value of items
                    }
                    return Json(new CartResponseModel()
                    {
                        Total = total,
                        Amount = listItem.Count(),
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
        
        //Get detail product without go to Detail page   
        public ActionResult DetailCart(string id)
        {
            var model = db.Books.Find(id);
            return PartialView("DetailCart",model);
        }
        // Update quantity and price of book.
        public ActionResult UpdateCart(string id,int quantity)
        {
            using (DbContextTransaction tran = db.Database.BeginTransaction())
            {
                try
                {
                    var model = db.CartItems.Where(x => x.IdCard == CurrentCartId.Id && x.IdBook == id).FirstOrDefault();
                    model.Quantity = quantity;

                    var newValue = (model.Quantity * model.Book.Price); //  new value of this book

                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    tran.Commit();

                    double total = 0;
                    var amount = db.CartItems.Where(x => x.IdCard == CurrentCartId.Id).ToList();
                    foreach (var item in amount)
                    {
                        total += (item.Quantity * item.Book.Price); // sum value of items
                    }

                    return Json(new UpdateCart() {
                        Result = true,
                        Message = "Update cart is successful.",
                        Total = total,
                        New = newValue
                    }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    tran.Rollback();
                    return Json(new UpdateCart()
                    {
                        Result = false,
                        Message = "Update cart is failed.",
                    }, JsonRequestBehavior.AllowGet);
                } 
            }
        
        }

        //Delete book
        public ActionResult Delete(string id)
        {

            var model = db.CartItems.Where(x => x.IdBook == id && x.IdCard == CurrentCartId.Id).FirstOrDefault();
            if (model != null)
            {
                using (DbContextTransaction tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.CartItems.Remove(model);
                        db.SaveChanges();
                        tran.Commit();

                        double total = 0;
                        var amount = db.CartItems.Where(x => x.IdCard == CurrentCartId.Id).ToList();
                        foreach (var item in amount)
                        {
                            total += (item.Quantity * item.Book.Price);
                        }
                        return RedirectToAction("IndexTest");
                    }
                    catch
                    {
                        tran.Rollback();
                        return RedirectToAction("IndexTest");
                    }
                }
            }
            else
            {
                return RedirectToAction("IndexTest");
            }
        }
    }
}