using HealthCare.GUI.Command;

namespace HealthCare.ViewModel.ManagerViewModel.AnalyticViewModel.Command
{
    public class LoadWorstDoctorsCommand : CommandBase
    {
        private readonly DoctorAnalyticsViewModel _model;

        public LoadWorstDoctorsCommand(DoctorAnalyticsViewModel model)
        {
            _model = model;
        }

        public override void Execute(object parameter)
        {
            _model.LoadBottom3();
        }
    }
}