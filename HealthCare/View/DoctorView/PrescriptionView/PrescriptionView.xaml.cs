using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.Prescriptions;
using System.Windows;

namespace HealthCare.View.DoctorView.PrescriptionView
{
    public partial class PrescriptionView : Window
    {
        public PrescriptionView(Hospital hospital, Patient patient)
        {
            InitializeComponent();
            DataContext = new PrescriptionViewModel(hospital, patient);
        }
    }
}
