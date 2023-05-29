using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Repository;
using System;

namespace HealthCare.Service
{
    public class OrderService : NumericService<OrderItem>
    {
        private readonly RoomService _roomService;
        private readonly InventoryService _inventory;

        public OrderService(IRepository<OrderItem> repository, InventoryService inventory) : base(repository)
        {
            _roomService = Injector.GetService<RoomService>();
            _inventory = inventory;

            ExecuteAll();
        }

        public void Execute(OrderItem item)
        {
            int warehouseId = _roomService.GetWarehouseId();

            var restockItem = new InventoryItem(
                item.ItemId, warehouseId, item.Quantity);

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
