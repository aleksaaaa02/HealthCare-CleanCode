using HealthCare.Model;
using HealthCare.Service.ScheduleService.Availability;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service.ScheduleService
{
    public class RoomSchedule
    {
        private List<IAvailable<int>> _availabilityValidators;
        public RoomSchedule()
        {
            _availabilityValidators = new List<IAvailable<int>> {
               new RoomRenovationAvailable()
            };
        }

        public bool IsAvailable(int key, TimeSlot timeSlot)
        {
            return _availabilityValidators.All(x => x.IsAvailable(key, timeSlot));
        }
    }
}
