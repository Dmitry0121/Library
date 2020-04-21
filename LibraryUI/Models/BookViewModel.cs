using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace LibraryUI.Models
{
    public class BooksViewModel
    {
        public BooksViewModel()
        {
            Books = new List<BookViewModel>();
        }

        public List<BookViewModel> Books;
    }

    public class BookViewModel
    {
        public BookViewModel()
        {
            Authors = new List<AuthorViewModel>();
            AuthorsIds = new List<int>();
            AuthorsList = new List<SelectListItem>();
        }

        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Required field")]
        public string Name { get; set; }

        [Display(Name = "Count")]
        [Required(ErrorMessage = "Required field")]
        public int Count { get; set; }

        [Display(Name = "Authors")]
        public List<AuthorViewModel> Authors { get; set; }

        public string AllAuthors
        {
            get
            {
                return String.Join(", ", Authors.Select(x => x.FullName));
            }
        }
        public bool IsAvailable
        {
            get
            {
                return Count > 0;
            }
        }
        public List<int> AuthorsIds { get; set; }
        public List<SelectListItem> AuthorsList { get; set; }
    }
}