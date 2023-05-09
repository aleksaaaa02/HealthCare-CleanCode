using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.Examination;
using System.Windows;

namespace HealthCare.View.DoctorView
{
    /// <summary>
    /// Interaction logic for DoctorExamView.xaml
    /// </summary>
    public partial class DoctorExamView : Window
    {
        public DoctorExamView(Hospital hospital, Appointment appointment, int roomId)
        {
            InitializeComponent();
            DataContext = new DoctorExamViewModel(hospital, this, appointment, roomId);
        }
    }
}
