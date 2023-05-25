using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.Referrals;
using System.Windows;

namespace HealthCare.View.DoctorView.ReferralView
{
    public partial class TreatmentReferralView : Window
    {
        public TreatmentReferralView(Hospital hospital, Patient patient)
        {
            InitializeComponent();
            DataContext = new TreatmentReferralViewModel(hospital, patient);
        }
    }
}
