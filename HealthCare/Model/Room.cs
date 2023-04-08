using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare
{
    enum RoomType
    {
        Examinational,
        Operational,
        PatientCare,
        Reception,
        Warehouse
    }
    class Room
    {
        public string Name { get; set; }
        public RoomType Type { get; set; }
        public Room(string name, RoomType type)
        {
            Name = name;
            Type = type;
        }

        public override bool Equals(object? obj)
        {
            return obj is Room room &&
                   Name == room.Name;
        }
    }
}
