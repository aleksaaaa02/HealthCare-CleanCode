using HealthCare.Command;
using HealthCare.Exceptions;
using System.Windows;

namespace HealthCare.ViewModel.DoctorViewModel.UsedEquipment.Commands
{
    public class QuantityChangeCommand : CommandBase
    {
        private EquipmentViewModel _equipmentViewModel;

        public QuantityChangeCommand(EquipmentViewModel equipmentViewModel)
        {
            _equipmentViewModel = equipmentViewModel;
        }

        public override void Execute(object parameter)
        {
            try
            {
                Validate();
                _equipmentViewModel.CurrentQuantity -= 1;
            }
            catch (ValidationException ve) 
            {
                MessageBox.Show(ve.Message, "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Validate()
        {
            if (_equipmentViewModel.CurrentQuantity <= 0)
            {
                throw new ValidationException("Trenutne opreme nema na stanju!");
            }
        }
    }
}
