using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    public class TreatmentReferralService : NumericService<TreatmentReferral>
    {
        public TreatmentReferralService(IRepository<TreatmentReferral> repository) : base(repository)
        {
        }
    }
}