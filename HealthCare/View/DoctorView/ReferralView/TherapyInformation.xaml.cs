using System.Windows;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.Referrals;

namespace HealthCare.View.DoctorView.ReferralView
{
    public partial class TherapyInformation : Window
    {
        public TherapyInformation(Patient patient, int medicationID, Therapy therapy)
        {
            InitializeComponent();
            DataContext = new TherapyInformationViewModel(patient, medicationID, therapy, this);
        }
    }
}