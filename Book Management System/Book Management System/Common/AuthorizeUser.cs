using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using static System.Net.WebRequestMethods;

namespace Book_Management_System.Common
{
    public class AuthorizeUser : ActionFilterAttribute, System.Web.Mvc.IActionFilter
    {

        // Custom property
        public string AccessLevel { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session[Constants.USER_SESSION] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"Controller","UserLogin" },
                    {"Action","Login" },
                    {"Area","" }
                });
            }
            base.OnActionExecuting(filterContext);
        }
        // Custom property
        /*public string AccessLevel { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }
            var role = new AccountValidation();
            var roleId = HttpContext.Current.Session[Constants.USER_ROLE].ToString();
            string privilegeLevels = string.Join("",role.GetByRole(roleId).RoleName); // Call another method to get rights of the user from DB

            return privilegeLevels.Contains(this.AccessLevel);
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "UserLogin",
                                action = "Login",
                                area = ""
                            })
                        );
        }
    }*/

    }
}