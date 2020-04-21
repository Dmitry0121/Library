using LibraryUI.Models;
using System.Web;
using System.Web.Mvc;

namespace LibraryUI.Infrastructure
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        public MyAuthorizeAttribute()
        { 
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var currentUser = HttpContext.Current.Session["CurrentUser"] as UserViewModel;
            if (currentUser != null)
            {
                return true;
            }

            return false;
        }
    }
}