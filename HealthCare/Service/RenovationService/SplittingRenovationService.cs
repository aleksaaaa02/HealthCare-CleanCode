using HealthCare.Application;
using HealthCare.Model.Renovation;
using HealthCare.Repository;

namespace HealthCare.Service.RenovationService
{
    public class SplittingRenovationService : RenovationService<SplittingRenovation>
    {
        private readonly RoomService _roomService;
        private readonly InventoryService _inventory;

        public SplittingRenovationService(IRepository<SplittingRenovation> repository) : base(repository)
        {
            _roomService = Injector.GetService<RoomService>();
            _inventory = Injector.GetService<InventoryService>(Injector.EQUIPMENT_INVENTORY_S);
        }

        public override void Execute(SplittingRenovation renovation)
        {
            var items = _inventory.GetRoomItems(renovation.RoomId);
            int warehouseId = _roomService.GetWarehouseId();
            items.ForEach(x => {
                x.RoomId = warehouseId;
                _inventory.Update(x);
            });

            _roomService.Remove(renovation.RoomId);
            _roomService.Add(renovation.ResultRoom1);
            _roomService.Add(renovation.ResultRoom2);

            renovation.Executed = true;
            Update(renovation);
        }
    }
}
