using HealthCare.Model;
using HealthCare.Repository;
using System.Threading;

namespace HealthCare.Service
{
    public class TherapyService : NumericService<Therapy>
    {
        public TherapyService(string filepath) : base(filepath) {}
        public TherapyService(IRepository<Therapy> repository) : base(repository) { }
    }
}
