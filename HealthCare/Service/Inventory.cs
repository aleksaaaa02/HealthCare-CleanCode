using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Repository;
using HealthCare.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public class Inventory : Service<InventoryItem>
    {
        public Inventory(string filepath) : base(filepath) { }

        public int GetTotalQuantity(int equipmentId)
        {
            int quantity = 0;
            GetAll().ForEach(x => { 
                if (x.EquipmentId == equipmentId) 
                    quantity += x.Quantity; 
            });
            return quantity;
        }

        public IEnumerable<int> GetLowQuantityEquipment(int threshold = 5)
        {
            List<int> equipment = new List<int>();
            GetAll().ForEach(x => {
                if (x.Quantity < threshold)
                    equipment.Add(x.EquipmentId);
            });
            return equipment;
        }

        public void RestockInventoryItem(InventoryItem item)
        {
            InventoryItem? found = TryGet(item.Key);

            if (found is not null) {
                found.Quantity += item.Quantity;
                Update(found);
            } else
                Add(item);
        }

        public bool TryReduceInventoryItem(InventoryItem item)
        {
            InventoryItem? found = TryGet(item.Key);

            if (found is null || found.Quantity < item.Quantity)
                    return false;

            found.Quantity -= item.Quantity;
            if (found.Quantity == 0)
                Remove(found.Key);
            else
                Update(found);
            return true;
        }

        public IEnumerable<InventoryItem> GetEquipmentItems(int equipmentId)
        {
            List<InventoryItem> items = new List<InventoryItem>();
            GetAll().ForEach(x => {
                if (x.EquipmentId == equipmentId)
                    items.Add(x);
            });
            return items;
        }
        public IEnumerable<InventoryItem> GetRoomItems(int roomId)
        {
            return GetAll().FindAll(x => x.RoomId==roomId);
        }

        public void ChangeDynamicEquipmentQuantity(Dictionary<int, int> newQuantites)
        {
            foreach(KeyValuePair<int, int> entry in newQuantites)
            {
                InventoryItem item = Get(entry.Key);
                item.Quantity = entry.Value;
                Update(item);
            }
        }

    }
}
