using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.Referrals;
using System.Windows;

namespace HealthCare.View.DoctorView.ReferralView
{
    public partial class TherapyInformation : Window
    {
        public TherapyInformation(Hospital hospital, Patient patient, int medicationID, Therapy therapy)
        {
            InitializeComponent();
            DataContext = new TherapyInformationViewModel(hospital, patient, medicationID, therapy);
        }
    }
}
