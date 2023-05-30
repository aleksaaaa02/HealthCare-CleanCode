using System;
using System.Collections.Generic;
using HealthCare.Model.Renovation;
using HealthCare.Repository;

namespace HealthCare.Service.RenovationService
{
    public class BasicRenovationService : NumericService<RenovationBase>, IRenovationService
    {
        public BasicRenovationService(IRepository<RenovationBase> repository) : base(repository)
        {
            ExecuteAll();
        }

        public IEnumerable<RenovationBase> GetRenovations()
        {
            return GetAll();
        }

        public void Execute(RenovationBase renovation)
        {
            renovation.Executed = true;
            Update(renovation);
        }

        public void ExecuteAll()
        {
            GetAll().ForEach(x =>
            {
                if (!x.Executed && x.Scheduled.End <= DateTime.Now)
                    Execute(x);
            });
        }
    }
}