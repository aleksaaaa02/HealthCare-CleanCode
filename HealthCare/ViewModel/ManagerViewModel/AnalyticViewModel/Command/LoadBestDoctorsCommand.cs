using HealthCare.GUI.Command;

namespace HealthCare.ViewModel.ManagerViewModel.AnalyticViewModel.Command
{
    public class LoadBestDoctorsCommand : CommandBase
    {
        private readonly DoctorAnalyticsViewModel _model;

        public LoadBestDoctorsCommand(DoctorAnalyticsViewModel model)
        {
            _model = model;
        }

        public override void Execute(object parameter)
        {
            _model.LoadTop3();
        }
    }
}