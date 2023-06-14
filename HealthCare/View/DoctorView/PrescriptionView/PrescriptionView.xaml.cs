using System.Windows;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.Prescriptions;

namespace HealthCare.View.DoctorView.PrescriptionView
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