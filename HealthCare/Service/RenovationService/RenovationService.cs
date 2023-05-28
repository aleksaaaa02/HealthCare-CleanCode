using HealthCare.Model.Renovation;
using System;

namespace HealthCare.Service.RenovationService
{
    public abstract class RenovationService<T> : NumericService<T> where T : BasicRenovation, new()
    {
        protected RenovationService(string filepath) : base(filepath)
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
