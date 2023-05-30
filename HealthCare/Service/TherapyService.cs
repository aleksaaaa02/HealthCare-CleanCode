using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    public class TherapyService : NumericService<Therapy>
    {
        public TherapyService(IRepository<Therapy> repository) : base(repository)
        {
        }
    }
}