using HealthCare.Context;
using HealthCare.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HealthCare.ViewModel.ManagerViewModel
{
    public class RearrangingViewModel
    {
        private readonly Hospital _hospital;
        public ObservableCollection<InventoryItemViewModel> FromRooms { get; }
        public ObservableCollection<InventoryItemViewModel> ToRooms { get; }
        public List<Equipment> Equipment => _hospital.EquipmentService.GetAll();

        public RearrangingViewModel(Hospital hospital)
        {
            _hospital = hospital;

            FromRooms = new ObservableCollection<InventoryItemViewModel>();
            ToRooms = new ObservableCollection<InventoryItemViewModel>();
        }

        public void Load(Equipment equipment)
        {
            FromRooms.Clear();
            ToRooms.Clear();

            foreach (Room room in _hospital.RoomService.GetAll()) {
                var item = new InventoryItem(0, equipment.Id, room.Id, 0);
                var found = _hospital.Inventory.GetAll().Find(x => x.Equals(item));

                if (found is not null) {
                    item.Quantity = found.Quantity;
                    FromRooms.Add(new InventoryItemViewModel(item, equipment, room));
                }
                else
                    item.Quantity = 0;

                ToRooms.Add(new InventoryItemViewModel(item, equipment, room));
            }
        }
    }
}
