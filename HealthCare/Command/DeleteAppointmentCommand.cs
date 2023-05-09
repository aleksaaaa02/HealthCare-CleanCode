using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace HealthCare.Command
{
    class DeleteAppointmentCommand : CommandBase
    {
        private readonly Hospital _hospital;
        private readonly DoctorMainViewModel _viewModel;
        public DeleteAppointmentCommand(Hospital hospital, DoctorMainViewModel mainViewModel) 
        {
            _hospital = hospital;
            _viewModel = mainViewModel;
        }

        public override void Execute(object parameter)
        {
            try
            {
                Validate();
                AppointmentViewModel a = _viewModel.SelectedPatient;
                Appointment appointmnet = Schedule.GetAppointment(Convert.ToInt32(a.AppointmentID));

                Schedule.DeleteAppointment(appointmnet.AppointmentID);
                _viewModel.Update();
            } catch(ValidationException ve)
            {
                MessageBox.Show(ve.Message, "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Validate()
        {
            
            var selectedAppointmentId = _viewModel.SelectedPatient?.AppointmentID;
            if (selectedAppointmentId is null)
            {
                throw new ValidationException("Odaberite pregled/operaciju iz tabele!");
            }

            Appointment selectedAppointment = Schedule.GetAppointment(Convert.ToInt32(selectedAppointmentId));
            if (selectedAppointment is null)
            {
                throw new ValidationException("Ups Doslo je do greske!");
            }
        }
    }
}
