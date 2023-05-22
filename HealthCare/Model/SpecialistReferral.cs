using HealthCare.Repository;

namespace HealthCare.Model
{
    public class SpecialistReferral : Identifier, ISerializable
    {
        public override object Key { get => Id; set { Id = (int)value; } }
        public int Id { get; set; }
        public string DoctorJMBG { get; set; }
        public string ReferredDoctorJMBG { get; set; }
        public bool IsUsed { get; set; }
        public SpecialistReferral() { }

        public SpecialistReferral(string doctorJMBG,  string referredDoctorJMBG)
        {
            DoctorJMBG = doctorJMBG;
            ReferredDoctorJMBG = referredDoctorJMBG;
            IsUsed = false;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            DoctorJMBG = values[1];
            ReferredDoctorJMBG = values[2];
            IsUsed = bool.Parse(values[3]);
        }

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), DoctorJMBG, ReferredDoctorJMBG, IsUsed.ToString()};
        }
    }
}
