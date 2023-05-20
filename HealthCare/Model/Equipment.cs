using HealthCare.Repository;
using HealthCare.Serialize;

namespace HealthCare.Model
{
    public enum EquipmentType
    {
        Examinational,
        Operational,
        RoomFurniture,
        HallwayFurniture
    }

    public class Equipment : IKey, ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EquipmentType Type { get; set; }
        public bool IsDynamic { get; set; }

        public Equipment() : this(0, "", EquipmentType.Examinational, false) { }
        public Equipment(int id, string name, EquipmentType type, bool dynamic)
        {
            Id = id;
            Name = name;
            Type = type;
            IsDynamic = dynamic;
        }
        public object Key
        {
            get => Id;
            set { Id = (int)value; }
        }

        public string[] Serialize()
        {
            return new string[] { Id.ToString(), Name, Type.ToString(), IsDynamic.ToString() };
        }

        public void Deserialize(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Type = Utility.Parse<EquipmentType>(values[2]);
            IsDynamic = bool.Parse(values[3]);
        }
    }
}
