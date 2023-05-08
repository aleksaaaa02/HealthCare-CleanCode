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
        private readonly Inventory _inventory;
        private readonly RoomService _roomService;
        private readonly EquipmentService _equipmentService;
        public ObservableCollection<InventoryItemViewModel> Items { get; set; }

        public InventoryListingViewModel(Inventory inv, EquipmentService es, RoomService rs)
        {
            _inventory = inv;
            _equipmentService = es;
            _roomService = rs;
            LoadAll();
        }

        public void LoadAll()
        {
            Load(_inventory.GetAll());
        }

        private void Load(List<InventoryItem> items)
        {
            List<InventoryItemViewModel> models = new List<InventoryItemViewModel>();
            foreach (InventoryItem item in items)
            {
                Equipment e = _equipmentService.Get(item.EquipmentId);
                Room r = _roomService.Get(item.RoomId);
                models.Add(new InventoryItemViewModel(item, e, r));
            }
            Load(models);
        }

        private void Load(List<InventoryItemViewModel> items)
        {
            Items = new ObservableCollection<InventoryItemViewModel>(items);
        }

        public void Filter(string query, bool[] quantities, bool[] equipmentTypes, bool[] roomTypes)
        {
            InventoryFilter filter = new InventoryFilter(Items.ToList());
            filter.FilterAnyProperty(query);
            filter.FilterQuantity(quantities);
            filter.FilterEquipmentType(equipmentTypes);
            filter.FilterRoomType(roomTypes);
            Load(filter.GetFiltered());
        }
    }
}
