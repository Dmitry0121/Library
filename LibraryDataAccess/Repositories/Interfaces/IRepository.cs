using LibraryDataAccess.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LibraryDataAccess.Repositories.Interfaces
{
    public interface IRepository<T>
        where T : class, IEntity
    {
        List<T> Get(params Expression<Func<T, object>>[] includeProperties);
        List<T> Get(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties);
        bool Create(T item);
        bool Update(T item);
        bool Delete(int id);
        void Save();
    }
}