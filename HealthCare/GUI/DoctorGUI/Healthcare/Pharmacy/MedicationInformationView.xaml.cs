using System.Windows;
using HealthCare.Core.PatientHealthcare.Pharmacy;

namespace HealthCare.GUI.DoctorGUI.Healthcare.Pharmacy
{
    public partial class MedicationInformationView : Window
    {
        public MedicationInformationView(Medication medication)
        {
            InitializeComponent();
            DataContext = new MedicationInformationViewModel(medication);
        }
    }
}