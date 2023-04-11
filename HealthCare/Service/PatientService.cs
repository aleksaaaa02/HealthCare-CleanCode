using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Markup;
using HealthCare.Model;
using HealthCare.Observer;
using HealthCare.Storage;
using HealthCare.View.PatientView;

namespace HealthCare.Service
{
	public class PatientService : ISubject
	{
		public List<Patient> Patients = new List<Patient>();
		private CsvStorage<Patient> csvStorage;
		private List<IObserver> observers;

		public PatientService(string filepath)
		{
			csvStorage = new CsvStorage<Patient> (filepath);
            observers = new List<IObserver>();
        }

		public bool CreateAccount(Patient newPatient)
		{
			Patient? patient = Patients.Find(x => x.JMBG == newPatient.JMBG);
			if(patient == null)
			{
                Patients.Add(newPatient);
				return true;
            }
			return false;
		}

		public bool UpdateAccount(Patient updatedPatient)
		{
            Patient? patient = Patients.Find(x => x.JMBG == updatedPatient.JMBG);
            if (patient != null)
			{ 
				int patientIndex = Patients.IndexOf(patient);
				Patients[patientIndex] = updatedPatient;
				return true;
			}
			return false;
        }

		public bool DeleteAccount(string JMBG)
		{
			Patient? patient = Patients.Find(x => x.JMBG == JMBG);
			if (patient != null)
			{
                Patients.Remove(patient);
                return true;
			}
			return false;
        }

		public Patient GetAccount(string JMBG)
		{
			Patient? patient = Patients.Find(x => x.JMBG == JMBG);
            return patient;
		}

		public void Load()
		{
			Patients = csvStorage.Load();
		}
		
		public void Save() 
		{
			csvStorage.Save(Patients);
		}

        public void Subscribe(IObserver observer)
        {
			observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
			observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach(var observer in observers)
			{
				observer.Update();
			}
        }

        public User? GetByUsername(string username)
        {
            return Patients.Find(x => x.UserName == username);
        }
    }
}
