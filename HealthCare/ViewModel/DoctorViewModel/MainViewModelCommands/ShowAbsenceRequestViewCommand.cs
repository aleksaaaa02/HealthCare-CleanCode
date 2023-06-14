
using HealthCare.Command;
using HealthCare.View.DoctorView.AbsenceRequestView;

namespace HealthCare.ViewModel.DoctorViewModel.MainViewModelCommands
{
    public class ShowAbsenceRequestViewCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            new AbsenceRequestView().ShowDialog();
        }
    }
}
