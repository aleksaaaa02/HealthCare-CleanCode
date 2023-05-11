using HealthCare.Repository;
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

        public CsvStorage(string filepath)
        {
            _filepath = filepath;
        }

        public List<T> Load()
        {
            return Serializer<T>.FromCSV(_filepath);
        }

        public void Save(List<T> objects)
        {
            Serializer<T>.ToCSV(_filepath, objects);
        }
    }
}
