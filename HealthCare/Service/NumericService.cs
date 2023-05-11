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
