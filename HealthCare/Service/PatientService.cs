using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HealthCare.Model;

namespace HealthCare.Service
{
	public class PatientService
	{
		public List<Patient> Patients = new List<Patient>();

		public void CreateAccount(Patient newPatient)
		{
			Patient patient = Patients.Find(x => x.JMBG == newPatient.JMBG);
			if(patient == null) Patients.Add(newPatient);
		}

		public void UpdateAccount(Patient updatedPatient)
		{
            Patient patient = Patients.Find(x => x.JMBG == updatedPatient.JMBG);
            int patientIndex = Patients.IndexOf(patient);

			if(patientIndex!= -1) Patients[patientIndex] = updatedPatient;
        }

        
		public void DeleteAccount(string JMBG)
		{
			Patient patient = Patients.Find(x => x.JMBG == JMBG);
			if (patient != null) Patients.Remove(patient);
        }

		public Patient GetAccount(string JMBG)
		{
			Patient patient = Patients.Find(x => x.JMBG == JMBG);
			return patient;
		}
		

    }
}
