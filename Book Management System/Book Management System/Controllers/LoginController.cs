
using Book_Management_System.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Book_Management_System.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new AccountValidation();
                var result = dao.Login(model.UserName, Encryptor.ToMD5(model.Password));
                switch (result)
                {
                    case 1:
                        {
                            var user = dao.GetById(model.UserName);
                            var userSession = new UserLogin();
                            userSession.UserId = user.Id;
                            userSession.UserName = user.UserName;
                            Session.Add(Common.Constants.USER_SESSION, userSession);
                            if (user.IdRole == "1" || user.IdRole == "2")
                            {
                                return RedirectToAction("Index", "AdminHome", new { area = "Amin" });
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            break;
                        }
                    case 0:
                        {
                            ModelState.AddModelError("", "Account is not exit !");
                            break;
                        }
                    case -1:
                        {
                            ModelState.AddModelError("", "Account was blocked !");
                            break;
                        }
                    case -2:
                        {
                            ModelState.AddModelError("", "Password is incorrect !");
                            break;
                        }
                    default:
                        {
                            ModelState.AddModelError("", "An error occurred !");
                            break;
                        }
                }
                return View("Index", "Login");
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove(Constants.USER_SESSION);
            return RedirectToAction("Index", "Home");
        }
    }
}