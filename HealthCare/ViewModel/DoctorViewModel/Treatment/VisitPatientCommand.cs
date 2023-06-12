using HealthCare.Command;
using HealthCare.Exceptions;
using HealthCare.View;

namespace HealthCare.ViewModel.DoctorViewModel.Treatment
{
    public class VisitPatientCommand : CommandBase
    {
        private readonly DoctorTreatmentViewModel _viewModel;
        public VisitPatientCommand(DoctorTreatmentViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object parameter)
        {
            try
            {
                Validate();
            }
            catch (ValidationException ve)
            {
                ViewUtil.ShowWarning(ve.Message);
            }
        }

        private void Validate()
        {
            if (_viewModel.SelectedTreatment is null)
            {
                throw new ValidationException("Morate odabrati lecenje iz date liste");
            }
        }
    }
}
