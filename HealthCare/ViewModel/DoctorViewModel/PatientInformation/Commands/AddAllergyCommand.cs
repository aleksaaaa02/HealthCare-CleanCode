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
    public class AddAllergyCommand : CommandBase
    {
        private readonly PatientInforamtionViewModel _viewModel;

        public AddAllergyCommand(PatientInforamtionViewModel viewModel) 
        {
            _viewModel = viewModel;
        }
        public override void Execute(object parameter)
        {
            try
            {
                Validate();
                _viewModel.AddAllergy(_viewModel.Allergy);
                
            } catch(ValidationException ve) 
            {
                MessageBox.Show(ve.Message, "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Validate()
        {
            if(string.IsNullOrWhiteSpace(_viewModel.Allergy))
            {
                throw new ValidationException("Morate uneti alergiju u polje");
            }
        }
    }
}
