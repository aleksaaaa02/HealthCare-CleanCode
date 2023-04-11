using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModels
{
    public class AppointmentViewModel : BaseViewModel
    {
        private readonly Appointment _appointment;
        public string AppointmentID => _appointment.AppointmentID.ToString();
        public string Patient => _appointment.Patient.Name + " " + _appointment.Patient.LastName;
        public string Doctor => _appointment.Doctor.Name + " " + _appointment.Doctor.LastName;
        public string StartingTime => _appointment.TimeSlot.Start.ToString();
        public string Duration => _appointment.TimeSlot.Duration.ToString();
        public bool IsOperation => _appointment.IsOperation;

            
        public AppointmentViewModel(Appointment appointment)
        {
            _appointment = appointment;
        }

    }
}
