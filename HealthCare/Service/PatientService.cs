using HealthCare.Model;
using HealthCare.Repository;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service
{
    public class PatientService : Service<Patient>
	{
        public PatientService(IRepository<Patient> repository) : base(repository) { }

        public void UpdatePatientMedicalRecord(Patient patient, MedicalRecord medicalRecord)
        {
			medicalRecord.TreatmentReferrals = patient.MedicalRecord.TreatmentReferrals;
			medicalRecord.SpecialistReferrals = patient.MedicalRecord.SpecialistReferrals;
            patient.MedicalRecord = medicalRecord;
			Update(patient);
		}

		public void AddReferral(string PatientJMBG, int referralID, bool isTreatmentReferral)
		{
			Patient patient = Get(PatientJMBG);
			MedicalRecord? medicalRecord = patient.MedicalRecord;
			if (medicalRecord is null) return;
			
			if (isTreatmentReferral)
			{
                AddTreatmentReferral(medicalRecord, referralID);
			}
			else
			{
				AddSpecialistReferral(medicalRecord, referralID);
			}
			UpdatePatientMedicalRecord(patient, medicalRecord);

		}
        private void AddSpecialistReferral(MedicalRecord medicalRecord, int referralID)
        {
            medicalRecord.SpecialistReferrals = medicalRecord.SpecialistReferrals.Concat(new int[] { referralID }).ToArray();
        }
        private void AddTreatmentReferral(MedicalRecord medicalRecord, int referralID)
        {
            medicalRecord.TreatmentReferrals = medicalRecord.TreatmentReferrals.Concat(new int[] { referralID }).ToArray();
        }
    }
}
