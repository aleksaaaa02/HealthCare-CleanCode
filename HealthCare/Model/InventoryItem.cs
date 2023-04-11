using HealthCare.Exceptions;
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

        public InventoryItem() { }

        public InventoryItem(int equipmentId, int roomId, int quantity=0)
        {
            EquipmentId = equipmentId;
            RoomId = roomId;
            Quantity = quantity;
        }

        public void Add(int quantity)
        {
            Quantity += quantity;
        }

        public void Remove(int quantity)
        {
            if (Quantity >= quantity)
            {
                Quantity -= quantity;
            }
            else
            {
                throw new NotEnoughEquipmentException();
            }
        }

        public string[] ToCSV()
        {
            return new string[] { EquipmentId.ToString(), RoomId.ToString(), Quantity.ToString() };
        }

        public void FromCSV(string[] values)
        {
            EquipmentId = int.Parse(values[0]);
            RoomId = int.Parse(values[1]);
            Quantity = int.Parse(values[2]);
        }
    }
}
