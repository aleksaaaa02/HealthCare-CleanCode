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
    public class OrderService : NumericService<OrderItem>
    {
        private readonly Inventory _inventory;
        private readonly RoomService _roomService;
        public OrderService(string filepath, Inventory inventory, RoomService roomService)
            : base(filepath) 
        {
            _inventory = inventory;
            _roomService = roomService;
        }

        public void Execute(OrderItem item)
        {
            int warehouseId = _roomService.GetWarehouseId();

            InventoryItem restockItem = new InventoryItem(
                0, item.EquipmentId, warehouseId, item.Quantity);
            _inventory.RestockInventoryItem(restockItem);
            item.Executed = true;
            Update(item);
        }

        public void ExecuteOrders()
        {
            foreach (OrderItem item in GetAll())
                if (!item.Executed && item.Scheduled <= DateTime.Now)
                    Execute(item);
        }
    }
}
