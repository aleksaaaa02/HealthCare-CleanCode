using System.Collections.Generic;

namespace HealthCare.Serialize
{
    public interface ISerializer<T> where T : ISerializable
    {
        void SerializeAll(string filepath, List<T> objects);
        List<T> DeserializeAll(string filepath);
    }
}
