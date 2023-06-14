using System.Windows;
using HealthCare.ViewModel.ManagerViewModel.AnalyticViewModel;

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