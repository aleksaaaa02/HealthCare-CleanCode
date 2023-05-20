using HealthCare.Repository;

namespace HealthCare.Model
{
    public class SpecialistReferral : Identifier, ISerializable
    {
        public override object Key { get => Id; set { Id = (int)value; } }
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public string Specialzation { get; set; }
        public string ReferredDoctorId { get; set; }

        public SpecialistReferral() { }

        public SpecialistReferral(string doctorId, string patientId, int id, string specialzation, string referredDoctorId)
        {
            DoctorId = doctorId;
            PatientId = patientId;
            Id = id;
            Specialzation = specialzation;
            ReferredDoctorId = referredDoctorId;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            DoctorId = values[1];
            PatientId = values[2];
            Specialzation = values[3];
            ReferredDoctorId = values[4];

        }

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), DoctorId, PatientId, Specialzation, ReferredDoctorId};
        }
    }
}
