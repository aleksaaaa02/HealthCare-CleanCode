using HealthCare.Command;
using HealthCare.View.DoctorView;
using HealthCare.ViewModels.DoctorViewModel;


namespace HealthCare.ViewModel.DoctorViewModel.MainViewModelCommands
{
    public class MakeAppointmentNavigationCommand : CommandBase
    {

        private DoctorMainViewModel _viewModel;
        public MakeAppointmentNavigationCommand(DoctorMainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            new MakeAppointmentView(_viewModel).ShowDialog();
        }
    }
}
