using System.Windows;

namespace HealthCare.GUI.DoctorGUI.Healthcare.Examination
{
    public partial class DoctorExamView : Window
    {
        public DoctorExamView(Model.Appointment appointment)
        {
            InitializeComponent();
            DataContext = new DoctorExamViewModel(this, appointment);
        }
    }
}