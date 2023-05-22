using HealthCare.Repository;
using System;

namespace HealthCare.Model
{
    public class TreatmentReferral : Identifier, ISerializable
    {
        public override object Key { get => Id; set { Id = (int) value; } }
        public int Id { get; set; }
        public int DaysOfTreatment { get; set; }
        public string DoctorJMBG { get; set; }
        public int[] Therapy { get; set; }
        public string[] AdditionalExamination { get; set; }
        public bool IsUsed { get; set; }

        public TreatmentReferral() { }
        public TreatmentReferral(int daysOfTreatment, string doctorJMBG, int[] therapy, string[] additionalExamination)
        {
            DaysOfTreatment = daysOfTreatment;
            DoctorJMBG = doctorJMBG;
            Therapy = therapy;
            AdditionalExamination = additionalExamination;
            IsUsed = false;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            DaysOfTreatment = int.Parse(values[1]);
            DoctorJMBG = values[2];
            IsUsed = bool.Parse(values[3]);
            if (!string.IsNullOrWhiteSpace(values[4]))
            {
                Therapy = Array.ConvertAll(values[4].Split("|"), int.Parse);
            } else
            {
                Therapy = new int[0];
            }
            AdditionalExamination = values[5].Split("|");
        }

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), DaysOfTreatment.ToString(), DoctorJMBG, IsUsed.ToString(), Utility.ToString(Therapy), Utility.ToString(AdditionalExamination)};
        }
    }
}
