using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Repository;
using HealthCare.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public class Inventory : NumericService<InventoryItem>
    {
        public Inventory(string filepath) : base(filepath) { }

        public int GetTotalQuantity(int equipmentId)
        {
            int quantity = 0;
            foreach (var item in _repository.Items())
                if (item.EquipmentId == equipmentId)
                    quantity += item.Quantity;
            return quantity;
        }

        public IEnumerable<int> GetLowQuantityEquipment(int threshold = 5)
        {
            List<int> equipment = new List<int>();
            foreach (var item in _repository.Items())
                if (item.Quantity <= threshold)
                    equipment.Add(item.EquipmentId);
            return equipment;
        }

        public void RestockInventoryItem(InventoryItem item)
        {
            InventoryItem? found = GetAll().Find(x => 
                x.EquipmentId==item.EquipmentId && 
                x.RoomId==item.RoomId);

            if (found is not null) {
                found.Quantity += item.Quantity;
                Update(found);
            } else {
                Add(item);
            }
        }
        public IEnumerable<InventoryItem> GetRoomItems(int roomId)
        {
            return GetAll().FindAll(x => x.RoomId==roomId);
        }
    }
}
