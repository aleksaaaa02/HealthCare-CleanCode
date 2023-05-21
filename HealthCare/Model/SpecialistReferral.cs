using HealthCare.Repository;

namespace HealthCare.Model
{
    public class SpecialistReferral : Identifier, ISerializable
    {
        public override object Key { get => Id; set { Id = (int)value; } }
        public int Id { get; set; }
        public string DoctorJMBG { get; set; }
        public string PatientJMBG { get; set; }
        public string Specialization { get; set; }
        public string ReferredDoctorId { get; set; }

        public SpecialistReferral() { }

        public SpecialistReferral(string doctorId, string patientId, int id, string specialization, string referredDoctorId)
        {
            DoctorJMBG = doctorId;
            PatientJMBG = patientId;
            Id = id;
            Specialization = specialization;
            ReferredDoctorId = referredDoctorId;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            DoctorJMBG = values[1];
            PatientJMBG = values[2];
            Specialization = values[3];
            ReferredDoctorId = values[4];

        }

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), DoctorJMBG, PatientJMBG, Specialization, ReferredDoctorId};
        }
    }
}
