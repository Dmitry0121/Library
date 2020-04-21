using LibraryDataAccess.Configuration;
using LibraryDataAccess.Entities;
using LibraryDataAccess.Repositories.Abstracts;
using LibraryDataAccess.Repositories.Interfaces;

namespace LibraryDataAccess.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(LibraryContext context) : base(context)
        {
        }
    }
}