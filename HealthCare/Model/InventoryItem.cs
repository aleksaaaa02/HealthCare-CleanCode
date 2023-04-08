using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare
{
    class InventoryItem
    {
        public Equipment Equipment { get; set; }
        public Room Room { get; set; }
        public double Quantity { get; set; }
        public InventoryItem(Equipment equipment, Room room, double quantity)
        {
            Equipment = equipment;
            Room = room;
            Quantity = quantity;
        }
    }
}
