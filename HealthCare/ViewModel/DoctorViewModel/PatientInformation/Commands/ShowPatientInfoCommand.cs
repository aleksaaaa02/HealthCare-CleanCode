﻿using System.Windows;
using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.View;
using HealthCare.View.DoctorView;
using HealthCare.ViewModel;
using HealthCare.ViewModel.DoctorViewModel.Examination;
using HealthCare.ViewModel.DoctorViewModel.PatientInformation;
using HealthCare.ViewModels.DoctorViewModel;

namespace HealthCare.ViewModel.DoctorViewModel.PatientInformation.Commands
{
    public class ShowPatientInfoCommand : CommandBase
    {
        private readonly Hospital _hospital;
        private readonly ViewModelBase _viewModel;
        private readonly bool _isEdit;
        public ShowPatientInfoCommand(Hospital hospital, ViewModelBase view, bool isEdit)
        {
            _hospital = hospital;
            _viewModel = view;
            _isEdit = isEdit;
        }

        public override void Execute(object parameter)
        {
            Patient? patient = ExtractPatient();
            if (patient is null) { return; }

            new PatientInformationView(patient, _hospital, _isEdit).ShowDialog();

            UpdateViewModel();
        }
        private Patient? ExtractPatient()
        {
            if (_viewModel is DoctorMainViewModel doctorMainViewModel)
            {
                var appointment = doctorMainViewModel.SelectedPatient;
                if (appointment is null)
                {
                    MessageBox.Show("Morate odabrati pregled/operaciju iz tabele!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                return _hospital.PatientService.GetAccount(appointment.JMBG);
            }

            if (_viewModel is PatientSearchViewModel patientSearchViewModel)
            {
                var selectedPatient = patientSearchViewModel.SelectedPatient;
                if (selectedPatient is null)
                {
                    MessageBox.Show("Morate odabrati pacijenta iz tabele!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                return _hospital.PatientService.GetAccount(selectedPatient.JMBG);

            }

            if (_viewModel is DoctorExamViewModel doctorExamViewModel)
            {
                var selectedPatient = doctorExamViewModel.SelectedPatient;
                return selectedPatient;
            }

            return null;
        }
        private void UpdateViewModel()
        {
            if (_viewModel is DoctorExamViewModel doctorExamViewModel)
            {
                doctorExamViewModel.RefreshView();
            }
        }
    }
}
