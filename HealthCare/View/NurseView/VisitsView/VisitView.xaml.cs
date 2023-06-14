using System.Windows;
using HealthCare.ViewModel.NurseViewModel.VisitsMVVM;

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