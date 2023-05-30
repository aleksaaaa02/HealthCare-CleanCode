using System.Collections.Generic;
using HealthCare.Model.Renovation;

namespace HealthCare.Service.RenovationService
{
    public interface IRenovationService
    {
        public IEnumerable<RenovationBase> GetRenovations();
    }
}