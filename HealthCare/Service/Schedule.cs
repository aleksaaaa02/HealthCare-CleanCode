using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Storage;
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
        public static List<Appointment> GetDoctorAppointments(Doctor Doctor)
        {
            return Appointments.Where(x => x.Doctor.Equals(Doctor)).ToList();
        }

        public static List<Appointment> GetDoctorAppointmentsForDays(Doctor doctor, DateTime start, int days)
        {
            DateTime end = start.AddDays(days);
            return GetDoctorAppointments(doctor).Where(x => x.TimeSlot.InBetweenDates(start, end)).ToList();
        }
        public static List<Appointment> GetPatientAppointments(Patient Patient)
        {
            return Appointments.Where(x => x.Patient.Equals(Patient)).ToList();
        }

        public static bool CreateAppointment(Appointment appointment)
        {
            if (appointment.Patient.IsAvailable(appointment.TimeSlot) && appointment.Doctor.IsAvailable(appointment.TimeSlot))
            {
                appointment.AppointmentID = NextId();
                Appointments.Add(appointment);
                Save(Global.appointmentPath);
                return true;
            }
            return false;
        }

        public static bool UpdateAppointment(Appointment updatedAppointment)
        {
            Appointment appointment = Appointments.Find(x => x.AppointmentID == updatedAppointment.AppointmentID);
            int appointmentIndex = Appointments.IndexOf(appointment);
            if (appointmentIndex != -1)
            {
                Appointments.RemoveAt(appointmentIndex);
                if (updatedAppointment.Patient.IsAvailable(updatedAppointment.TimeSlot) && updatedAppointment.Doctor.IsAvailable(updatedAppointment.TimeSlot))
                {
                    Appointments.Insert(appointmentIndex, updatedAppointment);
                    Save(Global.appointmentPath);
                    return true;

                }
                else
                {
                    Appointments.Insert(appointmentIndex, appointment);
                    Save(Global.appointmentPath);
                    return false;

                }
            }
            else
            {
                return false;
            }
        }

        public static void DeleteAppointment(int appointmentID)
        {
            Appointment? appointment = Appointments.Find(x => x.AppointmentID == appointmentID);

            if (appointment is not null) 
            {
                Appointments.Remove(appointment);
                Save(Global.appointmentPath);
            }

        }

        public static Appointment GetAppointment(int appointmentID)
        {
            return Appointments.Find(x => x.AppointmentID == appointmentID);
        }


        public static void Load(string filepath)
        {
            CsvStorage<Appointment> csvStorage = new CsvStorage<Appointment>(filepath);
            Appointments = csvStorage.Load();
        }
        public static void Save(string filepath)
        {
            CsvStorage<Appointment> csvStorage = new CsvStorage<Appointment>(filepath);
            csvStorage.Save(Appointments);
        }

        public static bool CheckAvailability(Appointment appointment, TimeSlot slot)
        {
            return appointment.Doctor.IsAvailable(slot) && appointment.Patient.IsAvailable(slot);
        }

        public static Appointment? TryGetReceptionAppointment(Patient patient)
        {
            DateTime reception = DateTime.Now;

            return GetPatientAppointments(patient).Find(x =>
                    !x.IsOperation && reception < x.TimeSlot.Start
                    && reception >= x.TimeSlot.Start.AddMinutes(-15));
        }

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
                DateTime end = appointment.TimeSlot.GetEnd();
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


        public static List<Appointment> GetPostponable(TimeSpan duration, Doctor specialist)
        {
            var postponable = GetDoctorAppointments(specialist).FindAll(x => x.TimeSlot.Start >= DateTime.Now);
            return FilterPostponable(duration, postponable);
        }

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

        public static List<Appointment> GetPossibleIntersections(Appointment appointment)
        {
            return Appointments.FindAll(x =>
                        x.AppointmentID != appointment.AppointmentID &&
                        (x.Patient.Equals(appointment.Patient) ||
                        x.Doctor.Equals(appointment.Doctor)) &&
                        x.TimeSlot.Overlaps(appointment.TimeSlot));
        }

        public static DateTime GetSoonestStartingTime(Appointment appointment)
        {
            DateTime postpone = DateTime.MaxValue;
            TimeSlot slot = new TimeSlot(appointment.TimeSlot);

            foreach (Appointment a in Appointments)
            {
                slot.Start = a.TimeSlot.GetEnd();
                if (slot.Start >= DateTime.Now &&
                    slot.Start < postpone &&
                    CheckAvailability(appointment, slot))
                    postpone = slot.Start;
            }

            return postpone;
        }

        public static void PostponeAppointment(Appointment appointment)
        {
            appointment.TimeSlot.Start = GetSoonestStartingTime(appointment);
            UpdateAppointment(appointment);
        }

        public static void CreateUrgentAppointment(Appointment appointment)
        {
            foreach (Appointment app in GetPossibleIntersections(appointment))
                PostponeAppointment(app);

            appointment.AppointmentID = NextId();
            appointment.IsUrgent = true;
            Appointments.Add(appointment);
            Save(Global.appointmentPath);
        }

        public static bool HasAppointmentStarted(Appointment appointment)
        {
            return appointment.TimeSlot.Start < DateTime.Now && appointment.TimeSlot.GetEnd() > DateTime.Now;
        }

    }
}