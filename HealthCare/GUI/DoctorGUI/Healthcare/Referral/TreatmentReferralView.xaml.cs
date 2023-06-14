using System.Windows;
using HealthCare.Core.Users.Model;

namespace HealthCare.GUI.DoctorGUI.Healthcare.Referral
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