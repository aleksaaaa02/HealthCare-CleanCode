using System.Windows.Input;
using HealthCare.GUI.DoctorGUI.Healthcare.Examination.Command;
using HealthCare.Model;
using HealthCare.ViewModel;

namespace HealthCare.GUI.DoctorGUI.Healthcare.Examination
{
    public class EquipmentViewModel : ViewModelBase
    {
        private readonly Equipment _equipment;
        private readonly InventoryItem _inventoryItem;
        private int _currentQuantity;

        public EquipmentViewModel(Equipment equipment, InventoryItem inventoryItem)
        {
            _equipment = equipment;
            _inventoryItem = inventoryItem;
            _currentQuantity = _inventoryItem.Quantity;
            UseEquipment = new QuantityChangeCommand(this);
        }

        public ICommand UseEquipment { get; }

        public string EquipmentName => _equipment.Name;
        public int EquipmentId => _equipment.Id;
        public int InventoryId => _inventoryItem.Id;

        public int CurrentQuantity
        {
            get => _currentQuantity;
            set
            {
                _currentQuantity = value;
                OnPropertyChanged();
            }
        }
    }
}