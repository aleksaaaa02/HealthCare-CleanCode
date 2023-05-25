using HealthCare.Model;
using HealthCare.Repository;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service
{
    public class PrescriptionService : NumericService<Prescription>
    {
        public PrescriptionService(string filepath) : base(filepath) {}
        private PrescriptionService(IRepository<Prescription> repository) : base(repository) { }

        private static PrescriptionService? _instance = null;
        public static PrescriptionService GetInstance(IRepository<Prescription> repository)
        {
            if (_instance is not null) return _instance;
            _instance = new PrescriptionService(repository);
            return _instance;
        }

        public List<Prescription> GetPatientsPrescriptions(string JMBG) {
            return GetAll().Where(x => x.PatientJMBG == JMBG).ToList();
        }
    }
}
