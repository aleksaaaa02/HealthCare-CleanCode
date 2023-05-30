using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service.RenovationService;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service.ScheduleService.Availability
{
    public class RoomRenovationAvailable : IAvailable<int>
    {
        private readonly List<IRenovationService> _renovationServices;

        public RoomRenovationAvailable()
        {
            _renovationServices = new List<IRenovationService>()
            {
                Injector.GetService<SplittingRenovationService>(),
                Injector.GetService<JoiningRenovationService>(),
                Injector.GetService<BasicRenovationService>()
            };
        }

        public bool IsAvailable(int key, TimeSlot timeSlot)
        {
            return _renovationServices.All(s => s.GetRenovations()
                .Where(x => x.RoomId == key)
                .All(x => !x.Scheduled.Overlaps(timeSlot)));
        }
    }
}
