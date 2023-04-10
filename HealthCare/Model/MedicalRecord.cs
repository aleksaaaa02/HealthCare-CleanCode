﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare
{
    public class MedicalRecord
    {
        public float Height { get; set; }
        public float Weight { get; set; }
        public string[] MedicalHistory;
        public MedicalRecord(float height, float weight, string[] medicalHistory)
        {
            Height = height;
            Weight = weight;
            MedicalHistory = medicalHistory;
        }

        public string[] toCSV()
        {
            string medicalRecord = string.Join("\\|",MedicalHistory);
            string[] csvValues = {Height.ToString(), Weight.ToString(),medicalRecord};
            return csvValues;
        }

        public void fromCSV(string[] values)
        {
            Height = float.Parse(values[0]);
            Weight = float.Parse(values[1]);
            MedicalHistory = values[2].Split("\\|");
        }
    }
}
