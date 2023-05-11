using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthCare.View.AppointmentView
{
    public class PatientRecordViewModel
    {
        public ObservableCollection<Appointment> Appointments { get; set; }
        public Hospital _hospital;
        public List<Appointment> _patientAppointments;
        public PatientRecordViewModel(Hospital hospital)
        {
            _hospital = hospital;
            Appointments = new ObservableCollection<Appointment>();
            _patientAppointments = Schedule.GetPatientAppointments((Patient)_hospital.Current);
            LoadData(Schedule.GetPatientAppointments((Patient)_hospital.Current));
        }

        public void LoadData(List<Appointment> appointments)
        {
            Appointments.Clear();
            foreach(Appointment appointment in appointments)
            {
                Appointments.Add(appointment);
            }
        }

        public void Sort(string sortProperty)
        {
            switch(sortProperty)
            {
                case "Datum":
                    LoadData(Appointments.OrderBy(x => x.TimeSlot.Start).ToList());
                    break;
                case "Doktor":
                    LoadData(Appointments.OrderBy(x => x.Doctor.Name).ToList());
                    break;
                case "Specijalizacija":
                    LoadData(Appointments.OrderBy(x => x.Doctor.Specialization).ToList());
                    break;
                default: break;
            }
        }

        public void Filter(string filterProperty)
        {
            IEnumerable<Appointment> query = (List<Appointment>)_patientAppointments.ToList().Where(
             x => x.Doctor.Name.Contains(filterProperty, StringComparison.OrdinalIgnoreCase) ||
             x.Doctor.Specialization.Contains(filterProperty, StringComparison.OrdinalIgnoreCase) ||
             x.TimeSlot.Start.ToString().Contains(filterProperty, StringComparison.OrdinalIgnoreCase)
            ).ToList();
            LoadData(query.ToList());
        }
    }
}
