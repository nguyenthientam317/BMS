﻿using Book_Management_System.Infrastructure;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;

namespace Book_Management_System.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if(Session[ConstantDefine.CURRENT_CULTURE]!= null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session[ConstantDefine.CURRENT_CULTURE].ToString());
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session[ConstantDefine.CURRENT_CULTURE].ToString());

            }
            else
            {
                Session[ConstantDefine.CURRENT_CULTURE] = "vi-VN";
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session[ConstantDefine.CURRENT_CULTURE].ToString());
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session[ConstantDefine.CURRENT_CULTURE].ToString());
            }
        }
        [HttpPost]
        public ActionResult ChangeCulture(string ddlCulture,string returnUrl)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(ddlCulture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(ddlCulture);
            Session[ConstantDefine.CURRENT_CULTURE] = ddlCulture;
            return Redirect(returnUrl);

        }

    }

}