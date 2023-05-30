using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service.ScheduleService.Availability;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service.ScheduleService
{
    public class PatientSchedule : ScheduleBase<string>, IAppointmentAvailable
    {
        private AppointmentService _appointmentService;

        public PatientSchedule()
        {
            _appointmentService = Injector.GetService<AppointmentService>();
            _availabilityValidators = new List<IAvailable<string>> {
               new PatientAppointmentAvailable()
            };
        }

        public Appointment? TryGetReceptionAppointment(string patientJMBG)
        {
            TimeSlot reception = new TimeSlot(DateTime.Now, new TimeSpan(0, 15, 0));

            return _appointmentService
                .GetByPatient(patientJMBG)
                .Find(x => !x.IsOperation && reception.Contains(x.TimeSlot.Start));
        }

        public bool IsAvailable(Appointment appointment)
        {
            return IsAvailable(appointment.PatientJMBG, appointment.TimeSlot);
        }
    }
}
