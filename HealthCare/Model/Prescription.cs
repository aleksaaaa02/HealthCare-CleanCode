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
        public MealTime Instruction { get; set; }
        public string PatientId { get; set; }
        public Prescription() { }
        public Prescription(int id, int medicationId, MealTime instruction, string patientId)
        {
            Id = id;
            MedicationId = medicationId;
            Instruction = instruction;
            PatientId = patientId;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            MedicationId = int.Parse(values[1]);
            Instruction = Utility.Parse<MealTime>(values[2]);
            PatientId = values[3];
        }

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), MedicationId.ToString(), Instruction.ToString(), PatientId };
        }
    }
}
