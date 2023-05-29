using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel;
using System.Windows;

namespace HealthCare.View.DoctorView.PrescriptionView
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
