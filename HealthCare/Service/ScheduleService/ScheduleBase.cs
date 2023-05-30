using HealthCare.Model;
using HealthCare.Service.ScheduleService.Availability;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service.ScheduleService
{
    public abstract class ScheduleBase : IAvailable
    {
        protected List<IAvailable> _availabilityValidators = new List<IAvailable>();

        public bool IsAvailable(Appointment appointment)
        {
            return _availabilityValidators.All(x => x.IsAvailable(appointment));
        }
    }
}
