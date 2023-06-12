using System.Collections.Generic;
using System.Linq;
using HealthCare.Model;
using HealthCare.Repository;

namespace HealthCare.Service
{
    public class AbsenceRequestService : NumericService<AbsenceRequest>
    {
        public AbsenceRequestService(IRepository<AbsenceRequest> repository) : base(repository)
        {
        }

        public IEnumerable<AbsenceRequest> GetDoctorRequests(string doctorJMBG)
        {
            return GetAll().Where(x => x.RequesterJMBG == doctorJMBG);
        }
    }
}
