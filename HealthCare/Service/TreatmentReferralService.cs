using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    public class TreatmentReferralService : NumericService<TreatmentReferral>
    {
        public TreatmentReferralService(string filepath) : base(filepath) {}
        private TreatmentReferralService(IRepository<TreatmentReferral> repository) : base(repository) { }

        private static TreatmentReferralService? _instance = null;
        public static TreatmentReferralService GetInstance(IRepository<TreatmentReferral> repository)
        {
            if (_instance is not null) return _instance;
            _instance = new TreatmentReferralService(repository);
            return _instance;
        }
    }
}
