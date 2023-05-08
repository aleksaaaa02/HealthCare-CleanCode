using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Exceptions;
using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthCare.ViewModel.DoctorViewModel.UsedEquipment.Commands
{
    public class UseEquipmentCommand : CommandBase
    {
        private readonly Hospital _hospital;
        private readonly UsedDynamicEquipmentViewModel _viewModel;
        public UseEquipmentCommand(Hospital hospital, UsedDynamicEquipmentViewModel viewModel)
        {
            _hospital = hospital;
            _viewModel = viewModel;
        }
        public override void Execute(object parameter)
        {
            try
            {
                Validate();
                int itemQuantity = _viewModel.ItemQuantity;
                InventoryItem inventoryItem = _hospital.Inventory.Get(_viewModel.SelectedItem.InventoryId);
                inventoryItem.Quantity -= itemQuantity;
                _hospital.Inventory.Update(inventoryItem);
                _viewModel.Update();
            }
            catch (ValidationException ve)
            {
                MessageBox.Show(ve.Message);
            }


        }
        private void Validate()
        {
            int itemQuantity = _viewModel.ItemQuantity;

            if (itemQuantity <= 0)
            {
                throw new ValidationException("Morati uneti pozitivan ceo broj");
            }

            if (_viewModel.SelectedItem == null)
            {
                throw new ValidationException("Morate odabrati opremu iz liste");
            }
            InventoryItem inventoryItem = _hospital.Inventory.Get(_viewModel.SelectedItem.InventoryId);

            if (inventoryItem.Quantity - itemQuantity < 0)
            {
                throw new ValidationException("Uneli ste veci broj nego sto posedujete");
            }
        }
    }
}
