using HealthCare.Model;

namespace HealthCare.Service
{
    public class PrescriptionService : NumericService<Prescription>
    {
        public PrescriptionService(string filepath) : base(filepath) {}
    }
}
