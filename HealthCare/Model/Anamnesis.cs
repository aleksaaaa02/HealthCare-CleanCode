﻿using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;

namespace HealthCare.Model
{
    public class Anamnesis : Indentifier, ISerializable
    {
        public override object Key { get => ID; set => ID = (int)value; }
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

        public void FromCSV(string[] values)
        {
            ID = int.Parse(values[0]);
            DoctorsObservations = values[1];
            Symptoms = values[2].Split("|");
        }

        public string[] ToCSV()
        {
            string symptoms = string.Join("|", Symptoms);
            string[] csvValues = { ID.ToString(), DoctorsObservations, symptoms };
            return csvValues;
        }
    }
}
