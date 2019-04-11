using Book_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book_Management_System.Common
{
    public class OrderItemViewModel
    {

        public string Id { get; set; }
        public string IdCard { get; set; }
        public string Status { get; set; }
        public string MethodPayment { get; set; }
        public DateTime CreateDate { get; set; }
        public double TotalPrice { get; set; }

        public IEnumerable<CartItem> ListCartItem;






    }
}