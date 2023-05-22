using HealthCare.Model;

namespace HealthCare.Service
{
    public class MedicationService : NumericService<Medication>
    {
        public MedicationService(string filepath) : base(filepath) {}
    

    }
}
