using HealthCare.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Storage
{
    public class CsvStorage<T> where T : ISerializable, new()
    {

        private string _filepath;

        private Serializer<T> _serializer;

        public CsvStorage(string filepath)
        {
            _serializer = new Serializer<T>();
            _filepath = filepath;
        }

        public List<T> Load()
        {
            return _serializer.FromCSV(_filepath);
        }

        public void Save(List<T> objects)
        {
            _serializer.ToCSV(_filepath, objects);
        }
    }
}
