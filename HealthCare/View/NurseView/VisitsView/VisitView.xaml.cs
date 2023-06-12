using HealthCare.ViewModel.NurseViewModel.VisitsMVVM;
using System.Windows;

namespace HealthCare.View.NurseView.VisitsView
{
    public partial class VisitView : Window
    {
        public VisitView()
        {
            InitializeComponent();
            DataContext = new VisitsViewModel(this);
        }
    }
}
