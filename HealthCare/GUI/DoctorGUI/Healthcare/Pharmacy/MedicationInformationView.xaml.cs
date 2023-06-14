using System.Windows;
using HealthCare.Model;

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