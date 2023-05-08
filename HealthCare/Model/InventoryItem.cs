using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    public class InventoryItem : ISerializable, IKey
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int RoomId { get; set; }
        public int Quantity { get; set; }
        public InventoryItem() : this(0) { }
        public InventoryItem(int id) : this(id, 0, 0, 0) { }
        public InventoryItem(int id, int equipmentId, int roomId, int quantity)
        {
            Id = id;
            EquipmentId = equipmentId;
            RoomId = roomId;
            Quantity = quantity;
        }

        public string[] ToCSV()
        {
            return new string[] {
                Id.ToString(), 
                EquipmentId.ToString(), 
                RoomId.ToString(), 
                Quantity.ToString()};
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            EquipmentId = int.Parse(values[1]);
            RoomId = int.Parse(values[2]);
            Quantity = int.Parse(values[3]);
        }

        public object GetKey()
        {
            return Id;
        }

        public void SetKey(object key)
        {
            Id = (int) key;
        }
    }
}
