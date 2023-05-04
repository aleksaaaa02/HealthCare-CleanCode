using HealthCare.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    public class OrderItem : ISerializable
    {
        public int Id { get; set; }
        public string EquipmentName { get; set; }
        public int Quantity { get; set; }
        DateTime Scheduled { get; set; }

        public OrderItem()
        {
            Scheduled = DateTime.Now;    
        }

        public OrderItem(int id, string equipmentId, int quantity, DateTime scheduled)
        {
            Id = id;
            EquipmentName = equipmentId;
            Quantity = quantity;
            Scheduled = scheduled;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            EquipmentName = values[1];
            Quantity = int.Parse(values[2]);
            Scheduled = DateTime.Parse(values[3]);
        }

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), EquipmentName.ToString(), Quantity.ToString(), Scheduled.ToString() };
        }

        internal void Copy(OrderItem item)
        {
            Id = item.Id;
            EquipmentName = item.EquipmentName;
            Quantity = item.Quantity;
            Scheduled = item.Scheduled;
        }
    }
}
