
using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    public class TreatmentService : NumericService<Treatment>
    {
        public TreatmentService(IRepository<Treatment> repository) : base(repository)
        {
        }
    }
}
