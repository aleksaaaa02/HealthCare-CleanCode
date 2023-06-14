using System.Windows;
using HealthCare.GUI.DoctorGUI.Healthcare.MedicationTherapy.Command;

namespace HealthCare.GUI.DoctorGUI.Healthcare.PatientTreatment
{
    public partial class PatientReleaseAppointmentView : Window
    {
        public PatientReleaseAppointmentView(Model.Treatment treatment)
        {
            InitializeComponent();
            DataContext = new PatientReleaseAppointmentViewModel(this, treatment);
        }
    }
}