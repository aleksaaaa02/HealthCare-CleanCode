using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Model;
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
            _load(_hospital.Inventory.Items);
        }

        private void _load(List<InventoryItem> items)
        {
            Items.Clear();
            foreach (var item in items)
            {
                Items.Add(new InventoryItemViewModel(item));
            }
        }

        public void Filter(string query, bool[] quantities, bool[] equipmentTypes, bool[] roomTypes)
        {
            InventoryFilter filter = new InventoryFilter(_hospital.Inventory.Items);
            filter.FilterAnyProperty(query);
            filter.FilterQuantity(quantities);
            filter.FilterEquipmentType(equipmentTypes);
            filter.FilterRoomType(roomTypes);
            _load(filter.GetFiltered());
        }
    }
}
