using HealthCare.Application;
using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service.ScheduleTest
{
    public class PatientSchedule : IAvailable<string>
    {
        private AppointmentService _appointmentService;
        private List<IAvailable<string>> _availabilityValidators;
        public PatientSchedule()
        {
            _appointmentService = Injector.GetService<AppointmentService>();
            _availabilityValidators = new List<IAvailable<string>> {
               new PatientAppointmentAvailable()
            };
        }

        public bool IsAvailable(string key, TimeSlot timeSlot)
        {
            return _availabilityValidators.All(x => x.IsAvailable(key, timeSlot));
        }
        public Appointment? TryGetReceptionAppointment(Patient patient)
        {
            TimeSlot reception = new TimeSlot(DateTime.Now, new TimeSpan(0, 15, 0));

            return _appointmentService.GetByPatient(patient.JMBG).Find(x =>
                    !x.IsOperation && reception.Contains(x.TimeSlot.Start));
        }

    }
}
