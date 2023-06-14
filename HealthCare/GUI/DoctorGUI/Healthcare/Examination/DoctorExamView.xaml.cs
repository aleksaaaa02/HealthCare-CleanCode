using System.Windows;
using HealthCare.Core.Scheduling.Examination;

namespace HealthCare.GUI.DoctorGUI.Healthcare.Examination
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