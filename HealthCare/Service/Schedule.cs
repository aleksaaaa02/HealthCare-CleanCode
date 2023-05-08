using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Storage;
using System.Windows;
using HealthCare.Command;

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
            List<Appointment> DoctorAppointments = new List<Appointment>();
            foreach (Appointment appointment in Appointments)
            {
                if (appointment.Doctor.Equals(Doctor))
                {
                    DoctorAppointments.Add(appointment);
                }
            }
            return DoctorAppointments;
        }

        public static List<Appointment> GetDoctorAppointmentsForDays(Doctor doctor, DateTime start, int days)
        {
            List<Appointment> appointments = new List<Appointment>();
            DateTime end = start.AddDays(days);
            foreach (Appointment appointment in GetDoctorAppointments(doctor))
            {
                if (appointment.TimeSlot.InBetweenDates(start, end))
                {
                    appointments.Add(appointment);
                }
            }
            return appointments;

        }
        public static List<Appointment> GetPatientAppointments(Patient Patient)
        {
            List<Appointment> PatientAppointments = new List<Appointment>();
            foreach (Appointment appointment in Appointments)
            {
                if (appointment.Patient.Equals(Patient))
                {
                    PatientAppointments.Add(appointment);
                }
            }
            return PatientAppointments;
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
            if (appointment != null) 
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

        public static Appointment? GetStartingAppointment(string JMBG)
        {
            DateTime reception = DateTime.Now;

            foreach(Appointment appointment in Appointments)
            {
                if(!appointment.IsOperation && reception<appointment.TimeSlot.Start 
                    && reception >= appointment.TimeSlot.Start.AddMinutes(-15) 
                    && appointment.Patient.JMBG == JMBG)
                {
                    return appointment;
                }
            }
            return null;
        }

        public static Appointment? GetSoonest(TimeSpan duration,List<Doctor> specialists)
        {
            DateTime soonest = DateTime.MaxValue;
            Doctor chosen = new Doctor();
            Appointment urgent = new Appointment();

            foreach(Doctor doctor in specialists){
                if (doctor.IsAvailable(new TimeSlot(DateTime.Now, duration))){
                    chosen = doctor;
                    soonest = DateTime.Now;
                    break;
                }

                foreach(Appointment appointment in  GetDoctorAppointments(doctor)){
                    DateTime end = appointment.TimeSlot.GetEnd();
                    if (end> DateTime.Now &&
                        doctor.IsAvailable(new TimeSlot(end, duration))
                        && end < soonest){
                        soonest = end;
                        chosen = doctor;
                    }
                }
            }

            if (soonest > (DateTime.Now + new TimeSpan(2, 0, 0)) )
                return null;
            urgent.Doctor = chosen;
            urgent.TimeSlot = new TimeSlot(soonest, duration);
            return urgent;
        }

        public static List<Appointment> GetPostponable(TimeSpan duration, Doctor specialist)
        {
            List<Appointment> postponable = new List<Appointment>();

            foreach(Appointment appointment in GetDoctorAppointments(specialist))
            {
                DateTime start = appointment.TimeSlot.Start;
                if (start >=DateTime.Now && (DateTime.Now + new TimeSpan(2, 0, 0))>start ){
                    postponable.Add(appointment);
                }
            }

            return FilterPostponable(duration,postponable);
        }

        public static List<Appointment> FilterPostponable(TimeSpan duration,List<Appointment> postponable)
        {
            List<Appointment> filtered = new List<Appointment>();
            postponable=postponable.OrderBy(x=>x.TimeSlot.Start).ToList();

            for (int i = 0; i < postponable.Count-1; i++)
            {
                if (postponable[i].TimeSlot.Start + duration > postponable[i+1].TimeSlot.Start)
                    filtered.Add(postponable[i]);
            }
            return filtered;
        }

        public static List<Appointment> GetPossibleIntersections(Appointment appointment) {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment a in Appointments)
            {
                if (a.Doctor == appointment.Doctor && a.TimeSlot.Start >= appointment.TimeSlot.GetEnd())
                {
                    appointments.Add(appointment);
                }
            }
            return appointments;
        }

        public static DateTime SoonestPostponable(Appointment appointment)
        {
            DateTime postpone = DateTime.MaxValue;
            foreach(Appointment a in GetPossibleIntersections(appointment))
            {
                TimeSlot slot = new TimeSlot(a.TimeSlot.GetEnd(),appointment.TimeSlot.Duration);
                if (appointment.Doctor.IsAvailable(slot) && appointment.Patient.IsAvailable(slot))
                    if (slot.Start < postpone)
                        postpone = slot.Start;
            }
            return postpone;
        }
    }
}
