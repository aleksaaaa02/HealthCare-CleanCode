using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    public class AbsenceRequestService : NumericService<AbsenceRequest>
    {
        public AbsenceRequestService(IRepository<AbsenceRequest> repository) : base(repository)
        {
        }
    }
}
