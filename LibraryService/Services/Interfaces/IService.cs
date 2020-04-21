using System.Collections.Generic;

namespace LibraryService.Services.Interfaces
{
    public interface IServiceRead<T>
        where T : class
    {
        List<T> Get();
        T Get(int id);
    }

    public interface IServiceWrite<T>
        where T : class
    {
        bool Create(T item);
        bool Update(T item);
        bool Delete(int id);
    }
}