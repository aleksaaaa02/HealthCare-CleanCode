using HealthCare.Command;
using HealthCare.Application;
using HealthCare.Service;
using System.Collections.Generic;
using System.Windows;

namespace HealthCare.ViewModel.DoctorViewModel.UsedEquipment.Commands
{
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
            Dictionary<int, int> newQuantities = new Dictionary<int, int>();
            foreach(EquipmentViewModel equipment in _viewModel.UsedDynamicEquipment)
            {
                newQuantities.Add(equipment.InventoryId, equipment.CurrentQuantity);
            }
            _equipmentInventory.ChangeDynamicEquipmentQuantity(newQuantities);
        }
    }
}
