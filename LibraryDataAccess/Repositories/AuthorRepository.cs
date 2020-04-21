using LibraryDataAccess.Configuration;
using LibraryDataAccess.Entities;
using LibraryDataAccess.Repositories.Abstracts;
using LibraryDataAccess.Repositories.Interfaces;

namespace LibraryDataAccess.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryContext context) : base(context)
        {
        }
    }
}