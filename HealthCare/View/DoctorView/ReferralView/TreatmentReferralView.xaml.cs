using HealthCare.Application;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.Referrals;
using System.Windows;

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
