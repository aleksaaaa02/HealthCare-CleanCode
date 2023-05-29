using HealthCare.Command;
using HealthCare.View.DoctorView;

namespace HealthCare.ViewModel.DoctorViewModel.MainViewModelCommands
{
    public class ShowPatientSearchViewCommand : CommandBase
    {
        public ShowPatientSearchViewCommand() { }

        public override void Execute(object parameter)
        {
            new PatientSearchView().Show();
        }
    }
}
