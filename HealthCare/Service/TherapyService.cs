using HealthCare.Model;

namespace HealthCare.Service
{
    public class TherapyService : NumericService<Therapy>
    {
        public TherapyService(string filepath) : base(filepath) {}
    }
}
