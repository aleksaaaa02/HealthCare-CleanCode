using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public abstract class NumericService<T> : Service<T> where T : Indentifier, ISerializable, new()
    {
        protected NumericService(string filepath) : base(filepath) { }

        private int NextId()
        {
            int maxId = int.MinValue;
            foreach (var item in _repository.Items())
                maxId = Math.Max((int) item.Key, maxId);
            return maxId + 1;
        }

        public int AddWithNewId(T item)
        {
            if (DefragmentationNeeded())
                DefragmentIds();

            item.Key = NextId();
            Add(item);
            return (int) item.Key;
        }

        private bool DefragmentationNeeded()
        {
            return NextId() - _repository.Count() >= Global.maxIdGap;
        }

        private void DefragmentIds()
        {
            int i = 1;
            foreach (var item in _repository.Items())
                item.Key = i++;
        }
    }
}
