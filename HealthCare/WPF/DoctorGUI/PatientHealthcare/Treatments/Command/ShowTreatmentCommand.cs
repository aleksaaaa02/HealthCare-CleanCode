using HealthCare.WPF.Common.Command;

namespace HealthCare.WPF.DoctorGUI.PatientHealthcare.Treatments.Command
{
    public class ShowTreatmentCommand : CommandBase
    {
        public ShowTreatmentCommand()
        {
        }

        public override void Execute(object parameter)
        {
            new DoctorTreatmentView().ShowDialog();
        }
    }
}