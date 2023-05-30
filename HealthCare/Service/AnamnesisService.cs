using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    public class AnamnesisService : NumericService<Anamnesis>
    {
        public AnamnesisService(IRepository<Anamnesis> repository) : base(repository)
        {
        }
    }
}