
using Book_Management_System.Common;
using System;
using System.Web;
using System.Web.Mvc;
namespace Book_Management_System.Controllers
{
    public class UserLoginController : BaseController
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
       // GET: UserLogin/Login
        public ActionResult Login()
        {
            if (CheckCookie() == null)  // If the cookie is not exist , loading to Login page
            {
                return View();
            } 
            else                      // If The cookie is exist , Using data stored in cookie to login 
            {
                var dao = new AccountValidation();
                var result = dao.Login(CheckCookie().UserName, CheckCookie().Password);
                switch (result)
                {
                    case 1:
                        {
                            var user = dao.GetById(CheckCookie().UserName);  // Get User with UserName 
                            var userSession = new UserLogin();       // Create a object consists of Id and UserName to store into session
                            userSession.UserId = user.Id;
                            userSession.UserName = user.UserName;
                            Session.Add(Common.Constants.USER_SESSION, userSession);  // create session for login
                            Session.Add(Common.Constants.USER_ROLE, user.IdRole);    // session for authorize
                            if (user.IdRole == "1" || user.IdRole == "2")
                            {

                                return RedirectToAction("Index", "AdminHome", new { area = "Admin" });

                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                    case 0:
                            
                            break;
                    case -1:
                        {
                            ModelState.AddModelError("", Resources.Admin.Login.Resources.Block);
                            break;
                        }
                    case -2:
                        {
                            ModelState.AddModelError("", Resources.Admin.Login.Resources.Incorrect);
                            break;
                        }
                    default:
                        {
                            ModelState.AddModelError("", Resources.Admin.Login.Resources.Error);
                            break;
                        }
                }

            }
            return View();
        }
        [HttpPost]  
      [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new AccountValidation();
                var result = dao.Login(model.UserName, Encryptor.ToMD5(model.Password));  // Encrypt Password
                switch (result)
                {
                    case 1:
                        {
                            var user = dao.GetById(model.UserName);  // Get User with UserName 
                            var userSession = new UserLogin();       // Create a object consists of Id and UserName to store into session
                            userSession.UserId = user.Id;             
                            userSession.UserName = user.UserName;
                            Session.Add(Common.Constants.USER_SESSION, userSession);
                            //Session.Add(Common.Constants.USER_ROLE, user.IdRole);
                            if (model.RememberMe == true)
                            {
                                HttpCookie ckUsername = new HttpCookie("UserName");    
                                ckUsername.Expires = DateTime.Now.AddMonths(6);
                                ckUsername.Value = model.UserName;
                                Response.Cookies.Add(ckUsername);
                                HttpCookie ckPassword = new HttpCookie("Password");
                                ckPassword.Expires = DateTime.Now.AddMonths(6);
                                ckPassword.Value = Encryptor.ToMD5(model.Password);
                                Response.Cookies.Add(ckPassword);
                            }
                            if (user.IdRole == "1" || user.IdRole == "2")
                            {
                                
                                return RedirectToAction("Index", "AdminHome", new { area = "Admin" });
                               
                            }
                            else 
                            {
                                return RedirectToAction("Index", "Home");  
                            } 
                        }
                    case 0:
                        {
                            ModelState.AddModelError("", Resources.Admin.Login.Resources.Result);
                            break;
                        }
                    case -1:
                        {
                            ModelState.AddModelError("", Resources.Admin.Login.Resources.Block);
                            break;
                        }
                    case -2:
                        {
                            ModelState.AddModelError("", Resources.Admin.Login.Resources.Incorrect);
                            break;
                        }
                    default:
                        {
                            ModelState.AddModelError("", Resources.Admin.Login.Resources.Error);
                            break;
                        }
                }
              
            }
            return View();
        }

        public ActionResult Logout()
        {           
            //Remove Session 
            Session.Remove(Constants.USER_SESSION);
            Session.Remove(Constants.USER_ROLE);
            //Remove Cookie
            // Remove cookie so that the next login will not autoload index page
            if(Request.Cookies["UserName"] != null)
            {
                HttpCookie ckUsername = new HttpCookie("UserName");
                ckUsername.Expires = DateTime.Now.AddDays(-1);    
                Response.Cookies.Add(ckUsername);
            }
            if (Request.Cookies["Password"] != null)
            {
                HttpCookie ckPassword = new HttpCookie("Password");
                ckPassword.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(ckPassword);
            }
            return RedirectToAction("Index", "Home");
        }
        // Check exist of Cookie from brower
        public LoginModel CheckCookie()
        {
            var account = new LoginModel();
            string UserName, Passoword;
            if(Response.Cookies["UserName"] != null)
            {
                 UserName = Request.Cookies["UserName"].Value;
                account.UserName = UserName;
            }
            if(Response.Cookies["Password"]!= null)
            {
                Passoword = Request.Cookies["Password"].Value;
                account.Password = Passoword;
            }          
            return account;
        }
    }
}