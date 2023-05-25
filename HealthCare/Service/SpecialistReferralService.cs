using HealthCare.Model;
using System.Collections.Generic;

namespace HealthCare.Service
{
    public class SpecialistReferralService : NumericService<SpecialistReferral>
    {
        public SpecialistReferralService(string filepath) : base(filepath) {}
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
