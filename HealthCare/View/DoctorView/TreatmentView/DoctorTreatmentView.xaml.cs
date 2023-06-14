using System.Windows;
using HealthCare.ViewModel.DoctorViewModel.Treatment;

namespace HealthCare.View.DoctorView.TreatmentView
{

    public partial class DoctorTreatmentView : Window
    {
        public DoctorTreatmentView()
        {
            InitializeComponent();
            DataContext = new DoctorTreatmentViewModel();
        }
    }
}
