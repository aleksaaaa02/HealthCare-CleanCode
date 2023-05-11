using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
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
        private readonly Inventory _inventory;
        private readonly EquipmentService _equipmentService;
        public ObservableCollection<OrderItemViewModel> Items { get; set; }

        public EquipmentOrderViewModel(Inventory inventory, Hospital hospital)
        {
            _inventory = inventory;
            _equipmentService = hospital.EquipmentService;

            Items = new ObservableCollection<OrderItemViewModel>();
            Load();
        }

        public void Load()
        {
            Items.Clear();
            foreach (int equipmentId in _inventory.GetLowQuantityEquipment())
            {
                var equipment = _equipmentService.Get(equipmentId);
                if (equipment.Dynamic)
                    Items.Add(new OrderItemViewModel(equipment, _inventory.GetTotalQuantity(equipmentId)));
            }
        }
    }
}
