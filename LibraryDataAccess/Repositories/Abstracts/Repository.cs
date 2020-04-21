using LibraryDataAccess.Entities.Interfaces;
using LibraryDataAccess.Repositories.Interfaces;
using Logger;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace LibraryDataAccess.Repositories.Abstracts
{
    public class Repository<T> : IRepository<T>
        where T : class, IEntity
    {
        protected readonly DbSet<T> _dbSet;
        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public List<T> Get(params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                return Include(includeProperties).ToList();
            }
            catch (Exception e)
            {
                NLogger.Write().Error($"Message:{e.Message}");
                return new List<T>();
            }
        }

        public List<T> Get(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                var query = Include(includeProperties);
                return query.AsNoTracking().Where(predicate).ToList();
            }
            catch (Exception e)
            {
                NLogger.Write().Error($"Message:{e.Message}");
                return new List<T>();
            }
        }

        public bool Create(T item)
        {
            try
            {
                _dbSet.Add(item);
                Save();
                return true;
            }
            catch (Exception e)
            {
                NLogger.Write().Error($"Message:{e.Message}");
                return false;
            }
        }

        public bool Update(T item)
        {
            try
            {
                _context.Entry(item).State = EntityState.Modified;
                Save();
                return true;
            }
            catch (Exception e)
            {
                NLogger.Write().Error($"Message:{e.Message}");
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                _context.Entry(Get(x => x.Id.Equals(id)).Single()).State = EntityState.Deleted;
                Save();
                return true;
            }
            catch (Exception e)
            {
                NLogger.Write().Error($"Message:{e.Message}");
                return false;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}