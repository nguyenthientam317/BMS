using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book_Management_System.Common
{
    public class Register
    {
        public string IdAccount { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string IdRole { get; set; }
        public bool IsActiveAccount { get; set; }

        public string IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool Gender { get; set; }
        public bool IsActiveUser { get; set; }
        public string Avatar { get; set; }
    }
}