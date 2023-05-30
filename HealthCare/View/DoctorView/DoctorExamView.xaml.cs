using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.Examination;
using System.Windows;

namespace HealthCare.View.DoctorView
{
    public partial class DoctorExamView : Window
    {
        public DoctorExamView(Appointment appointment)
        {
            InitializeComponent();
            DataContext = new DoctorExamViewModel(this, appointment);
        }
    }
}
