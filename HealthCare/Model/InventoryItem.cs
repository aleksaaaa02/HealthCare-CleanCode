using HealthCare.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    public class InventoryItem : ISerializable
    {
        public int EquipmentId { get; set; }
        public int RoomId { get; set; }
        public int Quantity { get; set; }
        public InventoryItem() : this(new Equipment(), new Room(), 0) { }
        public InventoryItem(Equipment equipment, Room room, int quantity)
        {
            EquipmentId = equipmentId;
            RoomId = roomId;
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
