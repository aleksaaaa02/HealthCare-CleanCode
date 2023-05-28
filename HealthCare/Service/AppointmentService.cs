using HealthCare.Model;
using HealthCare.Repository;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service
{
    public class AppointmentService : NumericService<Appointment>
    {
        public AppointmentService(IRepository<Appointment> repository) : base(repository) { }

        public List<Appointment> GetByDoctor(Doctor doctor)
        {
            return GetAll().Where(x => x.Doctor.JMBG == doctor.JMBG).ToList();
        }
        public List<Appointment> GetByPatient(Patient patient)
        {
            return GetAll().Where(x => x.Patient.JMBG == patient.JMBG).ToList();
        }
        public List<Appointment> GetPossibleIntersections(Appointment appointment)
        {
            return GetAll().FindAll(x =>
                        x.AppointmentID != appointment.AppointmentID &&
                        (x.Patient.Equals(appointment.Patient) ||
                        x.Doctor.Equals(appointment.Doctor)) &&
                        x.TimeSlot.Overlaps(appointment.TimeSlot));
        }

    }
}
