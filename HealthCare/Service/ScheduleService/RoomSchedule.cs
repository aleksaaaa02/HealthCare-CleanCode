using HealthCare.Model;
using HealthCare.Service.ScheduleService.Availability;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service.ScheduleService
{
    public class RoomSchedule : ScheduleBase
    {
        public RoomSchedule()
        {
            _availabilityValidators = new List<IAvailable> {
               new RoomRenovationAvailable()
            };
        }
    }
}
