using HealthCare.Repository;

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
        public Prescription() { }
        public Prescription(int id, int medicationId, MealTime instruction, string patientJMBG, int dailyDosage, int hoursBetweenConsumption, int consumptionDays)
        {
            Id = id;
            MedicationId = medicationId;
            Instruction = instruction;
            PatientJMBG = patientJMBG;
            DailyDosage = dailyDosage;
            HoursBetweenConsumption = hoursBetweenConsumption;
            ConsumptionDays = consumptionDays;
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

        }   

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), MedicationId.ToString(), Instruction.ToString(), PatientJMBG, DailyDosage.ToString(), HoursBetweenConsumption.ToString(), ConsumptionDays.ToString() };
        }
    }
}
