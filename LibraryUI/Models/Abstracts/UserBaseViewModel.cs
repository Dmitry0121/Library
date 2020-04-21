using System.ComponentModel.DataAnnotations;

namespace LibraryUI.Models.Abstracts
{
    public abstract class UserBaseViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Required field")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Wrong email")]
        public string Email { get; set; }
    }
}