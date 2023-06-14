using HealthCare.Command;

namespace HealthCare.GUI.DoctorGUI.Healthcare.PatientTreatment.Command
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