using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service.ScheduleService.Availability;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service.ScheduleService
{
    public class PatientSchedule : ScheduleBase
    {
        private AppointmentService _appointmentService;
        public PatientSchedule()
        {
            _appointmentService = Injector.GetService<AppointmentService>();
            _availabilityValidators = new List<IAvailable> {
               new PatientAppointmentAvailable()
            };
        }

        public Appointment? TryGetReceptionAppointment(Patient patient)
        {
            TimeSlot reception = new TimeSlot(DateTime.Now, new TimeSpan(0, 15, 0));

            return _appointmentService.GetByPatient(patient.JMBG).Find(x =>
                    !x.IsOperation && reception.Contains(x.TimeSlot.Start));
        }

    }
}
