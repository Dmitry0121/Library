using System.Collections.Generic;

namespace LibraryService.Dto
{
    public class BookDto
    {
        public BookDto()
        {
            Authors = new List<AuthorDto>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public List<AuthorDto> Authors { get; set; }
    }
}