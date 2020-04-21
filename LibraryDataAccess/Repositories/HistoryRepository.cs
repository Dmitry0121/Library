using LibraryDataAccess.Configuration;
using LibraryDataAccess.Entities;
using LibraryDataAccess.Repositories.Abstracts;
using LibraryDataAccess.Repositories.Interfaces;

namespace LibraryDataAccess.Repositories
{
    public class HistoryRepository : Repository<History>, IHistoryRepository
    {
        public HistoryRepository(LibraryContext context) : base(context)
        {
        }
    }
}