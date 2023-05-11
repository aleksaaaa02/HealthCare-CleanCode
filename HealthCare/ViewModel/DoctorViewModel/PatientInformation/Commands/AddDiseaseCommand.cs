using HealthCare.Command;
using HealthCare.Exceptions;
using HealthCare.ViewModel.DoctorViewModel.PatientInformation;
using System.Windows;

namespace HealthCare.ViewModel.DoctorViewModel.PatientInformation.Commands
{
    public class AddDiseaseCommand : CommandBase
    {
        private readonly PatientInforamtionViewModel _viewModel;

        public AddDiseaseCommand(PatientInforamtionViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object parameter)
        {
            try
            {
                Validate();
                _viewModel.AddPreviousDisease(_viewModel.Disease);
            }
            catch (ValidationException ve)
            {
                MessageBox.Show(ve.Message, "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(_viewModel.Disease))
            {
                throw new ValidationException("Morate uneti bolest u polje");
            }
        }
    }
}
