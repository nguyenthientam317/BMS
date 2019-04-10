using Book_Management_System.Common;
using Book_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book_Management_System.Controllers
{
    public class RegisterController : Controller
    {
        private Model db = new Model();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        // automatically increate Id
        public string FindNextIdAccount()
        {
            var ListId = db.Accounts.ToList().Select(x => x.Id);
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
        [AllowAnonymous]
        // Find UserName is already exist
        public JsonResult IsUserNameUnique(string UserName)
        {
            var result = new AccountValidation();
            if (result.CheckUserName(UserName) != null)
            {
                var MyDictionary = new Dictionary<string, bool>();
                MyDictionary.Add("valid", false);
                return Json(MyDictionary, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var MyDictionary = new Dictionary<string, bool>();
                MyDictionary.Add("valid", true);
                return Json(MyDictionary, JsonRequestBehavior.AllowGet);
            }

        }
        [AllowAnonymous]
        // Find Email is already exist
        public JsonResult IsEmailUnique(string email, string id)
        {
            var result = new AccountValidation();
            if (result.CheckEmail(email) != null && !email.Equals(result.GyByEmail(id)))
            {
                var MyDictionary = new Dictionary<string, bool>();
                MyDictionary.Add("valid", false);
                return Json(MyDictionary, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var MyDictionary = new Dictionary<string, bool>();
                MyDictionary.Add("valid", true);
                return Json(MyDictionary, JsonRequestBehavior.AllowGet);
            }

        }
        
        public ActionResult Create()
        {
            return View();
        }
        // POST: Admin/ManageAccount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAccount,UserName,Password,IdRole,IsActiveAccount,IdUser,FirstName,LastName,Birthday,Email,Address,Gender,IsActiveUser,Avatar")] Register register)
        {
            using (DbContextTransaction tran = db.Database.BeginTransaction())
            {
                try
                {
                    Account account = new Account();
                    //Insert data to Account table
                    account.Id = FindNextIdAccount();
                    account.UserName = register.UserName;
                    account.Password = Encryptor.ToMD5(register.Password);   // Encrypt Password
                    account.IdRole = "3"; //Set default role is User
                    account.IsActive = true;
                    //Add to db
                    db.Accounts.Add(account);
                    db.SaveChanges();
                    
                    User user = new User();
                    //Insert data to User table
                    user.Id = account.Id; //UserID and AccountID be the same
                    user.FirstName = register.FirstName;
                    user.LastName = register.LastName;
                    user.Birthday = Convert.ToDateTime(register.Birthday); //Convert birthday to MM/DD/YYYY HH:mm:ss A
                    user.Email = register.Email;
                    user.Address = register.Address;
                    user.Gender = register.Gender;
                    user.IsActive = true;

                    //Map path Avatar
                    string link = Server.MapPath(@"~\Assets\user-avatar\" + user.Id);
                    // Check directory existed or not
                    if (!Directory.Exists(link))
                    {
                        Directory.CreateDirectory(Server.MapPath(@"~\Assets\user-avatar\" + user.Id));
                    }
                    var f = Request.Files["Avatar"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string fullPath = link + f.FileName;
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                        var path = (link + @"\" + f.FileName);
                        f.SaveAs(path);
                        user.Avatar = (link + @"\" + f.FileName);
                    }
                    //Add to db
                    db.Users.Add(user);
                    db.SaveChanges();
                    //Commit transaction
                    tran.Commit();       
                }
                catch(Exception ex)
                {
                    tran.Rollback();
                }
            }
            return RedirectToAction("Login","UserLogin");
        }
    }
}