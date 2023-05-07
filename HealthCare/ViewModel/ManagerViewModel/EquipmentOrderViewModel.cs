using HealthCare.Context;
using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModel.ManagerViewModel
{
    public class EquipmentOrderViewModel : ViewModelBase
    {
        private readonly Hospital _hospital;
        public ObservableCollection<OrderItemViewModel> Items { get; set; }

        public EquipmentOrderViewModel(Hospital hospital)
        {
            _hospital = hospital;
            Items = new ObservableCollection<OrderItemViewModel>();
            LoadAll();
        }

        public void LoadAll()
        {
            Items.Clear();
            foreach (var equipment in _hospital.Inventory.GetLowQuantityEquipment())
            {
                if (equipment.Dynamic)
                    Items.Add(new OrderItemViewModel(equipment, _hospital.Inventory.GetTotalQuantity(equipment.Name)));
            }
        }
    }
}
