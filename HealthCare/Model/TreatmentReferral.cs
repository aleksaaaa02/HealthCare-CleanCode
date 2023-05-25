using HealthCare.Repository;
using HealthCare.Serialize;

namespace HealthCare.Model
{
    public class TreatmentReferral : RepositoryItem
    {
        public override object Key { get => Id; set { Id = (int) value; } }
        public int Id { get; set; }
        public int DaysOfTreatment { get; set; }
        public string DoctorJMBG { get; set; }
        public int TherapyID { get; set; }
        public string[] AdditionalExamination { get; set; }
        public bool IsUsed { get; set; }

        public TreatmentReferral() { }
        public TreatmentReferral(int daysOfTreatment, string doctorJMBG, int therapyId, string[] additionalExamination)
        {
            DaysOfTreatment = daysOfTreatment;
            DoctorJMBG = doctorJMBG;
            TherapyID = therapyId;
            AdditionalExamination = additionalExamination;
            IsUsed = false;
        }

        public override void Deserialize(string[] values)
        {
            Id = int.Parse(values[0]);
            DaysOfTreatment = int.Parse(values[1]);
            DoctorJMBG = values[2];
            IsUsed = bool.Parse(values[3]);
            TherapyID = int.Parse(values[4]);
            AdditionalExamination = values[5].Split("|");
        }

        public override string[] Serialize()
        {
            return new string[] { Id.ToString(), DaysOfTreatment.ToString(), DoctorJMBG, IsUsed.ToString(), TherapyID.ToString(), Utility.ToString(AdditionalExamination)};
        }
    }
}
