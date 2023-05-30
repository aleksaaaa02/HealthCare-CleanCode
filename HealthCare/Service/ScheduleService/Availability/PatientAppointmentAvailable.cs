using HealthCare.Application;
using HealthCare.Model;
using System.Linq;

namespace HealthCare.Service.ScheduleService.Availability
{
    public class PatientAppointmentAvailable : IAvailable<string>
    {
        private readonly AppointmentService _appointmentService;

        public PatientAppointmentAvailable()
        {
            _appointmentService = Injector.GetService<AppointmentService>();
        }

        public bool IsAvailable(string key, TimeSlot timeSlot)
        {
            return _appointmentService.GetAll()
                .Where(x => x.PatientJMBG == key)
                .All(x => !x.TimeSlot.Overlaps(timeSlot));
        }
    }
}
