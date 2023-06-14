using HealthCare.Model;
using HealthCare.ViewModel.NurseViewModel.VisitsMVVM;
using System.Windows;

namespace HealthCare.View.NurseView.VisitsView
{
    public partial class VisitInformationView : Window
    {
        public VisitInformationView(Visit visit)
        {
            InitializeComponent();
            DataContext = new VisitsInformationViewModel(visit,this);
        }
    }
}
