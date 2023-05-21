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
        public string PatientJMBG { get; set; }
        public int[] Therapy { get; set; }
        public string[] AdditionalExamination { get; set; }

        public TreatmentReferral() { }

        public TreatmentReferral(int id, int daysOfTreatment, string doctorJMBG, string patientJMBG, int[] therapy, string[] additionalExamination)
        {
            Id = id;
            DaysOfTreatment = daysOfTreatment;
            DoctorJMBG = doctorJMBG;
            PatientJMBG = patientJMBG;
            Therapy = therapy;
            AdditionalExamination = additionalExamination;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            DaysOfTreatment = int.Parse(values[1]);
            DoctorJMBG = values[2];
            PatientJMBG = values[3];
            Therapy = Array.ConvertAll(values[4].Split("|"), int.Parse);
            AdditionalExamination = values[5].Split("|");
        }

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), DaysOfTreatment.ToString(), DoctorJMBG, PatientJMBG, Utility.ToString(Therapy), Utility.ToString(AdditionalExamination)};
        }
    }
}
