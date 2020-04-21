using LibraryDataAccess.Configuration;
using LibraryDataAccess.Entities;
using LibraryDataAccess.Repositories.Abstracts;
using LibraryDataAccess.Repositories.Interfaces;
using Logger;
using System;
using System.Data.Entity;
using System.Linq;

namespace LibraryDataAccess.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(LibraryContext context) : base(context)
        {
        }

        public bool Give(Book book, History history)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _dbSet.Attach(book);
                    _context.Entry(book).Property(x => x.Count).IsModified = true;
                    _context.Set<History>().Add(history);
                    Save();

                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    NLogger.Write().Error($"Message:{e.Message}");
                    return false;
                }
                finally 
                {
                    var bookLocal = _dbSet.Local.FirstOrDefault(x => x.Id.Equals(book.Id));
                    if (bookLocal != null)
                    {
                        _context.Entry(bookLocal).State = EntityState.Detached;
                    }

                    var historyLocal = _context.Set<History>().Local.FirstOrDefault(x => x.Id.Equals(history.Id));
                    if (historyLocal != null)
                    {
                        _context.Entry(historyLocal).State = EntityState.Detached;
                    }
                }
            }
        }

        public bool Return(Book book, History history)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _dbSet.Attach(book);
                    _context.Entry(book).Property(x => x.Count).IsModified = true;
                    _context.Set<History>().Attach(history);
                    _context.Entry(history).Property(x => x.ReturnDate).IsModified = true;
                    Save();

                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    NLogger.Write().Error($"Message:{e.Message}");
                    return false;
                }
                finally
                {
                    var local = _dbSet.Local.FirstOrDefault(x => x.Id.Equals(book.Id));
                    if (local != null)
                    {
                        _context.Entry(local).State = EntityState.Detached;
                    }

                    var historyLocal = _context.Set<History>().Local.FirstOrDefault(x => x.Id.Equals(history.Id));
                    if (historyLocal != null)
                    {
                        _context.Entry(historyLocal).State = EntityState.Detached;
                    }
                }
            }
        }        
    }
}