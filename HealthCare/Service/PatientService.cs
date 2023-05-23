using HealthCare.Context;
using HealthCare.Model;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service
{
    public class PatientService : Service<Patient>, IUserService
	{
		public PatientService(string filepath) : base(filepath) { }

		public bool CreateAccount(Patient newPatient)
		{
			if (!Contains(newPatient.JMBG))
			{
                Add(newPatient);
				return true;
            }
			return false;
		}

		public bool UpdateAccount(Patient updatedPatient)
		{
            if (Contains(updatedPatient.JMBG))
			{
				Update(updatedPatient);
				return true;
			}
			return false;
        }

		public bool DeleteAccount(string JMBG)
		{
			if (Contains(JMBG))
			{
                Remove(JMBG);
                return true;
			}
			return false;
        }

		public Patient? GetAccount(string JMBG)
		{
            return TryGet(JMBG);
		}

        public void UpdatePatientMedicalRecord(Patient patient, MedicalRecord medicalRecord)
        {
			medicalRecord.TreatmentReferrals = patient.MedicalRecord.TreatmentReferrals;
			medicalRecord.SpecialistReferrals = patient.MedicalRecord.SpecialistReferrals;
            patient.MedicalRecord = medicalRecord;
			Update(patient);
		}

        public User? GetByUsername(string username)
        {
            return GetAll().Find(x => x.UserName == username);
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

		public List<SpecialistReferral> GetPatientsReferrals(Patient patient, Hospital hospital) {
            List<SpecialistReferral> patientsReferrals = new List<SpecialistReferral>();
			SpecialistReferral referral;

			foreach (int id in patient.MedicalRecord.SpecialistReferrals)
			{
				referral = hospital.SpecialistReferralService.Get(id);
				if (!referral.IsUsed)
					patientsReferrals.Add(referral);
			}
			
			return patientsReferrals;
		}

        public UserRole GetRole()
        {
            return UserRole.Patient;
        }
    }
}
