using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare
{
    enum EquipmentType
    {
        Examinational,
        Operational,
        RoomFurniture,
        HallwayFurniture
    }
    class Equipment
    {
        public string Name { get; set; }
        public EquipmentType Type { get; set; }
        public bool Dynamic { get; set; }

        public Equipment(string name, EquipmentType type, bool dynamic)
        {
            Name = name;
            Type = type;
            Dynamic = dynamic;
        }
    }
}
