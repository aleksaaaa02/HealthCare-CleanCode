using HealthCare.Application;
using HealthCare.Model;
using System.Linq;

namespace HealthCare.Service.ScheduleTest
{
    public class PatientAppointmentAvailable
    {
        private readonly AppointmentService _appointmentService;
        public PatientAppointmentAvailable()
        {
            _appointmentService = Injector.GetService<AppointmentService>();
        }
        public bool IsAvailable(string key, TimeSlot timeSlot)
        {
            return _appointmentService.GetAll().All(x => x.Patient.JMBG == key && !x.TimeSlot.Overlaps(timeSlot));
        }
    }
}
