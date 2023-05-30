using System.Collections.Generic;
using System.Linq;
using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    public class DoctorService : Service<Doctor>
    {
        public DoctorService(IRepository<Doctor> repository) : base(repository)
        {
        }

        public List<string> GetBySpecialization(string specialization)
        {
            return GetAll().Where(x => x.IsCapable(specialization))
                .Select(x => x.JMBG).ToList();
        }

        public string? GetFirstBySpecialization(string specialization)
        {
            return GetBySpecialization(specialization).FirstOrDefault();
        }

        public List<string> GetSpecializations()
        {
            return GetAll().Select(x => x.Specialization).Distinct().ToList();
        }
    }
}