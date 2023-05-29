using HealthCare.Model;
using HealthCare.Repository;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Service
{
    public class AppointmentService : NumericService<Appointment>
    {
        public AppointmentService(IRepository<Appointment> repository) : base(repository) { }

        public List<Appointment> GetByDoctor(string doctorJMBG)
        {
            return GetAll().Where(x => x.DoctorJMBG == doctorJMBG).ToList();
        }
        public List<Appointment> GetByPatient(string patientJMBG)
        {
            return GetAll().Where(x => x.PatientJMBG == patientJMBG).ToList();
        }
        public List<Appointment> GetPossibleIntersections(Appointment appointment)
        {
            return GetAll().FindAll(x =>
                        x.AppointmentID != appointment.AppointmentID &&
                        (x.PatientJMBG.Equals(appointment.PatientJMBG) ||
                        x.DoctorJMBG.Equals(appointment.DoctorJMBG)) &&
                        x.TimeSlot.Overlaps(appointment.TimeSlot));
        }
        public List<string> GetExaminedPatients(string doctorJMBG)
        {
            return GetAll().Where(x => x.DoctorJMBG == doctorJMBG)
                .Select(x => x.PatientJMBG)
                .Distinct().ToList();    
        }

    }
}
