using HealthCare.ViewModel.ManagerViewModel.AnalyticViewModel;
using System.Windows;

namespace HealthCare.View.ManagerView.AnalyticsView
{
    public partial class AnalyticsMainView : Window
    {
        public AnalyticsMainView()
        {
            InitializeComponent();

            DataContext = new SurveyAnalyticsViewModel();
        }
    }
}
