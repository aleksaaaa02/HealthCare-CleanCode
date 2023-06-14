using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Application;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.Service.ScheduleService;
using HealthCare.Service.UserService;

namespace HealthCareCli.DoctorCli
{
    public class AppointmentHandler
    {
        private readonly AppointmentService _appointmentService;
        private readonly Schedule _schedule;
        private readonly PatientService _patientService;
        public AppointmentHandler()
        {
            _appointmentService = Injector.GetService<AppointmentService>();
            _schedule = Injector.GetService<Schedule>();
            _patientService = Injector.GetService<PatientService>();
        }

        public List<Appointment> GetDoctorAppointments()
        {
            return _appointmentService.GetByDoctor(Context.Current.JMBG);
        }

        public bool DeleteAppointment(int AppointmentID)
        {
            Appointment? appointment = _appointmentService.TryGet(AppointmentID);
            if (appointment is null) return false;
            _appointmentService.Remove(AppointmentID);
            return true;
        }

        public bool AddAppointment(Appointment appointment)
        {
            if (_schedule.IsAvailable(appointment))
            {
                _schedule.Add(appointment);
                return true;
            }
            return false;
        }

        public Patient GetPatientFromAppointment(string PatientJMBG)
        {
            Patient patient = _patientService.Get(PatientJMBG);
            return patient;
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _patientService.GetAll();
        }

        public bool PatientExist(string patientJMBG)
        {
            Patient? p = _patientService.TryGet(patientJMBG);
            return p is not null;
        }

        public bool AppointmentExist(int appointmentID)
        {
            Appointment? a = _appointmentService.TryGet(appointmentID);
            return a is not null;
        }

        public Appointment GetAppointment(int appointmentID)
        {
            return _appointmentService.Get(appointmentID);
        }

        public bool UpdateAppointment(Appointment appointment)
        {
            _appointmentService.Remove(appointment.AppointmentID);

            if (_schedule.IsAvailable(appointment))
            {
                _appointmentService.Add(appointment);
                return true;
            }
            _appointmentService.Add(appointment);
            return false;
        }
    }
}
