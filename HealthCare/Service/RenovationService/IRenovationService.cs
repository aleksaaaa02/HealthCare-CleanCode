using HealthCare.Model.Renovation;
using System.Collections.Generic;

namespace HealthCare.Service.RenovationService
{
    public interface IRenovationService
    {
        public IEnumerable<RenovationBase> GetRenovations();
    }
}
