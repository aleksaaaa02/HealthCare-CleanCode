using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModel.NurseViewModel
{
    public class AppointmentViewModel
    {
        public Appointment Appointment { get; set; }
        public DateTime RescheduleTime { get; set; }
        public AppointmentViewModel(Appointment appointment, DateTime time) {
            Appointment = appointment;
            RescheduleTime = time;
        }
    }
}
