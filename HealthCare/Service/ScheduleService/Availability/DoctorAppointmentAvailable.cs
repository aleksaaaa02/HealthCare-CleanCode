using HealthCare.Application;
using HealthCare.Model;
using System.Linq;

namespace HealthCare.Service.ScheduleService.Availability
{
    public class DoctorAppointmentAvailable : IAvailable
    {
        private readonly AppointmentService _appointmentService;
        public DoctorAppointmentAvailable()
        {
            _appointmentService = Injector.GetService<AppointmentService>();
        }
        public bool IsAvailable(Appointment appointment)
        {
            return _appointmentService.GetAll()
                .Where(x => x.DoctorJMBG == appointment.DoctorJMBG)
                .All(x => !x.TimeSlot.Overlaps(appointment.TimeSlot));
        }
    }
}
