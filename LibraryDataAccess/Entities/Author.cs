using LibraryDataAccess.Entities.Abstracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDataAccess.Entities
{
    [Table("Authors")]
    public class Author : Entity
    {
        public Author()
        {
            Books = new List<Book>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Book> Books { get; set; }
    }
}