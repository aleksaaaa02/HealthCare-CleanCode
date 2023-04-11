using System.Linq;

namespace HealthCare.Serializer
{
    public interface ISerializable
    {
        string[] ToCSV();

        void FromCSV(string[] values);

        private string[] _trim(string[] values)
        {
            return values.Select(v => v = v.Trim()).ToArray();
        }
    }
}
