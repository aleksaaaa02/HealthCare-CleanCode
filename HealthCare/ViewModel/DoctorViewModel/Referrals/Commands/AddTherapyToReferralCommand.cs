using HealthCare.Command;
using HealthCare.Application;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
using System.Windows;

namespace HealthCare.ViewModel.DoctorViewModel.Referrals.Commands
{
    public class AddTherapyToReferralCommand : CommandBase
    {
        private readonly PrescriptionService _prescriptionService;
        private readonly TherapyInformationViewModel _therapyInformationViewModel;
        private int _medicationID;
        private Patient _examinedPatient;
        private Window _window;
        public AddTherapyToReferralCommand(Window window,TherapyInformationViewModel therapyInformationViewModel)
        {
            _prescriptionService = Injector.GetService<PrescriptionService>(Injector.THERAPY_PRESCRIPTION_S);
            _window = window;
            _therapyInformationViewModel = therapyInformationViewModel;
            _examinedPatient = therapyInformationViewModel.ExaminedPatient;
            _medicationID = therapyInformationViewModel.MedicationID;
        }
        public override void Execute(object parameter)
        {
            try
            {
                Validate();
                MakeReferral();
            }
            catch (ValidationException ve)
            {
                ViewUtil.ShowWarning(ve.Message);
            }

        }

        private void MakeReferral()
        {
            int dailyDosage = _therapyInformationViewModel.DailyDosage;
            int hoursBetweenConsumption = _therapyInformationViewModel.HoursBetweenConsumption;
            int consumptionDays = _therapyInformationViewModel.ConsumptionDays;
            string doctorJMBG = Context.Current.JMBG;
            MealTime mealTime = GetMealTime();

            Prescription prescription = new Prescription(_medicationID, mealTime, _examinedPatient.JMBG, doctorJMBG, dailyDosage, hoursBetweenConsumption, consumptionDays);
            _prescriptionService.Add(prescription);
            _therapyInformationViewModel.Therapy.InitialMedication.Add(prescription.Id);
            _window.Close();
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
