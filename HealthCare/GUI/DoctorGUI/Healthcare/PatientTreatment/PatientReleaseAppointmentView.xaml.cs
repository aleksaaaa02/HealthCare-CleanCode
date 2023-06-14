using System.Windows;
using HealthCare.Core.PatientHealthcare.HealthcareTreatment;
using HealthCare.GUI.DoctorGUI.Healthcare.MedicationTherapy.Command;

namespace HealthCare.GUI.DoctorGUI.Healthcare.PatientTreatment
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