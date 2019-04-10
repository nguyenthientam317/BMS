using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book_Management_System.Areas.Admin.Views.Shared
{
    /// <summary>
    /// Summary description for KeepAliveAdmin
    /// </summary>
    public class KeepAliveAdmin : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}