using HealthCare.ViewModel.ManagerViewModel.AnalyticViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
