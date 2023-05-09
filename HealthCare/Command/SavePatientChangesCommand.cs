﻿using HealthCare.Context;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.View.DoctorView;
using System.Linq;
using System.Windows;

namespace HealthCare.Command
{
    public class SavePatientChangesCommand : CommandBase
    {
        private readonly PatientInforamtionViewModel _viewModel;
        private readonly Hospital _hospital;
        private readonly Patient _selectedPatient;
        public SavePatientChangesCommand(Hospital hospital, Patient patient, PatientInforamtionViewModel viewModel) {
            _hospital = hospital;
            _viewModel = viewModel;
            _selectedPatient = patient;
        }
        public override void Execute(object parameter)
        {
            try
            {
                Validate();

                UpdatePatientMedicalRecord();
                ShowSuccesMessage();
            }
            catch (ValidationException ve)
            {
                MessageBox.Show(ve.Message, "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void UpdatePatientMedicalRecord()
        {
            float weight = _viewModel.Weight;
            float height = _viewModel.Height;
            string[] medicalHistory = _viewModel.PreviousDisease.ToArray();
            
            MedicalRecord updatedMedicalRecord = new MedicalRecord(height, weight, medicalHistory);

            _hospital.PatientService.UpdatePatientMedicalRecord(_selectedPatient, updatedMedicalRecord);

        }

        private void ShowSuccesMessage()
        {
            MessageBox.Show("Pacijent uspesno sacuvan!", "Obavestenje", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void Validate()
        {
            if(_viewModel.Weight <= 0)
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

