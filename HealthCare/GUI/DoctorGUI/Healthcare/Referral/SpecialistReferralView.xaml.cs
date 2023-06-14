using System.Windows;
using HealthCare.Model;

namespace HealthCare.GUI.DoctorGUI.Healthcare.Referral
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