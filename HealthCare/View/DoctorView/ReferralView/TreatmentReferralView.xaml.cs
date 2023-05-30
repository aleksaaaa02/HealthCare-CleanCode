using System.Windows;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.Referrals;

namespace HealthCare.View.DoctorView.ReferralView
{
    public partial class TreatmentReferralView : Window
    {
        public TreatmentReferralView(Patient patient)
        {
            InitializeComponent();
            DataContext = new TreatmentReferralViewModel(patient);
        }
    }
}