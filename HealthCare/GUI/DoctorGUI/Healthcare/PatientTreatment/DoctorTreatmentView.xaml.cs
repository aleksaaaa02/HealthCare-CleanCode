using System.Windows;

namespace HealthCare.GUI.DoctorGUI.Healthcare.PatientTreatment
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