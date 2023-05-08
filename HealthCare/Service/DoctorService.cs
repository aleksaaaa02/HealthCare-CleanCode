using HealthCare.Model;
using HealthCare.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service
{
    public class DoctorService : Service<Doctor>
    {
        public DoctorService(string filePath) : base(filePath) { }  

        public Doctor GetAccount(string JMBG)
        {
            return Get(JMBG);
        }
        public List<Patient> GetExaminedPatients(Doctor doctor)
        {
            HashSet<Patient> patients = new HashSet<Patient>();
            foreach (var appointmnet in Schedule.Appointments)
            {
                if (appointmnet.Doctor.Equals(doctor))
                {
                    patients.Add(appointmnet.Patient);
                }
            }

            return patients.ToList();
        }

        public List<Doctor> GetAccounts() 
        {
            return GetAll();
        }

        public Doctor? GetByUsername(string username)
        {
            return GetAll().Find(x => x.UserName == username);
        }

        public List<Doctor> GetBySpecialization(String specialization)
        {
            List<Doctor> specialists = new List<Doctor>();

            foreach(Doctor doctor in GetAll())
            {
                if (doctor.Specialization == specialization)
                    specialists.Add(doctor);
            }

            return specialists;
        }

        public HashSet<string> GetSpecializations()
        {
            HashSet<string> specializations = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (Doctor doctor in GetAll())
                specializations.Add(doctor.Specialization);
            return specializations;
        }
    }
}
