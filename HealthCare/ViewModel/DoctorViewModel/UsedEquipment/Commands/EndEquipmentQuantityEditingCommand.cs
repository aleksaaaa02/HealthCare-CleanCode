using System.Collections.Generic;
using System.Windows;
using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Service;

namespace HealthCare.ViewModel.DoctorViewModel.UsedEquipment.Commands;

public class EndEquipmentQuantityEditingCommand : CommandBase
{
    private readonly InventoryService _equipmentInventory;
    private readonly UsedDynamicEquipmentViewModel _viewModel;
    private readonly Window _window;

    public EndEquipmentQuantityEditingCommand(Window window, UsedDynamicEquipmentViewModel viewModel)
    {
        _equipmentInventory = Injector.GetService<InventoryService>(Injector.EQUIPMENT_INVENTORY_S);
        _viewModel = viewModel;
        _window = window;
    }

    public override void Execute(object parameter)
    {
        _window.Close();
        var newQuantities = new Dictionary<int, int>();
        foreach (var equipment in _viewModel.UsedDynamicEquipment)
            newQuantities.Add(equipment.InventoryId, equipment.CurrentQuantity);
        _equipmentInventory.ChangeDynamicEquipmentQuantity(newQuantities);
    }
}