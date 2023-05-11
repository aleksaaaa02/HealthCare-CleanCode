using HealthCare.Command;
using HealthCare.Exceptions;
using HealthCare.ViewModel.DoctorViewModel.PatientInformation;
using System.Windows;

namespace HealthCare.ViewModel.DoctorViewModel.PatientInformation.Commands
{
    public class RemoveDiseaseCommand : CommandBase
    {

        private readonly PatientInforamtionViewModel _viewModel;
        public RemoveDiseaseCommand(PatientInforamtionViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object parameter)
        {
            try
            {
                Validate();
                string selectedDisease = _viewModel.SelectedDisease;
                _viewModel.RemovePreviousDisease(selectedDisease);
            }
            catch (ValidationException ve)
            {
                MessageBox.Show(ve.Message, "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }
        private void Validate()
        {
            if (_viewModel.SelectedDisease is null)
            {
                throw new ValidationException("Morate odabrati bolest koju zelite da uklonite.");
            }
        }
    }
}
