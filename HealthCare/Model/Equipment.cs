using HealthCare.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    public enum EquipmentType
    {
        Examinational,
        Operational,
        RoomFurniture,
        HallwayFurniture
    }
    public class Equipment : ISerializable
    {
        public string Name { get; set; }
        public EquipmentType Type { get; set; }
        public bool Dynamic { get; set; }

        public Equipment() : this("", EquipmentType.Examinational, false) { }

        public Equipment(string name, EquipmentType type, bool dynamic)
        {
            Name = name;
            Type = type;
            Dynamic = dynamic;
        }

        public string[] ToCSV()
        {
            return new string[] { Name, Type.ToString(), Dynamic.ToString() };
        }

        public void FromCSV(string[] values)
        {
            Name = values[0];
            Type = (EquipmentType) Enum.Parse(typeof(EquipmentType), values[1]);
            Dynamic = bool.Parse(values[2]);
        }

        internal void Copy(Equipment equipment)
        {
            Name = equipment.Name;
            Type = equipment.Type;
            Dynamic = equipment.Dynamic;
        }

        public string TranslateType()
        {
            switch(Type)
            {
                case EquipmentType.Examinational:
                    return "za preglede";
                case EquipmentType.Operational:
                    return "operaciona";
                case EquipmentType.RoomFurniture:
                    return "sobni namestaj";
                case EquipmentType.HallwayFurniture:
                    return "oprema za hodnike";
            }
            return "";
        }
    }
}
