using HealthCare.Model;
using HealthCare.Model.Renovation;
using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service.RenovationService
{
    public class BasicRenovationService : NumericService<RenovationBase>, IRenovationService
    {
        public BasicRenovationService(IRepository<RenovationBase> repository) : base(repository) 
        {
            ExecuteAll();
        }

        public void Execute(RenovationBase renovation)
        {
            renovation.Executed = true;
            Update(renovation);
        }

        public void ExecuteAll()
        {
            GetAll().ForEach(x => {
                if (!x.Executed && x.Scheduled.End <= DateTime.Now)
                    Execute(x);
            });
        }

        public IEnumerable<RenovationBase> GetRenovations()
        {
            return GetAll();
        }
    }
}
