using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    public class MedicationService : NumericService<Medication>
    {
        public MedicationService(string filepath) : base(filepath) {}
        private MedicationService(IRepository<Medication> repository) : base(repository) { }

        private static MedicationService? _instance = null;
        public static MedicationService GetInstance(IRepository<Medication> repository)
        {
            if (_instance is not null) return _instance;
            _instance = new MedicationService(repository);
            return _instance;
        }
    }
}
