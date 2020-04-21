using LibraryDataAccess.Entities.Abstracts;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDataAccess.Entities
{
    [Table("History")]
    public class History : Entity
    {
        public History()
        { }
        public History(int bookId, int userId)
        {
            BookId = bookId;
            UserId = userId;
            DateReceiving = DateTime.Now;
        }
        public History(int id)
        {
            Id = id;
            ReturnDate = DateTime.Now;
        }
        public DateTime DateReceiving { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}