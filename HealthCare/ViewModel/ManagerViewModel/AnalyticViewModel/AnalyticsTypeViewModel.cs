using System.Windows.Controls;

namespace HealthCare.ViewModel.ManagerViewModel.AnalyticViewModel
{
    public class AnalyticsTypeViewModel
    {
        public AnalyticsTypeViewModel(string shortTitle, string title, UserControl control, object model)
        {
            ShortTitle = shortTitle;
            Title = title;
            Control = control;
            Model = model;
        }

        public string ShortTitle { get; set; }
        public string Title { get; set; }
        public UserControl Control { get; set; }
        public object Model { get; set; }
    }
}