using HealthCare.Command;
using HealthCare.Exceptions;
using HealthCare.View;

namespace HealthCare.ViewModel.DoctorViewModel.PatientInformation.Commands
{
    public class RemoveDiseaseCommand : CommandBase
    {
        private readonly PatientInformationViewModel _viewModel;

        public RemoveDiseaseCommand(PatientInformationViewModel viewModel)
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
                Utility.ShowWarning(ve.Message);
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
