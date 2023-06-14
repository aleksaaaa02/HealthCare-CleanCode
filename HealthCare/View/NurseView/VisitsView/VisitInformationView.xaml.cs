using System.Windows;
using HealthCare.Core.PatientHealthcare.HealthcareTreatment;
using HealthCare.ViewModel.NurseViewModel.VisitsMVVM;

namespace HealthCare.View.NurseView.VisitsView
{
    public partial class VisitInformationView : Window
    {
        public VisitInformationView(Visit visit)
        {
            InitializeComponent();
            DataContext = new VisitsInformationViewModel(visit, this);
        }
    }
}