using System.Windows;
using HealthCare.Core.Users.Model;
using HealthCare.ViewModel.NurseViewModel.TreatmantsReferralsMVVM;

namespace HealthCare.View.NurseView.ReferralView
{
    public partial class TreatmantReferralsView : Window
    {
        public TreatmantReferralsView(Patient patient)
        {
            InitializeComponent();
            DataContext = new TreatmantReferralViewModel(patient, this);
        }
    }
}