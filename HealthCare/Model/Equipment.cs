using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare
{
    class Equipment
    {
        public string Name { get; set; }
        public bool Dynamic { get; set; }
        public enum Type
        {
            Examinational,
            Operational,
            RoomFurniture,
            HallwayFurniture
        }

        public Equipment(string name, bool dynamic)
        {
            Name = name;
            Dynamic = dynamic;
        }
    }
}
