using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthCare.ViewModel.ManagerViewModel
{
    public class InventoryListingViewModel : ViewModelBase
    {
        private readonly Hospital _hospital;
        public ObservableCollection<InventoryItemViewModel> Items { get; set; }

        public InventoryListingViewModel(Hospital hospital)
        {
            _hospital = hospital;
            Items = new ObservableCollection<InventoryItemViewModel>();
            LoadAll();
        }

        public void LoadAll()
        {
            Items.Clear();
            foreach (InventoryItem item in _hospital.Inventory.GetAll())
            {
                Equipment equipment = _hospital.EquipmentService.Get(item.EquipmentId);
                Room room = _hospital.RoomService.Get(item.RoomId);
                Items.Add(new InventoryItemViewModel(item, equipment, room));
            }
        }

        private void LoadFiltered(List<InventoryItemViewModel> items)
        {
            Items.Clear();
            items.ForEach(item => Items.Add(item));
        }

        public void Filter(string query, bool[] quantities, bool[] equipmentTypes, bool[] roomTypes)
        {
            LoadAll();
            InventoryFilter filter = new InventoryFilter(Items.ToList());
            filter.FilterAnyProperty(query);
            filter.FilterQuantity(quantities);
            filter.FilterEquipmentType(equipmentTypes);
            filter.FilterRoomType(roomTypes);
            LoadFiltered(filter.GetFiltered());
        }
    }
}
