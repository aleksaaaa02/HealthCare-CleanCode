using System;
using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service.UserService;

namespace HealthCare.ViewModel.NurseViewModel.DataViewModel
{
    public class AppointmentViewModel
    {
        public AppointmentViewModel(Appointment appointment, DateTime time)
        {
            Appointment = appointment;
            RescheduleTime = time;
            Doctor = Injector.GetService<DoctorService>().Get(appointment.DoctorJMBG);
            Patient = Injector.GetService<PatientService>().Get(appointment.PatientJMBG);
        }

        public Appointment Appointment { get; set; }
        public DateTime RescheduleTime { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}