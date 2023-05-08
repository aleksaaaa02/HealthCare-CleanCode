using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.UsedEquipment.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HealthCare.ViewModel.DoctorViewModel.UsedEquipment
{
    public class UsedDynamicEquipmentViewModel : ViewModelBase
    {
        private readonly Hospital _hospital;
        private readonly int _roomId;
        private ObservableCollection<EquipmentViewModel> _usedDynamicEquipment;
        public IEnumerable<EquipmentViewModel> UsedDynamicEquipment => _usedDynamicEquipment;

        private EquipmentViewModel _selectedItem;
        public EquipmentViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        private int _itemQuantity;
        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                OnPropertyChanged(nameof(ItemQuantity));
            }
        }

        public ICommand UseEquipmentCommand { get; }
        public ICommand EndExaminationCommand { get; }
        public UsedDynamicEquipmentViewModel(Hospital hospital, Window window, int roomId) { 
            _usedDynamicEquipment = new ObservableCollection<EquipmentViewModel>();
            _hospital = hospital;
            _roomId = roomId;

            UseEquipmentCommand = new UseEquipmentCommand(hospital, this);
            EndExaminationCommand = new CancelCommand(window);

            Update();
        }

        public void Update()
        {
            _usedDynamicEquipment.Clear();

            foreach(InventoryItem inventoryItem in _hospital.Inventory.GetRoomItems(_roomId))
            {
                Equipment equipment = _hospital.EquipmentService.Get(inventoryItem.EquipmentId);
                if (equipment.Dynamic)
                {
                    _usedDynamicEquipment.Add(new EquipmentViewModel(equipment, inventoryItem));
                }
            }

        }
    }
}
