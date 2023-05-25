using HealthCare.Model;

namespace HealthCare.Service
{
    public class TreatmentReferralService : NumericService<TreatmentReferral>
    {
        public TreatmentReferralService(string filepath) : base(filepath) {}
    }
}
