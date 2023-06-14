using HealthCare.Model;
using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service
{
    public class TreatmentService : NumericService<Treatment>
    {
        public TreatmentService(IRepository<Treatment> repository) : base(repository)
        {
        }

        public List<Treatment> getCurrent() {
            return GetAll().Where(x => x.TreatmentDuration.Contains(DateTime.Now)).ToList();
        }
    }
}
