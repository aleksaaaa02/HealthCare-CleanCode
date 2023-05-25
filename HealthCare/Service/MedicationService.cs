using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    public class MedicationService : NumericService<Medication>
    {
        public MedicationService(string filepath) : base(filepath) {}
        public MedicationService(IRepository<Medication> repository) : base(repository) { }
    }
}
