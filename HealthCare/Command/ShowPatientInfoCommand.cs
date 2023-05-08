using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.View.DoctorView;
using HealthCare.ViewModel;
using HealthCare.ViewModel.DoctorViewModel;
using HealthCare.ViewModel.DoctorViewModel.Examination;
using HealthCare.ViewModels.DoctorViewModel;

namespace HealthCare.Command
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
            if (patient == null) { return; }

            new PatientInformationView(patient, _hospital, _isEdit).ShowDialog();

            Update();
        }
        private Patient? ExtractPatient()
        {
            Patient? patient = null;

            if (_viewModel is DoctorMainViewModel)
            {
                AppointmentViewModel appointment = ((DoctorMainViewModel)_viewModel).SelectedPatient;
                if (appointment != null)
                {
                    patient = _hospital.PatientService.GetAccount(appointment.JMBG);
                }
                else
                {
                    MessageBox.Show("Morate odabrati pregled/operaciju iz tabele!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (_viewModel is PatientSearchViewModel)
            {
                PatientViewModel selectedPatient = ((PatientSearchViewModel)_viewModel).SelectedPatient;
                if (selectedPatient != null)
                {
                    patient = _hospital.PatientService.GetAccount(selectedPatient.JMBG);
                }

                else
                {
                    MessageBox.Show("Morate odabrati pacijenta iz tabele!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                patient = ((DoctorExamViewModel)_viewModel).SelectedPatient;
                ((DoctorExamViewModel)_viewModel).Update();
            }
                return patient;
        }
        private void Update()
        {
            if (_viewModel is DoctorExamViewModel)
            {
                ((DoctorExamViewModel)_viewModel).Update();
            }
        }
    }
}
