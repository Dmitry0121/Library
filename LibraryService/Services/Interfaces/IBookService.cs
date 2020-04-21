using LibraryService.Dto;

namespace LibraryService.Services.Interfaces
{
    public interface IBookService : IServiceRead<BookDto>, IServiceWrite<BookDto>
    {
        bool Give(int bookId, int userId);
        bool Return(int bookId, int historyId);
    }
}