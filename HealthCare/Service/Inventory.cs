using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public class Inventory
    {
        public List<InventoryItem> Items { get; set; }

        private CsvStorage<InventoryItem> csvStorage;

        public Inventory(string filepath)
        {
            Items = new List<InventoryItem>();
            csvStorage = new CsvStorage<InventoryItem>(filepath);
        }

        public InventoryItem Get(string equipmentName, string roomName)
        {
            InventoryItem? found = Items.Find(x => 
                x.Equipment.Name == equipmentName && 
                x.Room.Name == roomName);
            if (found != null) { return found; }
            throw new ObjectNotFoundException();
        }

        public void Add(InventoryItem item)
        {
            if (Contains(item.Equipment.Name, item.Room.Name)) throw new ObjectAlreadyExistException();
            Items.Add(item);
        }

        public void Remove(string equipmentName, string roomName)
        {
            if (!Contains(equipmentName, roomName)) throw new ObjectNotFoundException();
            Items.RemoveAll(x =>
                x.Equipment.Name == equipmentName &&
                x.Room.Name == roomName);
        }

        public void Update(InventoryItem item)
        {
            if (!Contains(item.Equipment.Name, item.Room.Name)) throw new ObjectNotFoundException();
            InventoryItem current = Get(item.Equipment.Name, item.Room.Name);
            current.Copy(item);
        }

        public bool Contains(string equipmentName, string roomName)
        {
            return Items.FindIndex(x => 
                x.Equipment.Name == equipmentName && 
                x.Room.Name == roomName) >= 0;
        }

        public void Load()
        {
            Items = csvStorage.Load();
        }
        public void Save()
        {
            csvStorage.Save(Items);
        }

        internal int GetTotalQuantity(string equipmentName)
        {
            int quantity = 0;
            foreach (var item in Items)
            {
                if (item.Equipment.Name == equipmentName) quantity += item.Quantity;
            }
            return quantity;
        }
    }
}
