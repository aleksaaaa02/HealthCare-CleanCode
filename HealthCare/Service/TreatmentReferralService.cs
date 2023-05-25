using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    public class TreatmentReferralService : NumericService<TreatmentReferral>
    {
        public TreatmentReferralService(string filepath) : base(filepath) {}
        public TreatmentReferralService(IRepository<TreatmentReferral> repository) : base(repository) { }
    }
}
