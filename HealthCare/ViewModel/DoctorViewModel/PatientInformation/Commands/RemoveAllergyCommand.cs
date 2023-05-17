using HealthCare.Command;
using HealthCare.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthCare.ViewModel.DoctorViewModel.PatientInformation.Commands
{
    public class RemoveAllergyCommand : CommandBase
    {
        private readonly PatientInforamtionViewModel _viewModel;
        public RemoveAllergyCommand(PatientInforamtionViewModel viewModel) 
        {
            _viewModel = viewModel;
        }
        public override void Execute(object parameter)
        {
            try
            {
                Validate();
                string selectedAllergy = _viewModel.SelectedAllergy;
                _viewModel.RemoveAllergy(selectedAllergy);
            
            } catch(ValidationException ve)
            {
                MessageBox.Show(ve.Message, "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
        private void Validate()
        {
            if(_viewModel.SelectedAllergy is null)
            {
                throw new ValidationException("Morate odabrati alergiju koju zelite da uklonite.");
            }

        }
    }
}
