using HealthCare.Model;
using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service
{
    public class VisitService : NumericService<Visit>
    {
        public VisitService(IRepository<Visit> repository) : base(repository)
        {
        }

        public List<Visit> getVisits() {
            bool isMorning = DateTime.Now < DateTime.Now.Date.AddHours(12);
            return GetAll().Where(x => x.isMorningVisit() == isMorning && x.isToday()).ToList();
        }
        
    }
}
