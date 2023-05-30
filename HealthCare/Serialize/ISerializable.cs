namespace HealthCare.Serialize
{
    public interface ISerializable
    {
        string[] Serialize();

        void Deserialize(string[] values);
    }
}