using HealthCare.Model;
using HealthCare.View;

namespace HealthCare.ViewModel.NurseViewModel.DataViewModel
{
    public class PrescriptionViewModel
    {
        public Prescription Prescription { get; set; }
        public string MedicationName { get; set; }
        public bool IsLow { get; set; }
        public string MealTime => Utility.Translate(Prescription.Instruction);
        public string DoctorName { get; set; }
        public PrescriptionViewModel(Prescription prescription, int Quantity, string medicationName,string doctor)
        {
            Prescription = prescription;
            IsLow = Quantity < 5;
            MedicationName = medicationName;
            DoctorName = doctor;
        }
    }
}
