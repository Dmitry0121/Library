using LibraryDataAccess.Entities.Abstracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDataAccess.Entities
{
    [Table("Books")]
    public class Book : Entity
    {
        public Book()
        {
            Authors = new List<Author>();
        }
        public string Name { get; set; }
        public int Count { get; set; }
        public List<Author> Authors { get; set; }
    }
}