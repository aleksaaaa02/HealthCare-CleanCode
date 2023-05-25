using HealthCare.Model;
using System;

namespace HealthCare.ViewModel.NurseViewModel.DataViewModel
{
    public class AppointmentViewModel
    {
        public Appointment Appointment { get; set; }
        public DateTime RescheduleTime { get; set; }
        public AppointmentViewModel(Appointment appointment, DateTime time)
        {
            Appointment = appointment;
            RescheduleTime = time;
        }
    }
}
