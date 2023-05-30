using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HealthCare.Application;
using HealthCare.Service;
using HealthCare.ViewModel.DoctorViewModel.UsedEquipment.Commands;

namespace HealthCare.ViewModel.DoctorViewModel.UsedEquipment;

public class UsedDynamicEquipmentViewModel : ViewModelBase
{
    private readonly EquipmentService _equipmentService;
    private readonly InventoryService _inventoryService;
    private readonly int _roomId;
    private readonly ObservableCollection<EquipmentViewModel> _usedDynamicEquipment;

    private int _itemQuantity;

    public UsedDynamicEquipmentViewModel(Window window, int roomId)
    {
        _inventoryService = Injector.GetService<InventoryService>(Injector.EQUIPMENT_INVENTORY_S);
        _equipmentService = Injector.GetService<EquipmentService>();
        _roomId = roomId;
        _usedDynamicEquipment = new ObservableCollection<EquipmentViewModel>();

        ResetEquipmentCommand = new ResetEquipmentQuantityCommand(this);
        EndExaminationCommand = new EndEquipmentQuantityEditingCommand(window, this);

        Update();
    }

    public IEnumerable<EquipmentViewModel> UsedDynamicEquipment => _usedDynamicEquipment;

    public int ItemQuantity
    {
        get => _itemQuantity;
        set
        {
            _itemQuantity = value;
            OnPropertyChanged();
        }
    }

    public ICommand ResetEquipmentCommand { get; }
    public ICommand EndExaminationCommand { get; }

    public void Update()
    {
        _usedDynamicEquipment.Clear();

        foreach (var inventoryItem in _inventoryService.GetRoomItems(_roomId))
        {
            var equipment = _equipmentService.Get(inventoryItem.ItemId);
            if (equipment.IsDynamic) _usedDynamicEquipment.Add(new EquipmentViewModel(equipment, inventoryItem));
        }
    }
}