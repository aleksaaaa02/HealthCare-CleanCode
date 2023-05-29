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

        public List<Doctor> GetBySpecialization(string specialization)
        {
            return GetAll().Where(x => x.IsCapable(specialization)).ToList();
        }

        public Doctor? GetFirstBySpecialization(string specialization)
        {
            return GetBySpecialization(specialization).FirstOrDefault();
        }

        public List<string> GetSpecializations()
        {
            return GetAll().Select(x => x.Specialization).Distinct().ToList();
        }
    }
}
