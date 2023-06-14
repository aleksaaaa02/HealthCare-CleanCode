using System.Windows;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.Treatment;

namespace HealthCare.View.DoctorView.TreatmentView
{

    public partial class PatientReleaseAppointmentView : Window
    {
        public PatientReleaseAppointmentView(Treatment treatment)
        {
            InitializeComponent();
            DataContext = new PatientReleaseAppointmentViewModel(this, treatment);
        }
    }
}
