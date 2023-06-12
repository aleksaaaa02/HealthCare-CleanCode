using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Repository;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service
{
    public class VisitService : NumericService<Visit>
    {
        public VisitService(IRepository<Visit> repository) : base(repository)
        {
        }

        public List<Visit> getVisits(bool isMorning) {
            return GetAll().Where(x => x.isMorningVisit() == isMorning).ToList();
        }
        
    }
}
