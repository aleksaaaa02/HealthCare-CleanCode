﻿using HealthCare.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace HealthCare.Model
{
    public class Anamnesis : ISerializable
    {
        public int ID { get; set; }
        public string DoctorsObservations { get; set; }
        public string[] MedicalHistory { get; set; }
        public string[] Symptoms { get; set; }
        public string[] Allergies { get; set; }

        public Anamnesis()
        {
            ID = 0;
            DoctorsObservations = "";
            MedicalHistory = new string[0];
            Symptoms = new string[0];
            Allergies = new string[0];
        }

        public Anamnesis(int id,string doctorsObservations, string[] medicalHistory, string[] symptoms, string[] allergies)
        {
            ID = id;
            DoctorsObservations = doctorsObservations;
            MedicalHistory = medicalHistory;
            Symptoms = symptoms;
            Allergies = allergies;
        }

        public void FromCSV(string[] values)
        {
            ID = int.Parse(values[0]);
            DoctorsObservations = values[1];
            MedicalHistory = values[2].Split("|");
            Symptoms = values[3].Split("|");
            Allergies = values[4].Split("|");
        }

        public string[] ToCSV()
        {
            string medicalHistory = string.Join("|", MedicalHistory);
            string allergies = string.Join("|", Allergies);
            string symptoms = string.Join("|", Symptoms);
            string[] csvValues = { ID.ToString(),DoctorsObservations,medicalHistory,symptoms,allergies};
            return csvValues;
        }
    }
}
