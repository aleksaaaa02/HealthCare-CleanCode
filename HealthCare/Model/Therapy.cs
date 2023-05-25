using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Model
{
    public class Therapy : Identifier, ISerializable
    {
        public override object Key { get => Id; set => Id = (int)value; }
        public int Id { get; set; }
        public List<int> InitialMedication { get; set; } 
        public string PatientJMBG { get; set; }

        public Therapy() { }

        public Therapy(List<int> initialMedication, string patientJMBG) 
        {
            InitialMedication = initialMedication;
            PatientJMBG = patientJMBG;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            PatientJMBG = values[1];
            if (!string.IsNullOrWhiteSpace(values[2]))
            {
                InitialMedication = Array.ConvertAll(values[2].Split("|"), int.Parse).ToList();
            }
            else
            {
                InitialMedication = new List<int>();
            }
        }

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), PatientJMBG, string.Join('|', InitialMedication)};
        }
    }
}
