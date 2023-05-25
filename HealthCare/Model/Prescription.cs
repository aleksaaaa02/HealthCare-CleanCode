﻿using HealthCare.Repository;
using System;

namespace HealthCare.Model 
{
    public enum MealTime
    {
        BeforeMeal,
        AfterMeal,
        DuringMeal,
        NoPreference
    }

    public class Prescription : Identifier, ISerializable
    {
        public override object Key { get => Id; set { Id = (int) value; } }
        public int Id { get; set; }
        public int MedicationId { get; set; }
        public int DailyDosage { get; set; }
        public int HoursBetweenConsumption { get; set; }
        public int ConsumptionDays { get; set; }
        public MealTime Instruction { get; set; }
        public string PatientJMBG { get; set; }
        public string DoctorJMBG { get; set; }
        public DateTime Start { get; set; }
        public bool FirstUse { get; set; }
        public Prescription() { }
        public Prescription(int medicationId, MealTime instruction, string patientJMBG,string doctorJMBG, int dailyDosage, int hoursBetweenConsumption, int consumptionDays)
        {
            MedicationId = medicationId;
            Instruction = instruction;
            PatientJMBG = patientJMBG;
            DoctorJMBG = doctorJMBG;
            DailyDosage = dailyDosage;
            HoursBetweenConsumption = hoursBetweenConsumption;
            ConsumptionDays = consumptionDays;
            Start = DateTime.Now;
            FirstUse = false;
        }

        public int GetQuantity()
        {
            return DailyDosage * ConsumptionDays;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            MedicationId = int.Parse(values[1]);
            Instruction = Utility.Parse<MealTime>(values[2]);
            PatientJMBG = values[3];
            DoctorJMBG = values[4];
            DailyDosage = int.Parse(values[5]);
            HoursBetweenConsumption = int.Parse(values[6]);
            ConsumptionDays = int.Parse(values[7]);
            Start = Utility.ParseDate(values[8]);
            FirstUse = bool.Parse(values[9]);
        }   

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), MedicationId.ToString(), Instruction.ToString(), PatientJMBG, DoctorJMBG, DailyDosage.ToString(), HoursBetweenConsumption.ToString(), ConsumptionDays.ToString(), Utility.ToString(Start), FirstUse.ToString() };
        }
    }
}
