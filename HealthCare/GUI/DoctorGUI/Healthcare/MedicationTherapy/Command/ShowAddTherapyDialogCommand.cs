using HealthCare.Command;
using HealthCare.GUI.DoctorGUI.Healthcare.MedicalPrescription;
using HealthCare.GUI.DoctorGUI.Healthcare.PatientTreatment;
using HealthCare.Model;

namespace HealthCare.GUI.DoctorGUI.Healthcare.MedicationTherapy.Command
{
    public class ShowAddTherapyDialogCommand : CommandBase
    {
        private readonly Therapy _therapy;
        private readonly DoctorTreatmentVisitViewModel _viewModel;

        public ShowAddTherapyDialogCommand(DoctorTreatmentVisitViewModel viewModel, Therapy therapy)
        {
            _therapy = therapy;
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            new PrescriptionView(_therapy).ShowDialog();
            _viewModel.Update();
        }
    }
}