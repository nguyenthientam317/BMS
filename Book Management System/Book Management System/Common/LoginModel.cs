using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Book_Management_System.Common
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter an Username")]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter a Password")]
        [StringLength(200)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}