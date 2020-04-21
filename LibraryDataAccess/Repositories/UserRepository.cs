using LibraryDataAccess.Configuration;
using LibraryDataAccess.Entities;
using LibraryDataAccess.Repositories.Abstracts;
using LibraryDataAccess.Repositories.Interfaces;

namespace LibraryDataAccess.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(LibraryContext context) : base(context)
        {
        }
    }
}