using HealthCare.Command;
using HealthCare.Application;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
using System.Linq;
using System.Collections.Generic;

namespace HealthCare.ViewModel.DoctorViewModel.PatientInformation.Commands
{
    public class SavePatientChangesCommand : CommandBase
    {
        private readonly PatientInforamtionViewModel _viewModel;
        private readonly Patient _selectedPatient;
        private readonly PatientService _patientService;

        public SavePatientChangesCommand(Patient patient, PatientInforamtionViewModel viewModel)
        {
            _patientService = Injector.GetService<PatientService>();
            _viewModel = viewModel;
            _selectedPatient = patient;
        }

        public override void Execute(object parameter)
        {
            try
            {
                Validate();

                UpdatePatientMedicalRecord();
                ShowSuccessMessage();
            }
            catch (ValidationException ve)
            {
                Utility.ShowWarning(ve.Message);
            }
        }

        private void UpdatePatientMedicalRecord()
        {
            float weight = _viewModel.Weight;
            float height = _viewModel.Height;
            List<string> medicalHistory = _viewModel.PreviousDisease.ToList();
            List<string> allergies = _viewModel.Allergies.ToList();
            MedicalRecord updatedMedicalRecord = new MedicalRecord(height, weight, medicalHistory, allergies);

            _selectedPatient.MedicalRecord = updatedMedicalRecord;
            _patientService.Update(_selectedPatient);
        }

        private void ShowSuccessMessage()
        {
            Utility.ShowInformation("Pacijent uspesno sacuvan!");
        }

        private void Validate()
        {
            if (_viewModel.Weight <= 0)
            {
                throw new ValidationException("Neispravan unos tezine");
            }
            if (_viewModel.Height <= 0)
            {
                throw new ValidationException("Neispravan unos visine");
            }
        }
    }
}

