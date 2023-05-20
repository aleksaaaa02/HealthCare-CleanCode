using System.Collections.Generic;
using HealthCare.Serialize;

namespace HealthCare.Repository
{
    public class FileRepository<T> : IRepository<T> where T : IKey, ISerializable, new()
    {
        private readonly ISerializer<T> _serializer;
        private readonly string _filepath;

        // TODO remove in services
        public FileRepository(string filepath) : this(new CsvSerializer<T>(), filepath) { }

        public FileRepository(ISerializer<T> serializer, string filepath)
        {
            _serializer = serializer;
            _filepath = filepath;
        }

        public T? Get(object key)
        {
            return Load().Find(x => x.Key.Equals(key));
        }

        public virtual void Add(T item)
        {
            if (Contains(item.Key)) return;

            var items = Load();
            items.Add(item);
            Save(items);
        }

        public void Remove(object key)
        {
            var items = Load();
            items.RemoveAll(x => x.Key.Equals(key));
            Save(items);
        }

        public void Update(T item)
        {
            object key = item.Key;
            if (!Contains(key)) return;

            var items = Load();
            int i = items.FindIndex(x => x.Key.Equals(key));
            items[i] = item;
            Save(items);
        }

        public bool Contains(object key)
        {
            return Get(key) is not null;
        }

        public int Count()
        {
            return Load().Count;
        }

        public List<T> Load()
        {
            return _serializer.DeserializeAll(_filepath);
        }

        private void Save(List<T> items)
        {
            _serializer.SerializeAll(_filepath, items);
        }
    }
}
