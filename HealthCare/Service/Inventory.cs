using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare
{
    class Inventory
    {
        public List<InventoryItem> Items { get; }

        public Inventory()
        {
            Items = new List<InventoryItem>();
        }

        public InventoryItem? GetInventoryItem(Equipment equipment, Room room)
        {
            return Items.Find(x => x.Room == room && x.Equipment == equipment);
        }

        public void AddEquipment(Equipment equipment, Room room, int quantity)
        {
            InventoryItem? found = GetInventoryItem(equipment, room);

            if (found != null)
            {
                found.Quantity += quantity;
                return;
            }
            Items.Add(new InventoryItem(equipment, room, quantity));
        }

        public void RemoveEquipment(Equipment equipment, Room room, int quantity)
        {
            InventoryItem? found = GetInventoryItem(equipment, room);

            if (found == null) throw new Exception(); // TODO: specify exceptions
            if (found.Quantity < quantity) throw new Exception();
            found.Quantity -= quantity;
        }
    }
}
