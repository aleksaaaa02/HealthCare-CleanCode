using HealthCare.GUI.Command;

namespace HealthCare.GUI.DoctorGUI.AbsenceRequesting
{
    public class ShowAbsenceRequestViewCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            new AbsenceRequestView().ShowDialog();
        }
    }
}