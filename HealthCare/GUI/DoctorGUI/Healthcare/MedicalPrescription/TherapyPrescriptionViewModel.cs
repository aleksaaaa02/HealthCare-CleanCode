using HealthCare.Application;
using HealthCare.Core.PatientHealthcare.Pharmacy;
using HealthCare.View;
using HealthCare.ViewModel;

namespace HealthCare.GUI.DoctorGUI.Healthcare.MedicalPrescription
{
    public class TherapyPrescriptionViewModel : ViewModelBase
    {
        private readonly Medication _medication;
        private readonly Prescription _prescription;

        public TherapyPrescriptionViewModel(Prescription prescription)
        {
            _prescription = prescription;
            _medication = Injector.GetService<MedicationService>().Get(_prescription.MedicationId);
        }

        public int PrescriptionID => _prescription.Id;
        public int MedicationID => _medication.Id;
        public string MedicationName => _medication.Name;
        public int DailyDosage => _prescription.DailyDosage;
        public int ConsumptionDays => _prescription.ConsumptionDays;
        public string Instruction => ViewUtil.Translate(_prescription.Instruction);
    }
}