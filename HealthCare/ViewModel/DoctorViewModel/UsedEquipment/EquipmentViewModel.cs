using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// prepravi namespace
namespace HealthCare.ViewModel.DoctorViewModel.UsedEquipment
{

    public class EquipmentViewModel : ViewModelBase
    {
        private readonly Equipment _equipment;
        private readonly InventoryItem _inventoryItem;
        public string EquipmentName => _equipment.Name;
        public int EquipmentId => _equipment.Id;
        public int CurrentQuantity => _inventoryItem.Quantity;
        public int InventoryId => _inventoryItem.Id;

        public EquipmentViewModel(Equipment equipment, InventoryItem inventoryItem)
        {
            _equipment = equipment;
            _inventoryItem = inventoryItem;
            
        }
    }
}
