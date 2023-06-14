using HealthCare.Model;
using HealthCare.ViewModel.NurseViewModel.TreatmantsReferralsMVVM;
using System.Windows;

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
