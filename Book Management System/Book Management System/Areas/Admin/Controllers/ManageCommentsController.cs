using Book_Management_System.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book_Management_System.Areas.Admin.Controllers
{
    [AuthorizeUser]
    public class ManageCommentsController : Controller
    {
        // GET: Admin/ManageComments
        public ActionResult Index()
        {
            return View();
        }
    }
}