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
    public class ManagerMainViewModel : ViewModelBase
    {
        public readonly Hospital Hospital;
        public ObservableCollection<InventoryItemViewModel> Items { get; set; }

        public ManagerMainViewModel(Hospital hospital)
        {
            Hospital = hospital;
            Items = new ObservableCollection<InventoryItemViewModel>();
            LoadAll();
        }

        public void LoadAll()
        {
            _load(Hospital.Inventory.Items);
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
            InventoryItemFilter filter = new InventoryItemFilter(Hospital.Inventory.Items);
            filter.FilterAnyProperty(query);
            filter.FilterQuantity(quantities);
            filter.FilterEquipmentType(equipmentTypes);
            filter.FilterRoomType(roomTypes);
            _load(filter.GetFiltered());
        }
    }
}
