using LibraryDataAccess.Entities;

namespace LibraryDataAccess.Repositories.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        bool Give(Book book, History history);
        bool Return(Book book, History history);
    }
}