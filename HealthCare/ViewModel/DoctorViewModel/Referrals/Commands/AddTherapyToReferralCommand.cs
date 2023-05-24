using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.View;
using HealthCare.ViewModel.DoctorViewModel.Prescriptions;

namespace HealthCare.ViewModel.DoctorViewModel.Referrals.Commands
{
    public class AddTherapyToReferralCommand : CommandBase
    {
        private readonly Hospital _hospital;
        private readonly TherapyInformationViewModel _therapyInformationViewModel;
        private int _medicationID;
        private Patient _examinedPatient;
        public AddTherapyToReferralCommand(Hospital hospital, TherapyInformationViewModel therapyInformationViewModel)
        {
            _hospital = hospital;
            _therapyInformationViewModel = therapyInformationViewModel;
            _examinedPatient = therapyInformationViewModel.ExaminedPatient;
            _medicationID = therapyInformationViewModel.MedicationID;
        }
        public override void Execute(object parameter)
        {
            try
            {
                Validate();

                int dailyDosage = _therapyInformationViewModel.DailyDosage;
                int hoursBetweenConsumption = _therapyInformationViewModel.HoursBetweenConsumption;
                int consumptionDays = _therapyInformationViewModel.ConsumptionDays;
                MealTime mealTime = GetMealTime();

                Prescription prescription = new Prescription(_medicationID, mealTime, _examinedPatient.JMBG, dailyDosage, hoursBetweenConsumption, consumptionDays);
                _hospital.TherapyPrescriptionService.Add(prescription); 
                _therapyInformationViewModel.Therapy.InitialMedication.Add(prescription.Id);
            
            } catch(ValidationException ve)
            {
                Utility.ShowWarning(ve.Message);
            }

        }
        private MealTime GetMealTime()
        {
            if (_therapyInformationViewModel.BeforeMeal) return MealTime.BeforeMeal;

            if (_therapyInformationViewModel.AfterMeal) return MealTime.AfterMeal;

            if (_therapyInformationViewModel.DuringMeal) return MealTime.DuringMeal;

            return MealTime.NoPreference;
        }
        private void Validate()
        {
            if (_therapyInformationViewModel.DailyDosage <= 0)
                throw new ValidationException("Dnevna doza unosa nije validna");

            if (_therapyInformationViewModel.HoursBetweenConsumption <= 0)
                throw new ValidationException("Sati izmedju konzumacije nisu validni");

            if (_therapyInformationViewModel.ConsumptionDays <= 0)
                throw new ValidationException("Broj dana konzumacije nije validan");

        }

    }
}
