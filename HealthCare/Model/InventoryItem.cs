using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    public class InventoryItem : Indentifier, ISerializable
    {
        public override object Key {
            get => Tuple.Create(EquipmentId, RoomId);
            set
            {
                EquipmentId = ((Tuple<int, int>) value).Item1;
                RoomId = ((Tuple<int, int>)value).Item2;
            } 
        }
        public int EquipmentId { get; set; }
        public int RoomId { get; set; }
        public int Quantity { get; set; }
        public InventoryItem() : this(0, 0, 0) { }
        public InventoryItem(int equipmentId, int roomId, int quantity)
        {
            EquipmentId = equipmentId;
            RoomId = roomId;
            Quantity = quantity;
        }

        public string[] ToCSV()
        {
            return new string[] {
                EquipmentId.ToString(), 
                RoomId.ToString(), 
                Quantity.ToString()};
        }

        public void FromCSV(string[] values)
        {
            EquipmentId = int.Parse(values[0]);
            RoomId = int.Parse(values[1]);
            Quantity = int.Parse(values[2]);
        }
    }
}
