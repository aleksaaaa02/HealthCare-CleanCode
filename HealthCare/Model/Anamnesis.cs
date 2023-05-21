using HealthCare.Repository;
using HealthCare.Serialize;

namespace HealthCare.Model
{
    public class Anamnesis : RepositoryItem
    {
        public int ID { get; set; }
        public string DoctorsObservations { get; set; }
        public string[] Symptoms { get; set; }

        public Anamnesis()
        {
            ID = 0;
            DoctorsObservations = "";
            Symptoms = new string[0];
        }

        public Anamnesis(int id, string doctorsObservations, string[] symptoms)
        {
            ID = id;
            DoctorsObservations = doctorsObservations;
            Symptoms = symptoms;
        }

        public override object Key
        {
            get => ID;
            set => ID = (int)value;
        }

        public override string[] Serialize()
        {
            string symptoms = string.Join("|", Symptoms);
            string[] csvValues = { ID.ToString(), DoctorsObservations, symptoms };
            return csvValues;
        }

        public override void Deserialize(string[] values)
        {
            ID = int.Parse(values[0]);
            DoctorsObservations = values[1];
            Symptoms = values[2].Split("|");
        }
    }
}
