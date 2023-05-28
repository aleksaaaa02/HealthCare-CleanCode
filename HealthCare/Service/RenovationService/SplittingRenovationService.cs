using HealthCare.Model.Renovation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service.RenovationService
{
    public class SplittingRenovationService : RenovationService<SplittingRenovation>
    {
        private readonly RoomService _roomService;
        private readonly Inventory _inventory;

        public SplittingRenovationService(string filepath, RoomService roomService, Inventory inventory) : base(filepath)
        {
            _roomService = roomService;
            _inventory = inventory;
        }

        public override void Execute(SplittingRenovation renovation)
        {
            var items = _inventory.GetRoomItems(renovation.RoomId);
            int warehouseId = 1; // TODO chane to inventory.GetWarehouseId
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
