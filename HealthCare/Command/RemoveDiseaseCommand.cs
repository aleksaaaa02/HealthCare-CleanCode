using HealthCare.Exceptions;
using HealthCare.View.DoctorView;
using System.Windows;

namespace HealthCare.Command
{
    public class RemoveDiseaseCommand : CommandBase
    {          

        private readonly PatientInforamtionViewModel _viewModel;
        public RemoveDiseaseCommand(PatientInforamtionViewModel viewModel) {
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
            if (string.IsNullOrWhiteSpace(_viewModel.SelectedDisease))
            {
                throw new ValidationException("Morate odabrati bolest koju zelite da uklonite.");
            }
        }
    }
}
