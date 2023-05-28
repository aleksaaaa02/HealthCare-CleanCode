using HealthCare.Application;
using HealthCare.Model;
using System.Linq;

namespace HealthCare.Service.ScheduleTest
{
    public class DoctorAppointmentAvailable : IAvailable<string>
    {
        private readonly AppointmentService _appointmentService;   
        public DoctorAppointmentAvailable()
        {
            _appointmentService = Injector.GetService<AppointmentService>();
        }
        public bool IsAvailable(string key, TimeSlot timeSlot)
        {
                    
           return _appointmentService.GetAll().Where(x => x.DoctorJMBG == key).All(x => !x.TimeSlot.Overlaps(timeSlot));
        }
    }
}
