using HealthCare.Model;
using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service
{
    public class DoctorService : Service<Doctor>
    {
        public DoctorService(IRepository<Doctor> repository) : base(repository) { }

        public Doctor GetAccount(string JMBG)
        {
            return Get(JMBG);
        }
        public List<Patient> GetExaminedPatients(Doctor doctor)
        {
            HashSet<Patient> patients = new HashSet<Patient>();
            foreach (var appointment in Schedule.Appointments)
            {
                if (appointment.Doctor.Equals(doctor))
                {
                    patients.Add(appointment.Patient);
                }
            }
            return patients.ToList();
        }

        public List<Doctor> GetBySpecialization(string specialization)
        {
            return GetAll().Where(x => x.IsCapable(specialization)).ToList();
        }

        public Doctor GetFirstBySpecialization(string specialization)
        {
            var doctor = GetBySpecialization(specialization).FirstOrDefault();
            if (doctor is null) 
                throw new ArgumentException("Ne postoji doktor za datu specijalizaciju.");
            return doctor;
        }

        public List<string> GetSpecializations()
        {
            return GetAll().Select(x => x.Specialization).Distinct().ToList();
        }
    }
}
