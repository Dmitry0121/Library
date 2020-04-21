using System.Collections.Generic;

namespace LibraryService.Dto
{
    public class AuthorDto
    {
        public AuthorDto()
        {
            Books = new List<BookDto>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<BookDto> Books { get; set; }
    }
}