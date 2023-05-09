using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public abstract class NumericService<T> : Service<T> where T : Indentifier, ISerializable, new()
    {
        protected NumericService(string filepath) : base(filepath) { }

        public new void Add(T item)
        {
            if ((int)item.Key == 0)
                AddWithNewId(item);
            else _repository.Add(item);
        }

        private int NextId()
        {
            int maxId = Count();
            foreach (var item in _repository.Items())
                maxId = Math.Max((int) item.Key, maxId);
            return maxId + 1;
        }

        public int AddWithNewId(T item)
        {
            item.Key = NextId();
            Add(item);
            return (int) item.Key;
        }
    }
}
