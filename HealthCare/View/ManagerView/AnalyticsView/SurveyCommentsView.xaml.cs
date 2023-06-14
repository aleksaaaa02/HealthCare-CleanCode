using System.Collections.Generic;
using System.Windows;
using HealthCare.Core.PatientSatisfaction;
using HealthCare.ViewModel.ManagerViewModel.AnalyticViewModel;

namespace HealthCare.View.ManagerView.AnalyticsView
{
    public partial class SurveyCommentsView : Window
    {
        public SurveyCommentsView(List<Survey> surveys)
        {
            InitializeComponent();

            DataContext = new SurveyCommentsViewModel(surveys);
        }
    }
}