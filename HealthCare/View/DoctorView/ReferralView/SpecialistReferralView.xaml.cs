using System.Windows;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.Referrals;

namespace HealthCare.View.DoctorView.ReferralView
{
    public partial class SpecialistReferralView : Window
    {
        public SpecialistReferralView(Patient patient)
        {
            InitializeComponent();
            DataContext = new SpecialistReferralViewModel(patient);
        }
    }
}