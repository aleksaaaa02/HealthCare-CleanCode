using HealthCare.Serialize;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Model
{
    public class MedicalRecord : ISerializable
    {
        public float Height { get; set; }
        public float Weight { get; set; }
        public List<string> MedicalHistory { get; set; }
        public List<string> Allergies { get; set; }

        public MedicalRecord() : this(0, 0, new List<string>(), new List<string>()) { }
        public MedicalRecord(float height, float weight, List<string> medicalHistory, List<string> allergies)
        {
            Height = height;
            Weight = weight;
            MedicalHistory = medicalHistory;
            Allergies = allergies;
        }
        
        public override string? ToString()
        {
            return "Visina: " + Height.ToString() + "\nTezina: " + Weight.ToString() + "\nIstorija: " + string.Join(", ", MedicalHistory);
        }
        
        public string[] Serialize()
        {
            string medicalHistory = SerialUtil.ToString(MedicalHistory);
            string allergies = SerialUtil.ToString(Allergies);
            return new string[] {Height.ToString(), Weight.ToString(), medicalHistory, allergies};
        }
        
        public void Deserialize(string[] values)
        {
            Height = float.Parse(values[0]);
            Weight = float.Parse(values[1]);
            MedicalHistory = values[2].Split("|").ToList();
            Allergies = values[3].Split("|").ToList();
        }
    }
}
