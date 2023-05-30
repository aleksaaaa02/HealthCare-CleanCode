using HealthCare.Application;
using HealthCare.Model;
using System.Linq;

namespace HealthCare.Service.ScheduleService.Availability
{
    public class PatientAppointmentAvailable : IAvailable
    {
        private readonly AppointmentService _appointmentService;
        public PatientAppointmentAvailable()
        {
            _appointmentService = Injector.GetService<AppointmentService>();
        }
        public bool IsAvailable(Appointment appointment)
        {
            return _appointmentService.GetAll()
                .Where(x => x.PatientJMBG == appointment.PatientJMBG)
                .All(x => !x.TimeSlot.Overlaps(appointment.TimeSlot));
        }
    }
}
