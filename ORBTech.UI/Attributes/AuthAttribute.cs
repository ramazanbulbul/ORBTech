using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ORBTech.UI.Attributes
{
    public class AuthAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["LOGON_USER"] == null)
            {
                filterContext.Result = new RedirectResult("/Admin/LoginPartial");
            }
        }
    }
}