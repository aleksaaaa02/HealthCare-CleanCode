using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HealthCare.ViewModel.ManagerViewModel
{
    public class RearrangingViewModel
    {
        private readonly RoomService _roomService;
        private readonly Inventory _equipmentInventory;
        private readonly EquipmentService _equipmentService;

        public ObservableCollection<InventoryItemViewModel> FromRooms { get; }
        public ObservableCollection<InventoryItemViewModel> ToRooms { get; }
        public List<Equipment> Equipment => _equipmentService.GetAll();

        public RearrangingViewModel()
        {
            _roomService = (RoomService)ServiceProvider.services["RoomService"];
            _equipmentInventory = (Inventory)ServiceProvider.services["EquipmentInventory"];
            _equipmentService = (EquipmentService)ServiceProvider.services["EquipmentService"];
            FromRooms = new ObservableCollection<InventoryItemViewModel>();
            ToRooms = new ObservableCollection<InventoryItemViewModel>();
        }

        public void Load(Equipment equipment)
        {
            FromRooms.Clear();
            ToRooms.Clear();
            var fromRooms = new List<InventoryItemViewModel>();
            var toRooms = new List<InventoryItemViewModel>();

            foreach (Room room in _roomService.GetAll()) {
                var found = _equipmentInventory.SearchByEquipmentAndRoom(equipment.Id, room.Id);

                if (found is not null && found.Quantity > 0)
                    fromRooms.Add(new InventoryItemViewModel(found, equipment, room));
                else 
                    found = new InventoryItem(0, equipment.Id, room.Id, 0);

                toRooms.Add(new InventoryItemViewModel(found, equipment, room));
            }

            Sort(fromRooms, -1).ForEach(model => FromRooms.Add(model));
            Sort(toRooms).ForEach(model => ToRooms.Add(model));
        }

        public List<InventoryItemViewModel> Sort(List<InventoryItemViewModel> items, int order=1)
        {
            IEnumerable<InventoryItemViewModel> sorted;
            if (order == -1)
                sorted = items.OrderByDescending(x => x.Quantity).ThenBy(x => x.RoomName);
            else
                sorted = items.OrderBy(x => x.Quantity).ThenBy(x => x.RoomName);
            return sorted.ToList();
        }
    }
}
