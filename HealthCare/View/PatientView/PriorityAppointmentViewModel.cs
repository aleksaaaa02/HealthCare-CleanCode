using HealthCare.Application;
using HealthCare.Application.Common;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.Service.ScheduleService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthCare.View.AppointmentView
{
    public class PriorityAppointmentViewModel
    {
        private readonly DoctorService _doctorService;
        private readonly PatientService _patientService;
        private readonly Schedule _schedule;

        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }

        public PriorityAppointmentViewModel()
        {
            _patientService = Injector.GetService<PatientService>();
            _doctorService = Injector.GetService<DoctorService>();
            _schedule = Injector.GetService<Schedule>();
            
            Doctors = new ObservableCollection<Doctor>();
            Appointments = new ObservableCollection<Appointment>();
            LoadDoctors(_doctorService.GetAll());
        }

        public void LoadDoctors(List<Doctor> doctors)
        {
            Doctors.Clear();
            foreach (Doctor doctor in doctors)
            {
                Doctors.Add(doctor);
            }
        }

        public void LoadAppointments(List<Appointment> appointments)
        {
            Appointments.Clear();
            foreach (Appointment appointment in appointments)
            {
                Appointments.Add(appointment);
            }
        }

        public void getAppointments(DateTime endDate, int hoursStart, int minutesStart, int hoursEnd, int minutesEnd, Doctor doctor, String priority)
        {
            endDate = endDate.AddHours(hoursEnd);
            endDate = endDate.AddMinutes(minutesEnd);
            Appointment resultAppointment;
            if (priority=="Date")
            {
                resultAppointment = GetAppointmentByDateAndDoctor(endDate, hoursStart, minutesStart, hoursEnd, minutesEnd, doctor);
                if (resultAppointment == null)
                {
                    resultAppointment = GetAppointmentByDate(endDate, hoursStart, minutesStart, hoursEnd, minutesEnd);
                }
            }
            else
            {
                resultAppointment = GetAppointmentByDateAndDoctor(endDate, hoursStart, minutesStart, hoursEnd, minutesEnd, doctor);
                if (resultAppointment == null)
                {
                    resultAppointment = GetAppointmentByDoctor(endDate, hoursStart, minutesStart, hoursEnd, minutesEnd, doctor);
                }
            }
            List<Appointment> appointments = new List<Appointment>();
            if (resultAppointment == null)
            {
                appointments = GetAppointmentByDoctor(doctor);
            }
            else
            {
                appointments.Add(resultAppointment);
            }
            LoadAppointments(appointments);
        }
        public Appointment GetAppointmentByDoctor(DateTime endDate, int hoursStart, int minutesStart, int hoursEnd, int minutesEnd, Doctor doctor)
        {
            DateTime startDate = DateTime.Today;
            startDate = startDate.AddMinutes(15);
            Patient patient = (Patient)Context.Current;
            while (startDate < endDate)
            {
                TimeSlot timeSlot = new TimeSlot(startDate, new TimeSpan(0, 15, 0));
                Appointment appointment = new Appointment(patient.JMBG, doctor.JMBG, timeSlot, false);
                if (_schedule.IsAvailable(appointment))
                {
                    return appointment;
                }
                else
                {
                    startDate = startDate.AddMinutes(15);
                    if (startDate.Hour >= hoursEnd && startDate.Minute>=minutesEnd)
                    {
                        startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day + 1, hoursStart, minutesStart, 0);
                    }
                }
            }
            return null;
        }

        public List<Appointment> GetAppointmentByDoctor(Doctor doctor)
        {
            DateTime startDate = DateTime.Today;
            startDate = startDate.AddMinutes(15);
            List<Appointment> appointments = new List<Appointment>();
            Patient patient = (Patient)Context.Current;
            while (appointments.Count() < 3)
            {
                TimeSlot timeSlot = new TimeSlot(startDate, new TimeSpan(0, 15, 0));
                Appointment appointment = new Appointment(patient.JMBG, doctor.JMBG, timeSlot, false);
                if (_schedule.IsAvailable(appointment))
                {
                    appointments.Add(appointment);
                }
                startDate = startDate.AddMinutes(15);
            }
            return appointments;
        }

        public Appointment GetAppointmentByDate(DateTime endDate, int hoursStart, int minutesStart, int hoursEnd, int minutesEnd)
        {
            List<Doctor> doctors = _doctorService.GetAll();
            foreach (Doctor doctor in doctors)
            {
                DateTime startDate = DateTime.Today;
                startDate = startDate.AddHours(hoursStart);
                startDate = startDate.AddMinutes(minutesStart);
                Patient patient = (Patient)Context.Current;
                while (startDate < endDate)
                {
                    TimeSlot timeSlot = new TimeSlot(startDate, new TimeSpan(0, 15, 0));
                    Appointment appointment = new Appointment(patient.JMBG, doctor.JMBG, timeSlot, false);
                    if (_schedule.IsAvailable(appointment))
                    {
                        return appointment;
                    }
                    startDate = startDate.AddMinutes(15);
                    if (startDate.Hour >= hoursEnd && startDate.Minute >= minutesEnd)
                    {
                        startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day + 1, hoursStart, minutesStart, 0);
                    }
                }
            }
            return null;
        }

        public Appointment GetAppointmentByDateAndDoctor(DateTime endDate, int hoursStart, int minutesStart, int hoursEnd, int minutesEnd, Doctor doctor)
        {

            DateTime startDate = DateTime.Today;
            startDate = startDate.AddHours(hoursStart);
            startDate = startDate.AddMinutes(minutesStart);
            Patient patient = (Patient)Context.Current;
            while (startDate < endDate)
            {
                TimeSlot timeSlot = new TimeSlot(startDate, new TimeSpan(0, 15, 0));
                Appointment appointment = new Appointment(patient.JMBG, doctor.JMBG, timeSlot, false);
                if (_schedule.IsAvailable(appointment))
                {
                    return appointment;
                }
                startDate = startDate.AddMinutes(15);
                if (startDate.Hour >= hoursEnd && startDate.Minute >= minutesEnd)
                {
                    startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day + 1, hoursStart, minutesStart, 0);
                }
            }

            return null;
        }
        public void IsUserBlocked()
        {
            Patient patient = (Patient)Context.Current;
            using (var reader = new StreamReader(Paths.PATIENT_LOGS, Encoding.Default))
            {
                string line;
                int updateDeleteCounter = 0;
                int createCounter = 0;
                while ((line = reader.ReadLine()) != null)
                {

                    string[] values = line.Split('|');
                    if (values[0] == patient.JMBG)
                    {
                        DateTime inputDate = DateTime.Parse(values[2]);
                        DateTime currentDate = DateTime.Now;
                        int daysDifference = (currentDate - inputDate).Days;
                        if (daysDifference < 30)
                        {
                            if (values[1] == "CREATE") createCounter++;
                            if (values[1] == "UPDATE" || values[1] == "DELETE") updateDeleteCounter++;
                        }
                    }


                }
                if (updateDeleteCounter >= 5 || createCounter > 8)
                {
                    patient.Blocked = true;
                }
                else
                {
                    patient.Blocked = false;
                }
                _patientService.Update(patient);
            }
        }

    }
}
