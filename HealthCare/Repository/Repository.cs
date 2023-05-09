using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthCare.Repository
{
    public class Repository<T> where T : Indentifier, ISerializable, new()
    {
        private readonly string _filepath;

        public Repository(string filepath)
        {
            _filepath = filepath;
        }

        public T? Get(object key)
        {
            return Items().Find(x => x.Key.Equals(key));
        }

        public virtual void Add(T item)
        {
            if (Contains(item.Key)) return;

            var items = Items();
            items.Add(item);
            _save(items);
        }

        public void Remove(object key)
        {
            var items = Items();
            items.RemoveAll(x => x.Key.Equals(key));
            _save(items);
        }

        public void Update(T item)
        {
            object key = item.Key;
            if (!Contains(key)) return;

            var items = Items();
            int i = items.FindIndex(x => x.Key.Equals(key));
            items[i] = item;
            _save(items);
        }

        public bool Contains(object key)
        {
            return Get(key) is not null;
        }

        public int Count()
        {
            return Items().Count;
        }

        public List<T> Items()
        {
            return Serializer<T>.FromCSV(_filepath);
        }

        private void _save(List<T> items)
        {
            Serializer<T>.ToCSV(_filepath, items);
        }
    }
}
