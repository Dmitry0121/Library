using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LibraryUI.Models
{
    public class AuthorsViewModel
    {
        public AuthorsViewModel()
        {
            Authors = new List<AuthorViewModel>(); 
        }

        public List<AuthorViewModel> Authors;
    }

    public class AuthorViewModel
    {
        public AuthorViewModel()
        {
            Books = new List<BookViewModel>();
            BooksIds = new List<int>();
            BooksList = new List<SelectListItem>();
        }

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

        [Display(Name = "Books")]
        public List<BookViewModel> Books { get; set; }

        public List<int> BooksIds { get; set; }
        public List<SelectListItem> BooksList { get; set; }
    }
}