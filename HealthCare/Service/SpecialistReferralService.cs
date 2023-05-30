using System.Collections.Generic;
using System.Linq;
using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    public class SpecialistReferralService : NumericService<SpecialistReferral>
    {
        public SpecialistReferralService(IRepository<SpecialistReferral> repository) : base(repository)
        {
        }

        public List<SpecialistReferral> GetPatientsReferrals(string patientJMBG)
        {
            return GetAll()
                .Where(x => x.PatientJMBG == patientJMBG && !x.IsUsed)
                .ToList();
        }
    }
}