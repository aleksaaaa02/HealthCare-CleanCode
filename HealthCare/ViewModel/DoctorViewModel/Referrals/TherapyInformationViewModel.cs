using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.Referrals.Commands;
using System.Windows;
using System.Windows.Input;

namespace HealthCare.ViewModel.DoctorViewModel.Referrals
{
    public class TherapyInformationViewModel : ViewModelBase
    {
        private Hospital _hospital;
        private Medication _medication;
        public int MedicationID => _medication.Id;
        public Therapy Therapy { get; set; }        
        public Patient ExaminedPatient { get; set; }

        public string MedicationName { get; set; }
        public int DailyDosage { get; set; }
        public int HoursBetweenConsumption { get; set; }
        public int ConsumptionDays { get; set; }
        public bool BeforeMeal { get; set; }
        public bool DuringMeal { get; set; }
        public bool AfterMeal { get; set; }
        public bool NoPreference { get; set; }

        public ICommand AddMedicationToTherapyCommand { get; }
        public TherapyInformationViewModel(Hospital hospital, Patient patinet, int medicationID, Therapy therapy, Window window) 
        { 
            _hospital = hospital;
            ExaminedPatient = patinet;
            _medication = hospital.MedicationService.Get(medicationID);
            Therapy = therapy;

            AddMedicationToTherapyCommand = new AddTherapyToReferralCommand(hospital, window, this);

            // OVDE CE DOCI DO IZMENE
            MedicationName = _medication.Name;
            NoPreference = true;
        }
    }
}
