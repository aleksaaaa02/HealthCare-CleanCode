using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Model.Renovation;
using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service.RenovationService
{
    public class JoiningRenovationService : NumericService<JoiningRenovation>, IRenovationService
    {
        private readonly RoomService _roomService;
        private readonly InventoryService _inventory;

        public JoiningRenovationService(IRepository<JoiningRenovation> repository) : base(repository)
        {
            _roomService = Injector.GetService<RoomService>();
            _inventory = Injector.GetService<InventoryService>(Injector.EQUIPMENT_INVENTORY_S);
            ExecuteAll();
        }
        
        public void Execute(JoiningRenovation renovation)
        {
            var items1 = _inventory.GetRoomItems(renovation.RoomId);
            var items2 = _inventory.GetRoomItems(renovation.OtherRoomId);

            var combined = _inventory.CombineItems(items1, items2);

            items1.ForEach(x => _inventory.Remove(x.Id));
            items2.ForEach(x => _inventory.Remove(x.Id));
            combined.ForEach(x =>{
                x.RoomId = renovation.ResultRoom.Id;
                _inventory.RestockInventoryItem(x);
            });

            _roomService.Remove(renovation.RoomId);
            _roomService.Remove(renovation.OtherRoomId);
            _roomService.Add(renovation.ResultRoom);

            renovation.Executed = true;
            Update(renovation);
        }

        public void ExecuteAll()
        {
            GetAll().ForEach(x => {
                if (!x.Executed && x.Scheduled.End <= DateTime.Now)
                    Execute(x);
            });
        }

        public IEnumerable<RenovationBase> GetRenovations()
        {
            return GetAll().Cast<RenovationBase>();
        }
    }
}
