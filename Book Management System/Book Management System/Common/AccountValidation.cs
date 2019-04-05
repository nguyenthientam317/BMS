using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Book_Management_System.Models;

namespace Book_Management_System.Common
{
    public class AccountValidation
    {
        
        Model db = new Model();

        //GET: right of user
        public Role GetByRole(string Id)
        {
            return db.Roles.Where(x=>x.Id == Id).FirstOrDefault();
        }

        //GET: account information by username
        public Account GetById(string Username)
        {
            return db.Accounts.SingleOrDefault(x => x.UserName == Username);
        }

        //Check account is true or false
        public int Login(string Username,string Password)
        {
            var model = db.Accounts.Where(x => x.UserName == Username ).FirstOrDefault();
            if (model == null)
            {
                return 0;
            }
            else
            {
                if(model.IsActive == false)
                {
                    return -1;
                }
                else
                {
                    if(model.Password == Password)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
           
        }

        /*Check user existed or not*/

        public Account CheckUserName(string userName)
        {
            return db.Accounts.SingleOrDefault(x => x.UserName == userName);
        }

        /*Check email existed or not */
        public User CheckEmail(string email)
        {
            return db.Users.SingleOrDefault(x => x.Email == email);
        }

        public string GyByEmail(string id)
        {
            return db.Users.Where(x=>x.Id == id).Select(x=>x.Email).FirstOrDefault();
        }
    }

    }

