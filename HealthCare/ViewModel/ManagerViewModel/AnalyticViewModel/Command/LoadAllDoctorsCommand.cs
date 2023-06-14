using HealthCare.GUI.Command;

namespace HealthCare.ViewModel.ManagerViewModel.AnalyticViewModel.Command
{
    public class LoadAllDoctorsCommand : CommandBase
    {
        private readonly DoctorAnalyticsViewModel _model;

        public LoadAllDoctorsCommand(DoctorAnalyticsViewModel model)
        {
            _model = model;
        }

        public override void Execute(object parameter)
        {
            _model.LoadAll();
        }
    }
}