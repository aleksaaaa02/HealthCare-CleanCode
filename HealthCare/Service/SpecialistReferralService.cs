using HealthCare.Model;

namespace HealthCare.Service
{
    public class SpecialistReferralService : NumericService<SpecialistReferral>
    {
        public SpecialistReferralService(string filepath) : base(filepath) {}
    }
}
