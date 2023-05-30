using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service.RenovationService;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service.ScheduleService.Availability
{
    public class RoomRenovationAvailable : IAvailable
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

        public bool IsAvailable(Appointment appointment)
        {
            return _renovationServices.All(s => !s.GetRenovations()
                .Where(x => x.RoomId == appointment.RoomID)
                .Any(x => x.Scheduled.Overlaps(appointment.TimeSlot)));
        }
    }
}
