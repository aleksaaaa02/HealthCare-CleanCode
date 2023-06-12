using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HealthCare.View.ManagerView.AnalyticsView
{
    public class AnalyticType
    {
        public string ShortTitle { get; set; }
        public string Title { get; set; }
        public UserControl Control { get; set; }

        public AnalyticType(string shortTitle, string title, UserControl control)
        {
            ShortTitle = shortTitle;
            Title = title;
            Control = control;
        }
    }
}
