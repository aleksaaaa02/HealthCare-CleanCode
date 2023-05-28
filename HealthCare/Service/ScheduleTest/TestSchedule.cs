using HealthCare.Application;
using HealthCare.Model;
using System;
using System.Collections.Generic;

namespace HealthCare.Service.ScheduleTest
{
    public class TestSchedule 
    {
        private readonly PatientSchedule _patientSchedule;
        private readonly DoctorSchedule _doctorSchedule;  
        private readonly AppointmentService _appointmentService;
        public TestSchedule()
        {
            _doctorSchedule = new DoctorSchedule();
            _patientSchedule = new PatientSchedule();   
            _appointmentService = Injector.GetService<AppointmentService>();
        }

        public bool CheckAvailability(Appointment appointment, TimeSlot slot)
        {
            return _doctorSchedule.IsAvailable(appointment.Doctor.JMBG, slot) 
                && _patientSchedule.IsAvailable(appointment.Patient.JMBG, slot);
        }
        public DateTime GetSoonestStartingTime(Appointment appointment)
        {
            DateTime postpone = DateTime.MaxValue;
            TimeSlot slot = new TimeSlot(appointment.TimeSlot);

            foreach (Appointment a in _appointmentService.GetAll())
            {
                slot.Start = a.TimeSlot.End;
                if (slot.Start >= DateTime.Now &&
                    slot.Start < postpone &&
                    CheckAvailability(appointment, slot))
                    postpone = slot.Start;
            }

            return postpone;
        }
        public void PostponeAppointment(Appointment appointment)
        {
            appointment.TimeSlot.Start = GetSoonestStartingTime(appointment);
            _appointmentService.Update(appointment);
        }

        public void AddUrgentAppointment(Appointment appointment)
        {
            foreach (Appointment app in _appointmentService.GetPossibleIntersections(appointment))
                PostponeAppointment(app);

            appointment.IsUrgent = true;
            _appointmentService.Add(appointment);
        }

        public Appointment? TryGetUrgent(TimeSpan duration, List<Doctor> specialists)
        {
            Appointment urgent = new Appointment();
            urgent.TimeSlot = new TimeSlot(DateTime.MaxValue, duration);

            foreach (Doctor doctor in specialists)
                urgent = GetUrgentForDoctor(urgent, doctor);

            DateTime twoHours = DateTime.Now + new TimeSpan(2, 0, 0);
            if (urgent.TimeSlot.Start > twoHours)
                return null;
            return urgent;
        }

        private Appointment GetUrgentForDoctor(Appointment urgent, Doctor doctor)
        {
            TimeSpan duration = urgent.TimeSlot.Duration;

            if (doctor.IsAvailable(new TimeSlot(DateTime.Now, duration)))
            {
                urgent.TimeSlot.Start = DateTime.Now;
                urgent.Doctor = doctor;
                return urgent;
            }

            foreach (Appointment appointment in _appointmentService.GetByDoctor(doctor))
            {
                DateTime end = appointment.TimeSlot.End;
                TimeSlot newTimeslot = new TimeSlot(end, duration);

                if (end > DateTime.Now && end < urgent.TimeSlot.Start &&
                    _doctorSchedule.IsAvailable(doctor.JMBG ,newTimeslot) &&
                    _patientSchedule.IsAvailable(urgent.Patient.JMBG, newTimeslot))
                {
                    urgent.TimeSlot.Start = end;
                    urgent.Doctor = doctor;
                }
            }

            return urgent;
        }
    }
}
