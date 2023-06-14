using System.Windows;
using HealthCare.Core.PatientHealthcare.Pharmacy;
using HealthCare.Core.Users.Model;

namespace HealthCare.GUI.DoctorGUI.Healthcare.MedicalPrescription
{
    public partial class PrescriptionView : Window
    {
        public PrescriptionView(Patient patient)
        {
            InitializeComponent();
            DataContext = new PrescriptionViewModel(patient);
        }

        public PrescriptionView(Therapy therapy)
        {
            InitializeComponent();
            DataContext = new PrescriptionViewModel(therapy);
        }
    }
}