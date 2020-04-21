using System;

namespace LibraryService.Dto
{
    public class HistoryDto
    {
        public int Id { get; set; }
        public DateTime DateReceiving { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int BookId { get; set; }
        public BookDto Book { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
    }
}