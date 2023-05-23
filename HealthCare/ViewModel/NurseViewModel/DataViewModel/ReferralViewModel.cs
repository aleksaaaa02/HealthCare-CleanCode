using HealthCare.Model;

namespace HealthCare.ViewModel.NurseViewModel.DataViewModel
{
    public class ReferralViewModel
    {
        public SpecialistReferral SpecialistReferral { get; set; }
        public string FromName { get; set; }
        public string ToName { get; set; }
        public string ReferredSpecialty { get; set; }

        public ReferralViewModel(SpecialistReferral referral, Doctor from, Doctor to)
        {
            SpecialistReferral = referral;
            FromName = from.Name + " " + from.LastName;
            ToName = to.Name + " " + to.LastName;
            ReferredSpecialty = to.Specialization;
        }
    }
}
