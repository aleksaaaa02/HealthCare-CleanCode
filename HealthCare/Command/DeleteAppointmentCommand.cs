using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.Collections.Generic;
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
            AppointmentViewModel a = _doctorMainViewModel.SelectedPatient;
            if (a is null)
            {
                MessageBox.Show("Odaberite pregled/operaciju iz tabele!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Appointment appointmnet = Schedule.GetAppointment(Convert.ToInt32(a.AppointmentID));
            if (appointmnet is null)
            {
                MessageBox.Show("Ups Doslo je do greske!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Schedule.DeleteAppointment(appointmnet.AppointmentID);
            _doctorMainViewModel.Update();
            

        }
    }
}
