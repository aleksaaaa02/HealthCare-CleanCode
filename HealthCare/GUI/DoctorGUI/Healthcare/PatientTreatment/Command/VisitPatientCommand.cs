using HealthCare.Application;
using HealthCare.Core.PatientHealthcare.HealthcareTreatment;
using HealthCare.Exceptions;
using HealthCare.GUI.Command;
using HealthCare.View;

namespace HealthCare.GUI.DoctorGUI.Healthcare.PatientTreatment.Command
{
    public class VisitPatientCommand : CommandBase
    {
        private readonly TreatmentService _treatmentService;
        private readonly DoctorTreatmentViewModel _viewModel;
        private Treatment _treatment;

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