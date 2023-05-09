using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
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
                Utility.ShowWarning("Odaberite pregled/operaciju iz tabele!");
                return;
            }
            Appointment appointmnet = Schedule.GetAppointment(Convert.ToInt32(a.AppointmentID));
            if (appointmnet is null)
            {
                Utility.ShowWarning("Ups Doslo je do greske!");
                return;
            }
            Schedule.DeleteAppointment(appointmnet.AppointmentID);
            _doctorMainViewModel.Update();
            

        }
    }
}
