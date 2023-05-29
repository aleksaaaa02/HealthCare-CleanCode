using HealthCare.Model;
using HealthCare.Repository;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service
{
    public class PrescriptionService : NumericService<Prescription>
    {
        public PrescriptionService(IRepository<Prescription> repository) : base(repository) { }

        public List<Prescription> GetPatientsPrescriptions(string JMBG) {
            return GetAll().Where(x => x.PatientJMBG == JMBG).ToList();
        }
    }
}
