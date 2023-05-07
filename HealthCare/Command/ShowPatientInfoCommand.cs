using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.View.DoctorView;
using HealthCare.ViewModel.DoctorViewModel;
using HealthCare.ViewModels.DoctorViewModel;

namespace HealthCare.Command
{
    public class ShowPatientInfoCommand : CommandBase
    {
        // Refaktorisi tako da umesto imas oba view spojis u Jednu promenljivu jer nasljeduju ViewModel
        // Nakon toga po isEdit promenljivoj moze se zakljuciti da li je u pitanju D ili P
        private readonly Hospital _hospital;
        private readonly DoctorMainViewModel _doctorViewModel;
        private readonly PatientSearchViewModel _patientSearchViewModel;
        private readonly bool isEdit;
        public ShowPatientInfoCommand(Hospital hospital, DoctorMainViewModel view) 
        { 
            _hospital = hospital;
            _doctorViewModel = view;
            isEdit = false;
        }
        public ShowPatientInfoCommand(Hospital hospital, PatientSearchViewModel view)
        {
            _hospital = hospital;
            _patientSearchViewModel = view;
            isEdit = true;
        }

        public override void Execute(object parameter)
        {
            if (isEdit) {
                EditPatient();
            }
            else
            {
                ShowPatient();    
            }
        }
        private void ShowPatient()
        {
            AppointmentViewModel appointment = _doctorViewModel.SelectedPatient;
            if (appointment != null)
            {
                Patient patient = _hospital.PatientService.GetAccount(appointment.JMBG);
                PatientInformationView patientInformationView = new PatientInformationView(patient, _hospital, false);
                patientInformationView.Show();
            }
            else
            {
                MessageBox.Show("Morate odabrati pregled/operaciju iz tabele!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void EditPatient()
        {
            PatientViewModel selectedPatient = _patientSearchViewModel.SelectedPatient;
            if (selectedPatient != null)
            {
                Patient patient = _hospital.PatientService.GetAccount(selectedPatient.JMBG);
                new PatientInformationView(patient, _hospital, true).Show();
                
            }
            else
            {
                MessageBox.Show("Morate odabrati pacijenta iz tabele!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
