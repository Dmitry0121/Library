using LibraryUI.Models.Abstracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryUI.Models
{
    public class UsersViewModel
    {
        public UsersViewModel()
        {
            Users = new List<UserViewModel>();
        }

        public List<UserViewModel> Users;
    }

    public class UserViewModel : UserBaseViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Required field")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Required field")]
        public string LastName { get; set; }

        public string FullName 
        { 
            get 
            { 
                return $"{FirstName} {LastName}"; 
            } 
        }
        public int RoleId { get; set; }
        public RoleViewModel Role { get; set; }
        public bool IsAdmin
        {
            get
            {
                return Role.Name.Equals("Admin");
            }
        }
    }
}