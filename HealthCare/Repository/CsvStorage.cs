using System.Collections.Generic;
using HealthCare.Serialize;

namespace HealthCare.Repository
{
    public class CsvStorage<T> where T : ISerializable, new()
    {

        private string _filepath;

        public CsvStorage(string filepath)
        {
            _filepath = filepath;
        }

        public List<T> Load()
        {
            ISerializer<T> serializer = new CsvSerializer<T>();
            return serializer.DeserializeAll(_filepath);
        }

        public void Save(List<T> objects)
        {
            ISerializer<T> serializer = new CsvSerializer<T>();
            serializer.SerializeAll(_filepath, objects);
        }
    }
}
