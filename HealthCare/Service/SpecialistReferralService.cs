using HealthCare.Model;
using HealthCare.Repository;
using System.Collections.Generic;

namespace HealthCare.Service
{
    public class SpecialistReferralService : NumericService<SpecialistReferral>
    {
        public SpecialistReferralService(string filepath) : base(filepath) {}
        private SpecialistReferralService(IRepository<SpecialistReferral> repository) : base(repository) { }

        private static SpecialistReferralService? _instance = null;
        public static SpecialistReferralService GetInstance(IRepository<SpecialistReferral> repository)
        {
            if (_instance is not null) return _instance;
            _instance = new SpecialistReferralService(repository);
            return _instance;
        }

        public List<SpecialistReferral> GetPatientsReferrals(Patient patient)
        {
            List<SpecialistReferral> patientsReferrals = new List<SpecialistReferral>();
            SpecialistReferral referral;

            foreach (int id in patient.MedicalRecord.SpecialistReferrals)
            {
                referral = Get(id);
                if (!referral.IsUsed)
                    patientsReferrals.Add(referral);
            }

            return patientsReferrals;
        }
    }
}
