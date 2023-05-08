using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthCare.Command
{
    class DeleteAppointmentCommand : CommandBase
    {
        private readonly Hospital _hospital;
        private readonly DoctorMainViewModel _doctorMainViewModel;
        public DeleteAppointmentCommand(Hospital hospital, DoctorMainViewModel mainViewModel) 
        {
            _hospital = hospital;
            _doctorMainViewModel = mainViewModel;
        }

        public override void Execute(object parameter)
        {
            try
            {
                Validate();
                AppointmentViewModel a = _doctorMainViewModel.SelectedPatient;
                Appointment appointmnet = Schedule.GetAppointment(Convert.ToInt32(a.AppointmentID));

                Schedule.DeleteAppointment(appointmnet.AppointmentID);
                _doctorMainViewModel.Update();
            } catch(ValidationException ve)
            {
                MessageBox.Show(ve.Message, "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void Validate()
        {
            AppointmentViewModel a = _doctorMainViewModel.SelectedPatient;
            if (a == null)
            {
                throw new ValidationException("Odaberite pregled/operaciju iz tabele!");
            }
            Appointment appointmnet = Schedule.GetAppointment(Convert.ToInt32(a.AppointmentID));
            if (appointmnet == null)
            {
                throw new ValidationException("Ups Doslo je do greske!");
            }
        }
    }
}
