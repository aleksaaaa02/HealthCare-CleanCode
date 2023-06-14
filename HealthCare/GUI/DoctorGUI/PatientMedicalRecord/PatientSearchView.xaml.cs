using System.Windows;

namespace HealthCare.GUI.DoctorGUI.PatientMedicalRecord
{
    public partial class PatientSearchView : Window
    {
        public PatientSearchView()
        {
            InitializeComponent();
            DataContext = new PatientSearchViewModel();
        }
    }
}