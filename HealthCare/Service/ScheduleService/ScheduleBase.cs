using HealthCare.Model;
using HealthCare.Service.ScheduleService.Availability;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service.ScheduleService
{
    public abstract class ScheduleBase<T> : IAvailable<T>
    {
        protected List<IAvailable<T>> _availabilityValidators = new List<IAvailable<T>>();

        public bool IsAvailable(T key, TimeSlot timeSlot)
        {
            return _availabilityValidators.All(x => x.IsAvailable(key, timeSlot));
        }
    }
}
