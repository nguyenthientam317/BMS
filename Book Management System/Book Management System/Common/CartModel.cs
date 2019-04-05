using Book_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book_Management_System.Common
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public double TotalAmount { get; set; }
        public int TotalQuantity { get; set; }
    }
    //Object return Layout
    public class CartResponseModel
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public double Total { get; set; }
    }

}