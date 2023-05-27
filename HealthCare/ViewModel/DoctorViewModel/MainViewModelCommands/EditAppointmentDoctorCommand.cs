﻿using HealthCare.Command;
using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
using HealthCare.View.DoctorView;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using HealthCare.ViewModels.DoctorViewModel;
using System.ComponentModel.DataAnnotations;

namespace HealthCare.ViewModel.DoctorViewModel.MainViewModelCommands
{
    public class EditAppointmentDoctorCommand : CommandBase
    {
        private readonly DoctorMainViewModel _doctorMainViewModel;
        public EditAppointmentDoctorCommand(DoctorMainViewModel viewModel)
        {
            _doctorMainViewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            try
            {
                Validate();
                EditSelectedAppointment();
            }
            catch (ValidationException ve)
            {
                Utility.ShowWarning(ve.Message);
            }
        }

        private void EditSelectedAppointment()
        {
            AppointmentViewModel appointmentViewModel = _doctorMainViewModel.SelectedAppointment;
            Appointment selectedAppointment = Schedule.GetAppointment(appointmentViewModel.AppointmentID);
            MakeAppointmentView makeAppointmentView = new MakeAppointmentView(_doctorMainViewModel, selectedAppointment);
            makeAppointmentView.ShowDialog();
        }

        private void Validate()
        {
            AppointmentViewModel appointmentViewModel = _doctorMainViewModel.SelectedAppointment;
            if (appointmentViewModel == null)
            {
                throw new ValidationException("Morate odabrati pregled/operaciju iz tabele!");
            }

            Appointment selectedAppointment = Schedule.GetAppointment(appointmentViewModel.AppointmentID);
            if (selectedAppointment == null)
            {
                throw new ValidationException("Ups doslo je do greske");
            }

        }

    }
}
