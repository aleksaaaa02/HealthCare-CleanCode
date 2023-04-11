using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.View.DoctorView;
using HealthCare.ViewModels.DoctorViewModel;

namespace HealthCare.Command
{
    public class ShowPatientInfoCommand : CommandBase
    {
        private readonly Hospital _hospital;
        private readonly DoctorMainViewModel _doctorViewModel;
        public ShowPatientInfoCommand(Hospital hospital, DoctorMainViewModel view) 
        { 
            _hospital = hospital;
            _doctorViewModel = view;
        }

        public override void Execute(object parameter)
        {
            AppointmentViewModel appointment = _doctorViewModel.SelectedPatient;
            if (appointment != null)
            {
                Patient patient = _hospital.PatientService.GetAccount(appointment.JMBG);
                PatientInformationView patientInformationView = new PatientInformationView(patient);
                patientInformationView.Show();
            }
            else
            {
                MessageBox.Show("Morate odabrati pregled/operaciju iz tabele!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
