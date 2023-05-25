using HealthCare.Repository;
using HealthCare.Serialize;
using System.Linq;

namespace HealthCare.Service
{
    public abstract class NumericService<T> : Service<T> where T : RepositoryItem, new()
    {
        public NumericService(string filepath) : base(filepath) { }
        public NumericService(IRepository<T> repository) : base(repository) { }

        public new void Add(T item)
        {
            if ((int)item.Key == 0)
                item.Key = NextId();

            base.Add(item);
        }

        private int NextId()
        {
            var max = GetAll().Max(x => x.Key);
            if (max is null) return 1;
            return (int)max + 1;
        }
    }
}
