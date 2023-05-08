using HealthCare.Model;
using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public abstract class NumericService<T> : Service<T> where T : ISerializable, IKey, new()
    {
        protected NumericService(string filepath) : base(filepath) { }

        public int NextId()
        {
            int maxId = int.MinValue;
            foreach (var item in _repository.Items())
                maxId = Math.Max((int) item.GetKey(), maxId);
            return maxId + 1;
        }

        public void AddWithNewId(T item)
        {
            item.SetKey(NextId());
            _repository.Items().Add(item);
        }
    }
}
