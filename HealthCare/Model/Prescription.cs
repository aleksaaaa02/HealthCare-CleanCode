using HealthCare.Repository;
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
        public DateTime Start { get; set; }
        public Prescription() { }
        public Prescription(int medicationId, MealTime instruction, string patientJMBG, int dailyDosage, int hoursBetweenConsumption, int consumptionDays, DateTime start)
        {
            MedicationId = medicationId;
            Instruction = instruction;
            PatientJMBG = patientJMBG;
            DailyDosage = dailyDosage;
            HoursBetweenConsumption = hoursBetweenConsumption;
            ConsumptionDays = consumptionDays;
            Start = start;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            MedicationId = int.Parse(values[1]);
            Instruction = Utility.Parse<MealTime>(values[2]);
            PatientJMBG = values[3];
            DailyDosage = int.Parse(values[4]);
            HoursBetweenConsumption = int.Parse(values[5]);
            ConsumptionDays = int.Parse(values[6]);
            Start = Utility.ParseDate(values[7]);
        }   

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), MedicationId.ToString(), Instruction.ToString(), PatientJMBG, DailyDosage.ToString(), HoursBetweenConsumption.ToString(), ConsumptionDays.ToString(), Utility.ToString(Start) };
        }
    }
}
