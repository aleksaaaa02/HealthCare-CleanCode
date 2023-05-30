using HealthCare.Application;
using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service.ScheduleService
{
    public class Schedule 
    {
        private readonly PatientSchedule _patientSchedule;
        private readonly DoctorSchedule _doctorSchedule;  
        private readonly AppointmentService _appointmentService;
        private readonly RoomSchedule _roomSchedule;
        private List<ScheduleBase> _schedules;
        public Schedule()
        {
            _roomSchedule = Injector.GetService<RoomSchedule>();
            _doctorSchedule = Injector.GetService<DoctorSchedule>();
            _patientSchedule = Injector.GetService<PatientSchedule>();
            _appointmentService = Injector.GetService<AppointmentService>();
            _schedules = new List<ScheduleBase>
            {
                _doctorSchedule,
                _patientSchedule,
                _roomSchedule
            
            };
        }

        public bool CheckAvailability(Appointment appointment)
        {
            return _schedules.All(x => x.IsAvailable(appointment));
        }
        public DateTime GetSoonestStartingTime(Appointment appointment)
        {
            DateTime postpone = DateTime.MaxValue;
            TimeSlot slot = new TimeSlot(appointment.TimeSlot);

            foreach (Appointment a in _appointmentService.GetAll())
            {
                slot.Start = a.TimeSlot.End;
                appointment.TimeSlot = slot;
                if (slot.Start >= DateTime.Now &&
                    slot.Start < postpone &&
                    CheckAvailability(appointment))
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
            List<Appointment> possible = new List<Appointment>();
            foreach (Doctor doctor in specialists) { 
                possible.Add(GetUrgentForDoctor(duration, doctor));

            }
            DateTime twoHours = DateTime.Now + new TimeSpan(2, 0, 0);
            Appointment? best = possible.OrderBy(x => x.TimeSlot.Start).FirstOrDefault();
            return best is not null && best.TimeSlot.Start <= twoHours ? best : null;
        }

        private Appointment GetUrgentForDoctor(TimeSpan duration, Doctor doctor)
        {
            Appointment urgent = new Appointment();
            urgent.TimeSlot = new TimeSlot(DateTime.Now, duration);
            urgent.DoctorJMBG = doctor.JMBG;

            if (_doctorSchedule.IsAvailable(urgent))
            {
                urgent.TimeSlot.Start = DateTime.Now;
                urgent.DoctorJMBG = doctor.JMBG;
                return urgent;
            }
            TimeSlot temp;
            urgent.TimeSlot = new TimeSlot(DateTime.MaxValue, duration); 

            foreach (Appointment appointment in _appointmentService.GetByDoctor(doctor.JMBG))
            {
                DateTime end = appointment.TimeSlot.End;
                temp = urgent.TimeSlot;
                urgent.TimeSlot = new TimeSlot(end, duration);
                if (end > DateTime.Now && end < urgent.TimeSlot.Start &&
                    CheckAvailability(urgent))
                {
                    urgent.TimeSlot.Start = end;
                    urgent.DoctorJMBG = doctor.JMBG;
                }
                else
                {
                    urgent.TimeSlot = temp;
                }
            }

            return urgent;
        }
    }
}
