using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    public class MedicalRecord : ISerializable
    {
        public float Height { get; set; }
        public float Weight { get; set; }
        public string[] MedicalHistory { get; set; }
        public int[] PastAppointments { get; set; }
        public MedicalRecord(float height, float weight, string[] medicalHistory)
        {
            Height = height;
            Weight = weight;
            MedicalHistory = medicalHistory;
            PastAppointments = new int[0];
        }
        public MedicalRecord(float height, float weight, string[] medicalHistory, int[] pastAppointments)
        {
            Height = height;
            Weight = weight;
            MedicalHistory = medicalHistory;
            PastAppointments = pastAppointments;
        }

        public MedicalRecord() 
        { 
            MedicalHistory = new string[0];
            PastAppointments = new int[0];
        }

        public void addAppointment(int appointmentID)
        {
            PastAppointments = PastAppointments.Concat(new int[] { appointmentID }).ToArray();
        }

        public override string? ToString()
        {
            return "Visina: " + Height.ToString() + "\nTezina: "+ Weight.ToString() + "\nIstorija: " +string.Join(", ", MedicalHistory);
        }

        public string[] ToCSV()
        {
            string medicalHistory = string.Join("|",MedicalHistory);
            string pastAppointments = string.Join("|", PastAppointments);
            string[] csvValues = {Height.ToString(), Weight.ToString(), medicalHistory,pastAppointments};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Height = float.Parse(values[0]);
            Weight = float.Parse(values[1]);
            MedicalHistory = values[2].Split("|");
            string[] pastAppointments = values[3].Split("|", StringSplitOptions.RemoveEmptyEntries);
            PastAppointments = new int[pastAppointments.Length];
            for (int i = 0; i < pastAppointments.Length; i++)
                PastAppointments[i] = int.Parse(pastAppointments[i]);
        }
    }
}
