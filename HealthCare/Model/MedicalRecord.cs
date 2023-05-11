using HealthCare.Repository;

namespace HealthCare.Model
{
    public class MedicalRecord : ISerializable
    {
        public float Height { get; set; }
        public float Weight { get; set; }
        public string[] MedicalHistory { get; set; }
        public string[] Allergies { get; set; }

        public MedicalRecord(float height, float weight, string[] medicalHistory)
        {
            Height = height;
            Weight = weight;
            MedicalHistory = medicalHistory;
            Allergies = new string[0];
        }
        public MedicalRecord(float height, float weight, string[] medicalHistory, string[] allergies)
        {
            Height = height;
            Weight = weight;
            MedicalHistory = medicalHistory;
            Allergies = allergies;
        }

        public MedicalRecord() 
        { 
            MedicalHistory = new string[0];
            Allergies = new string[0];
        }


        public override string? ToString()
        {
            return "Visina: " + Height.ToString() + "\nTezina: "+ Weight.ToString() + "\nIstorija: " +string.Join(", ", MedicalHistory);
        }

        public string AllergiesToString()
        {
            return string.Join(", ", Allergies);    
        }

        public string MedicalHistoryToString()
        {
            return string.Join(", ", MedicalHistory);
        }

        public string[] ToCSV()
        {
            string medicalHistory = Utility.ToString(MedicalHistory);
            string allergies = Utility.ToString(Allergies);
            return new string[] {Height.ToString(), Weight.ToString(), medicalHistory, allergies};
        }

        public void FromCSV(string[] values)
        {
            Height = float.Parse(values[0]);
            Weight = float.Parse(values[1]);
            MedicalHistory = values[2].Split("|");
            Allergies = values[3].Split("|");
        }
    }
}
