using HealthCare.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    enum EquipmentType
    {
        Examinational,
        Operational,
        RoomFurniture,
        HallwayFurniture
    }
    class Equipment : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EquipmentType Type { get; set; }
        public bool Dynamic { get; set; }

        public Equipment(int id, string name, EquipmentType type, bool dynamic)
        {
            Id = id;
            Name = name;
            Type = type;
            Dynamic = dynamic;
        }

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), Name, Type.ToString(), Dynamic.ToString() };
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Type = (EquipmentType) Enum.Parse(typeof(EquipmentType), values[2]);
            Dynamic = bool.Parse(values[3]);
        }
    }
}
