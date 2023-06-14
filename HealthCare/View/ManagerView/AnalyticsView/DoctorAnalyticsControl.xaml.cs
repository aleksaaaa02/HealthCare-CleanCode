using System.Windows.Controls;
using System.Windows.Input;
using HealthCare.ViewModel.ManagerViewModel.AnalyticViewModel;

namespace HealthCare.View.ManagerView.AnalyticsView
{
    public partial class DoctorAnalyticsControl : UserControl
    {
        private DoctorAnalyticsViewModel _model;

        public DoctorAnalyticsControl(DoctorAnalyticsViewModel model)
        {
            InitializeComponent();
            _model = model;
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _model.ShowDoctorCommentsCommand.Execute(this);
        }
    }
}