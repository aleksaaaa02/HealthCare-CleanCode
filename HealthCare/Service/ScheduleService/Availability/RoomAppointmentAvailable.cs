using System.Linq;
using HealthCare.Application;
using HealthCare.Model;

namespace HealthCare.Service.ScheduleService.Availability
{
    public class RoomAppointmentAvailable : IAvailable<int>
    {
        private readonly AppointmentService _appointmentService;

        public RoomAppointmentAvailable()
        {
            _appointmentService = Injector.GetService<AppointmentService>();
        }

        public bool IsAvailable(int key, TimeSlot timeSlot)
        {
            return _appointmentService.GetAll()
                .Where(x => x.RoomID == key)
                .All(x => !x.TimeSlot.Overlaps(timeSlot));
        }
    }
}