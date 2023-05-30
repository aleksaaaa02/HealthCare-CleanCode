using System.Windows;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel;

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