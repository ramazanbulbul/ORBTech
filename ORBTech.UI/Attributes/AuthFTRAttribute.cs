using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ORBTech.UI.Attributes
{
    public class AuthFTRAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["LOGON_FATURA_USER"] == null)
            {
                filterContext.Result = new RedirectResult("/FTR/LoginPartial");
            }
        }
    }
}