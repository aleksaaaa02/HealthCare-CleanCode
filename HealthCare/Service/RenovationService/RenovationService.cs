using HealthCare.Model.Renovation;
using HealthCare.Repository;
using System;

namespace HealthCare.Service.RenovationService
{
    public abstract class RenovationService<T> : NumericService<T> where T : BasicRenovation
    {
        public RenovationService(IRepository<T> repository) : base(repository) 
        {
            ExecuteAll();
        }

        public abstract void Execute(T renovation);
        public void ExecuteAll() 
        {
            GetAll().ForEach(x => {
                if (!x.Executed && x.Scheduled.End <= DateTime.Now)
                    Execute(x);
            });
        }
    }
}
