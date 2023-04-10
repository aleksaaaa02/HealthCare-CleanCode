using HealthCare.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    public class Hospital
    {   
        public string Name { get; set; }
        public DoctorService DoctorService;
        public PatientService PatientService;
            

        public Hospital(string name)
        {
            Name = name;
            DoctorService = new DoctorService("path");
            PatientService = new PatientService("path");
        }

        private void FillAppointmentDetails()
        {
            foreach(Appointment appointment in Schedule.Appointments)
            {
                appointment.Doctor = DoctorService.GetAccount(appointment.Doctor.JMBG);
                appointment.Patient = PatientService.GetAccount(appointment.Patient.JMBG);
            }
        }
    }
}
