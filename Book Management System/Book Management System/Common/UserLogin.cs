using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book_Management_System.Common
{
    [Serializable]
    public class UserLogin
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}