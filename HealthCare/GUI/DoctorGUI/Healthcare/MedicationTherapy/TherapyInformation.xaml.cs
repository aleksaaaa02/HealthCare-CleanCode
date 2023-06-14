using System.Windows;
using HealthCare.Model;

namespace HealthCare.GUI.DoctorGUI.Healthcare.MedicationTherapy
{
    public partial class TherapyInformation : Window
    {
        public TherapyInformation(Patient patient, int medicationID, Model.Therapy therapy)
        {
            InitializeComponent();
            DataContext = new TherapyInformationViewModel(patient, medicationID, therapy, this);
        }
    }
}