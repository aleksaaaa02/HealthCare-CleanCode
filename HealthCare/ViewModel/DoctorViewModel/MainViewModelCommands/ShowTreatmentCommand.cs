using HealthCare.Command;
using HealthCare.View.DoctorView.TreatmentView;

namespace HealthCare.ViewModel.DoctorViewModel.MainViewModelCommands
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
