using HealthCare.Model;
using HealthCare.Repository;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service
{
    public class TreatmentReferralService : NumericService<TreatmentReferral>
    {
        public TreatmentReferralService(IRepository<TreatmentReferral> repository) : base(repository)
        {
        }

        public List<TreatmentReferral> GetPatientsReferrals(string patientJMBG)
        {
            return GetAll()
                .Where(x => x.PatientJMBG == patientJMBG && !x.IsUsed)
                .ToList();
        }
    }
}