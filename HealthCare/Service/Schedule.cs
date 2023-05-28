using HealthCare.Application;
using HealthCare.Application.Common;
using HealthCare.Model;
using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service
{
    static public class Schedule
    {
        public static List<Appointment> Appointments = new();

        public static int NextId()
        {
            return Appointments.Max(s => s.AppointmentID) + 1;
        }

        // Odvojen if koji zove DoctorSchedule -- DONE -- Schedule
        public static bool CheckAvailability(Appointment appointment, TimeSlot slot)
        {
            return appointment.Doctor.IsAvailable(slot) && appointment.Patient.IsAvailable(slot);
        }

        // AppointmentService -- DONE --
        public static List<Appointment> GetDoctorAppointments(Doctor Doctor)
        {
            return Appointments.Where(x => x.Doctor.Equals(Doctor)).ToList();
        }

        // DoctorSchedule -- DONE --
        public static List<Appointment> GetDoctorAppointmentsForDays(Doctor doctor, DateTime start, int days)
        {
            DateTime end = start.AddDays(days);
            return GetDoctorAppointments(doctor).Where(x => 
                    x.TimeSlot.InBetweenDates(start, end)).ToList();
        }

        // AppointmentService -- DONE --
        public static List<Appointment> GetPatientAppointments(Patient Patient)
        {
            return Appointments.Where(x => x.Patient.Equals(Patient)).ToList();
        }

        // AppointmentService -- DONE ALI TREBA IZMENITI U KODU KOD LJUDI --
        public static bool CreateAppointment(Appointment appointment)
        {
            // Ne treba raditi ovo to se radi na visem nivou
            if (!CheckAvailability(appointment, appointment.TimeSlot) || appointment.TimeSlot.Start<DateTime.Now)
                return false;

            appointment.AppointmentID = NextId();
            Appointments.Add(appointment);
            Save(Paths.APPOINTMENTS);
            return true;
        }

        // AppointmentService -- ISTO SRANJE KAO IZNAD --
        public static bool UpdateAppointment(Appointment updatedAppointment)
        {
            int index = Appointments.FindIndex(x => x.AppointmentID == updatedAppointment.AppointmentID);
            if (index == -1) return false;

            if (!CheckAvailability(updatedAppointment, updatedAppointment.TimeSlot))
                return false;

            Appointments[index] = updatedAppointment;
            Save(Paths.APPOINTMENTS);
            return true;
        }

        // AppointmentService -- DONE --
        public static void DeleteAppointment(int appointmentID)
        {
            Appointment? appointment = Appointments.Find(x => x.AppointmentID == appointmentID);

            if (appointment is not null) 
            {
                Appointments.Remove(appointment);
                Save(Paths.APPOINTMENTS);
            }
        }

        // AppointmentService -- DONE --
        public static Appointment GetAppointment(int appointmentID)
        {
            var appointment = Appointments.Find(x => x.AppointmentID == appointmentID);
            if (appointment is null) throw new KeyNotFoundException();
            return appointment;
        }

        // AppointmentService (NJEMA ME) -- DONE --
        public static void Load(string filepath)
        {
            CsvStorage<Appointment> csvStorage = new CsvStorage<Appointment>(filepath);
            Appointments = csvStorage.Load();

            var doctorService = Injector.GetService<DoctorService>();
            var patientService = Injector.GetService<PatientService>();
            foreach (Appointment appointment in Appointments)
            {
                appointment.Doctor = doctorService.Get(appointment.Doctor.JMBG);
                appointment.Patient = patientService.Get(appointment.Patient.JMBG);
            }
        }

        // NJEMA -- DONE --
        public static void Save(string filepath)
        {
            CsvStorage<Appointment> csvStorage = new CsvStorage<Appointment>(filepath);
            csvStorage.Save(Appointments);
        }

        // PatientSchedule -- DONE --
        public static Appointment? TryGetReceptionAppointment(Patient patient)
        {
            TimeSlot reception = new TimeSlot(DateTime.Now, new TimeSpan(0, 15, 0));

            return GetPatientAppointments(patient).Find(x =>
                    !x.IsOperation && reception.Contains(x.TimeSlot.Start));
        }

        // Schedule
        public static Appointment? TryGetUrgent(TimeSpan duration, List<Doctor> specialists)
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

        // UrgentSchedule ili Schedule
        public static Appointment GetUrgentForDoctor(Appointment urgent, Doctor doctor)
        {
            TimeSpan duration = urgent.TimeSlot.Duration;

            if (doctor.IsAvailable(new TimeSlot(DateTime.Now, duration)))
            {
                urgent.TimeSlot.Start = DateTime.Now;
                urgent.Doctor = doctor;
                return urgent;
            }

            foreach (Appointment appointment in GetDoctorAppointments(doctor))
            {
                DateTime end = appointment.TimeSlot.End;
                TimeSlot newTimeslot = new TimeSlot(end, duration);

                if (end > DateTime.Now && end < urgent.TimeSlot.Start &&
                    doctor.IsAvailable(newTimeslot) &&
                    urgent.Patient.IsAvailable(newTimeslot))
                {
                    urgent.TimeSlot.Start = end;
                    urgent.Doctor = doctor;
                }
            }

            return urgent;
        }
         // DoctorSchedule -- DONE --
        public static List<Appointment> GetPostponable(TimeSpan duration, Doctor specialist)
        {
            var postponable = GetDoctorAppointments(specialist).FindAll(x => x.TimeSlot.Start >= DateTime.Now);
            return FilterPostponable(duration, postponable);
        }
        // DoctorSchedule  -- DONE --
        public static List<Appointment> FilterPostponable(TimeSpan duration, List<Appointment> postponable)
        {
            postponable = postponable.OrderBy(x => x.TimeSlot.Start).ToList();
            var filtered = new List<Appointment>();

            for (int i = 0; i < postponable.Count - 1; i++)
                if (postponable[i].TimeSlot.Start + duration <= postponable[i + 1].TimeSlot.Start)
                    filtered.Add(postponable[i]);

            if (postponable.Count > 0)
                filtered.Add(postponable.Last());

            return filtered;
        }
        // ili Schedule ili AppointmentService -- DONE --
        public static List<Appointment> GetPossibleIntersections(Appointment appointment)
        {
            return Appointments.FindAll(x =>
                        x.AppointmentID != appointment.AppointmentID &&
                        (x.Patient.Equals(appointment.Patient) ||
                        x.Doctor.Equals(appointment.Doctor)) &&
                        x.TimeSlot.Overlaps(appointment.TimeSlot));
        }
        // Schedule -- DONE -- 
        public static DateTime GetSoonestStartingTime(Appointment appointment)
        {
            DateTime postpone = DateTime.MaxValue;
            TimeSlot slot = new TimeSlot(appointment.TimeSlot);

            foreach (Appointment a in Appointments)
            {
                slot.Start = a.TimeSlot.End;
                if (slot.Start >= DateTime.Now &&
                    slot.Start < postpone &&
                    CheckAvailability(appointment, slot))
                    postpone = slot.Start;
            }

            return postpone;
        }
        // Schedule ili AppointmentService -- DONE -- 
        public static void PostponeAppointment(Appointment appointment)
        {
            appointment.TimeSlot.Start = GetSoonestStartingTime(appointment);
            UpdateAppointment(appointment);
        }

        // AppointmentService -- DONE -- 
        public static void CreateUrgentAppointment(Appointment appointment)
        {
            foreach (Appointment app in GetPossibleIntersections(appointment))
                PostponeAppointment(app);

            appointment.AppointmentID = NextId();
            appointment.IsUrgent = true;
            Appointments.Add(appointment);
            Save(Paths.APPOINTMENTS);
        }

        // APPOINTMENT  --DONE--
        public static bool HasAppointmentStarted(Appointment appointment)
        {
            return appointment.TimeSlot.Start < DateTime.Now && appointment.TimeSlot.End > DateTime.Now;
        }
    }
}