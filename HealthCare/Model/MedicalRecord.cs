using HealthCare.Repository;
using System;

namespace HealthCare.Model
{
    public class MedicalRecord : ISerializable
    {
        public float Height { get; set; }
        public float Weight { get; set; }
        public string[] MedicalHistory { get; set; }
        public string[] Allergies { get; set; }
        public int[] TreatmentReferrals { get; set; }
        public int[] SpecialistReferrals { get; set; }

        public MedicalRecord() : this(0, 0, new string[0], new string[0]) { }
        public MedicalRecord(float height, float weight, string[] medicalHistory, string[] allergies)
        {
            Height = height;
            Weight = weight;
            MedicalHistory = medicalHistory;
            Allergies = allergies;
            TreatmentReferrals = new int[0];
            SpecialistReferrals = new int[0];
        }
        
        public override string? ToString()
        {
            return "Visina: " + Height.ToString() + "\nTezina: "+ Weight.ToString() + "\nIstorija: " +string.Join(", ", MedicalHistory);
        }
        

        public string[] ToCSV()
        {
            string medicalHistory = Utility.ToString(MedicalHistory);
            string allergies = Utility.ToString(Allergies);
            string treatmentReferrals = Utility.ToString(TreatmentReferrals);
            string specialistReferrals = Utility.ToString(SpecialistReferrals);
            return new string[] {Height.ToString(), Weight.ToString(), medicalHistory, allergies, treatmentReferrals, specialistReferrals};
        }
        
        public void FromCSV(string[] values)
        {
            Height = float.Parse(values[0]);
            Weight = float.Parse(values[1]);
            MedicalHistory = values[2].Split("|");
            Allergies = values[3].Split("|");
            var a = values;
            if (!string.IsNullOrWhiteSpace(values[4])) TreatmentReferrals = Array.ConvertAll(values[4].Split("|"), int.Parse);
            if (!string.IsNullOrWhiteSpace(values[5])) SpecialistReferrals = Array.ConvertAll(values[5].Split("|"), int.Parse);
        }
    }
}
