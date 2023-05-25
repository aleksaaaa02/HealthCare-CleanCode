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
            return Serializer<T>.DeserializeAll(_filepath);
        }

        public void Save(List<T> objects)
        {
            Serializer<T>.SerializeAll(_filepath, objects);
        }
    }
}
