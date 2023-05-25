using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Repository;
using System;

namespace HealthCare.Service
{
    public class OrderService : NumericService<OrderItem>
    {
        private readonly Inventory _inventory;
        private readonly RoomService _roomService;

        public OrderService(string filepath) : base(filepath) 
        {
            _inventory = (Inventory)ServiceProvider.services["EquipmentInventory"];
            _roomService = (RoomService)ServiceProvider.services["RoomService"];
        }

        private OrderService(IRepository<OrderItem> repository) : base(repository)
        {
            _inventory = (Inventory)ServiceProvider.services["EquipmentInventory"];
            _roomService = (RoomService)ServiceProvider.services["RoomService"];
        }

        private static OrderService? _instance = null;
        public static OrderService GetInstance(IRepository<OrderItem> repository)
        {
            if (_instance is not null) return _instance;
            _instance = new OrderService(repository);
            return _instance;
        }

        public void Execute(OrderItem item)
        {
            int warehouseId = _roomService.GetWarehouseId();

            var restockItem = new InventoryItem(
                item.EquipmentId, warehouseId, item.Quantity);

            _inventory.RestockInventoryItem(restockItem);
            item.Executed = true;

            Update(item);
        }

        public void ExecuteAll()
        {
            GetAll().ForEach(x => {
                if (!x.Executed && x.Scheduled <= DateTime.Now) 
                    Execute(x);
            });
        }
    }
}
