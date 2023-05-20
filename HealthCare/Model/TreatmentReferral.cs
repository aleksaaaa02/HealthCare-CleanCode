using HealthCare.Repository;
using System;

namespace HealthCare.Model
{
    public class TreatmentReferral : Identifier, ISerializable
    {
        public override object Key { get => Id; set { Id = (int) value; } }
        public int Id { get; set; }
        public int DaysOfTreatment { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public int[] Therapy { get; set; }
        public string[] AdditionalExamination { get; set; }

        public TreatmentReferral() { }

        public TreatmentReferral(int id, int daysOfTreatment, string doctorId, string patientId, int[] therapy, string[] additionalExamination)
        {
            Id = id;
            DaysOfTreatment = daysOfTreatment;
            DoctorId = doctorId;
            PatientId = patientId;
            Therapy = therapy;
            AdditionalExamination = additionalExamination;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            DaysOfTreatment = int.Parse(values[1]);
            DoctorId = values[2];
            PatientId = values[3];
            Therapy = Array.ConvertAll(values[4].Split("|"), int.Parse);
            AdditionalExamination = values[5].Split("|");
        }

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), DaysOfTreatment.ToString(), DoctorId, PatientId, Utility.ToString(Therapy), Utility.ToString(AdditionalExamination)};
        }
    }
}
