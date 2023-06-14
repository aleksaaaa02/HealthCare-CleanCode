using System.Windows;
using HealthCare.Model;

namespace HealthCare.GUI.DoctorGUI.Healthcare.MedicalPrescription
{
    public partial class PrescriptionView : Window
    {
        public PrescriptionView(Patient patient)
        {
            InitializeComponent();
            DataContext = new PrescriptionViewModel(patient);
        }

        public PrescriptionView(Model.Therapy therapy)
        {
            InitializeComponent();
            DataContext = new PrescriptionViewModel(therapy);
        }
    }
}