using HealthCare.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    public class InventoryItem : ISerializable
    {
        public Equipment Equipment { get; set; }
        public Room Room { get; set; }
        public int Quantity { get; set; }
        public InventoryItem() : this(new Equipment(), new Room(), 0) { }
        public InventoryItem(Equipment equipment, Room room, int quantity)
        {
            Equipment = equipment;
            Room = room;
            Quantity = quantity;
        }

        public string[] ToCSV()
        {
            return new string[] { Equipment.Name, Room.Name, Quantity.ToString() };
        }

        public void FromCSV(string[] values)
        {
            Equipment = new Equipment();
            Room = new Room();

            Equipment.Name = values[0];
            Room.Name = values[1];
            Quantity = int.Parse(values[2]);
        }

        internal void Copy(InventoryItem item)
        {
            Quantity = item.Quantity;
        }
    }
}
