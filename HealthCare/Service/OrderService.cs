using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public class OrderService
    {
        public List<OrderItem> Items { get; set; }

        private CsvStorage<OrderItem> csvStorage;

        public OrderService(string filepath)
        {
            Items = new List<OrderItem>();
            csvStorage = new CsvStorage<OrderItem>(filepath);
        }

        public OrderItem Get(int orderId)
        {
            OrderItem? found = Items.Find(x => x.Id == orderId);
            if (found != null) { return found; }
            throw new ObjectNotFoundException();
        }

        public void Add(OrderItem item)
        {
            if (Contains(item.Id)) throw new ObjectAlreadyExistException();
            Items.Add(item);
        }

        public void Remove(int orderId)
        {
            if (!Contains(orderId)) throw new ObjectNotFoundException();
            Items.RemoveAll(x => x.Id == orderId);
        }

        public void Update(OrderItem item)
        {
            if (!Contains(item.Id)) throw new ObjectNotFoundException();
            OrderItem current = Get(item.Id);
            current.Copy(item);
        }

        public bool Contains(int orderId)
        {
            return Items.FindIndex(x => x.Id == orderId) >= 0;
        }

        public int NextId()
        {
            int id = 1;
            for (; id <= Items.Count + 1; id++)
            {
                if (!Contains(id)) break;
            }
            return id;
        }

        public void Load()
        {
            Items = csvStorage.Load();
        }
        public void Save()
        {
            csvStorage.Save(Items);
        }
    }
}
