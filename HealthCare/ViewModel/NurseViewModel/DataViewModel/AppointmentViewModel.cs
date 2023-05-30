using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using System;

namespace HealthCare.ViewModel.NurseViewModel.DataViewModel
{
    public class AppointmentViewModel
    {
        public Appointment Appointment { get; set; }
        public DateTime RescheduleTime { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public AppointmentViewModel(Appointment appointment, DateTime time)
        {
            Appointment = appointment;
            RescheduleTime = time;
            Doctor = Injector.GetService<DoctorService>().Get(appointment.DoctorJMBG);
            Patient = Injector.GetService<PatientService>().Get(appointment.PatientJMBG);
        }
    }
}
