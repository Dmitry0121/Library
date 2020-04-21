using LibraryUI.Models;
using System.Web;

namespace LibraryUI.Infrastructure
{
    public class CurrentUser
    {
        public static readonly int Id;
        public static readonly string Email;
        public static readonly string FullName;
        public static readonly bool IsAdmin;

        static CurrentUser()
        {
            var currentUser = HttpContext.Current.Session["CurrentUser"] as UserViewModel;
            if (currentUser != null)
            {
                Id = currentUser.Id;
                Email = currentUser.Email;
                FullName = currentUser.FullName;
                IsAdmin = currentUser.IsAdmin;
            }            
        }
    }
}