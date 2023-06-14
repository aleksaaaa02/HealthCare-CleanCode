using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Exceptions;
using HealthCare.Service;
using HealthCare.View;
using HealthCare.View.DoctorView.TreatmentView;

namespace HealthCare.ViewModel.DoctorViewModel.Treatment
{
    public class VisitPatientCommand : CommandBase
    {
        private readonly DoctorTreatmentViewModel _viewModel;
        private readonly TreatmentService _treatmentService;
        private Model.Treatment _treatment;
        public VisitPatientCommand(DoctorTreatmentViewModel viewModel)
        {
            _viewModel = viewModel;
            _treatmentService = Injector.GetService<TreatmentService>();
        }
        public override void Execute(object parameter)
        {
            try
            {
                Validate();
                new DoctorTreatmentVisitView(_treatment).ShowDialog();
                _viewModel.Update();

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

            _treatment = _treatmentService.Get(_viewModel.SelectedTreatment.TreatmentId);

        }
    }
}
