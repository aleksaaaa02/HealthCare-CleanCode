using System.Collections.Generic;
using HealthCare.Serialize;

namespace HealthCare.Repository
{
    public interface IRepository<T> where T : RepositoryItem
    {
        T? Get(object key);
        void Add(T item);
        void Remove(object key);
        void Update(T item);
        bool Contains(object key);
        int Count();
        List<T> Load();
    }
}
