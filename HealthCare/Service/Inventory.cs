using HealthCare.Exceptions;
using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    class Inventory
    {
        public List<InventoryItem> Items { get; }

        public Inventory()
        {
            Items = new List<InventoryItem>();
        }

        public InventoryItem Get(InventoryItem item)
        {
            InventoryItem? found = Items.Find(x => x == item);
            if (found is not null) { return found; }
            else { throw new NonExistingObjectException(); }
        }

        public void Add(InventoryItem item)
        {
            if (Contains(item)) { throw new DuplicateObjectException(); }
            Items.Add(item);
        }

        public void Remove(InventoryItem item)
        {
            if (!Contains(item)) { throw new NonExistingObjectException(); }
            Items.Remove(item);
        }

        public void Update(InventoryItem item)
        {
            InventoryItem current = Get(item);
            current.Quantity = item.Quantity;
        }

        public bool Contains(InventoryItem item) 
        {
            return Items.Find(x => x == item) is not null;
        }
    }
}
