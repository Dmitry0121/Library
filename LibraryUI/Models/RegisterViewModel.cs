using LibraryUI.Models.Abstracts;
using System.ComponentModel.DataAnnotations;

namespace LibraryUI.Models
{
    public class RegisterViewModel : UserBaseViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Required field")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]        
        [Required(ErrorMessage = "Required field")]
        public string LastName { get; set; }
    }
}