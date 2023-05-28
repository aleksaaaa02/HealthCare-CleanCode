using HealthCare.Model.Renovation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service.RenovationService
{
    public class JoiningRenovationService : RenovationService<JoiningRenovation>
    {
        private readonly RoomService _roomService;
        private readonly Inventory _inventory;

        public JoiningRenovationService(string filepath, RoomService roomService, Inventory inventory) : base(filepath) 
        {
            _roomService = roomService;
            _inventory = inventory;
        }

        public override void Execute(JoiningRenovation renovation)
        {
            var items1 = _inventory.GetRoomItems(renovation.RoomId);
            var items2 = _inventory.GetRoomItems(renovation.OtherRoomId);

            var combined = _inventory.CombineItems(items1, items2);

            items1.ForEach(x => _inventory.Remove(x.Id));
            items2.ForEach(x => _inventory.Remove(x.Id));
            _roomService.Remove(renovation.RoomId);
            _roomService.Remove(renovation.OtherRoomId);

            _roomService.Add(renovation.ResultRoom);
            combined.ForEach(x =>{
                x.RoomId = renovation.ResultRoom.Id;
                _inventory.Update(x);
            });

            renovation.Executed = true;
            Update(renovation);
        }
    }
}
