using System.Windows;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.Treatment;

namespace HealthCare.View.DoctorView.TreatmentView
{
    public partial class DoctorTreatmentVisitView : Window
    {
        public DoctorTreatmentVisitView(Treatment treatment)
        {
            InitializeComponent();
            DataContext = new DoctorTreatmentVisitViewModel(this, treatment);
        }
    }
}
