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
    public class Inventory : NumericService<InventoryItem>
    {
        public Inventory(string filepath) : base(filepath) { }

        public int GetTotalQuantity(int equipmentId)
        {
            return GetEquipmentItems(equipmentId).Sum(x => x.Quantity);
        }

        public IEnumerable<int> GetLowQuantityEquipment(int threshold = 5)
        {
            return GetAll()
                .Where(x => x.Quantity <= threshold)
                .Select(x => x.EquipmentId);
        }

        public void RestockInventoryItem(InventoryItem item)
        {
            var found = GetAll().Find(x => x.Equals(item));

            if (found is not null) {
                found.Quantity += item.Quantity;
                Update(found);
            } else
                Add(item);
        }

        public bool TryReduceInventoryItem(InventoryItem item)
        {
            var found = GetAll().Find(x => x.Equals(item));

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
            return GetAll().Where(x => x.EquipmentId == equipmentId);
        }

        public IEnumerable<InventoryItem> GetRoomItems(int roomId)
        {
            return GetAll().Where(x => x.RoomId == roomId);
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
