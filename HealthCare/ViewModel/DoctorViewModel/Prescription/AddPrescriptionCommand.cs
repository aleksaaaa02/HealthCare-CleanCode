using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.View;

namespace HealthCare.ViewModel.DoctorViewModel.Prescriptions
{
    public class AddPrescriptionCommand : CommandBase
    {
        private readonly Hospital _hospital;
        private Patient _patient;
        private PrescriptionViewModel _prescriptionViewModel;
        public AddPrescriptionCommand(Hospital hospital, Patient patient, PrescriptionViewModel prescriptionViewModel) 
        {
            _hospital = hospital;
            _patient = patient;
            _prescriptionViewModel = prescriptionViewModel;
        
        }
        public override void Execute(object parameter)
        {
            try
            {
                Validate();
                MakePrescription();
                Utility.ShowInformation("Recept uspesno izdat!");

            }
            catch (ValidationException ex)
            {
                Utility.ShowWarning(ex.Message);
            }
        }
        private void MakePrescription()
        {
            int dailyDosage = _prescriptionViewModel.DailyDosage;
            int hoursBetweenConsumption = _prescriptionViewModel.HoursBetweenConsumption;
            int consumptionDays = _prescriptionViewModel.ConsumptionDays;
            int selectedMedication = _prescriptionViewModel.SelectedMedication.MedicationId;
            MealTime mealTime = GetMealTime();

            CheckPatientAllergies(_patient, selectedMedication);

            Prescription prescription = new Prescription(selectedMedication, mealTime, _patient.JMBG, dailyDosage, hoursBetweenConsumption, consumptionDays);
            _hospital.PrescriptionService.Add(prescription);
        }
        private MealTime GetMealTime()
        {
            if (_prescriptionViewModel.BeforeMeal) return MealTime.BeforeMeal;

            if (_prescriptionViewModel.AfterMeal) return MealTime.AfterMeal;
            
            if (_prescriptionViewModel.DuringMeal) return MealTime.DuringMeal;

            return MealTime.NoPreference;
        }
        private void Validate()
        {
            if (_prescriptionViewModel.DailyDosage <= 0)
                throw new ValidationException("Dnevna doza unosa nije validna");    
            
            if(_prescriptionViewModel.HoursBetweenConsumption <= 0)
                throw new ValidationException("Sati izmedju konzumacije nisu validni");
            
            if (_prescriptionViewModel.ConsumptionDays <= 0)
                throw new ValidationException("Broj dana konzumacije nije validan");
            
            if (_prescriptionViewModel.SelectedMedication is null)
                throw new ValidationException("Niste odabrali lek"); 
        }
        private void CheckPatientAllergies(Patient patient, int medicationID)
        {
            Medication medication = _hospital.MedicationService.Get(medicationID);

            if (patient.IsAllergic(medication.Ingredients))
                throw new ValidationException("Pacijent je alergican na lek: " + medication.Name);
        }
    }
}
