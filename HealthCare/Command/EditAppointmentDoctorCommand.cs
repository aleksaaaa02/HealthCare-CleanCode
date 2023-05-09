using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View.DoctorView;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace HealthCare.Command
{
    public class EditAppointmentDoctorCommand : CommandBase
    {
        private readonly Hospital _hospital;
        private readonly DoctorMainViewModel _doctorMainViewModel;
        public EditAppointmentDoctorCommand(Hospital hospital, DoctorMainViewModel viewModel)
        {
            _hospital = hospital;
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
                MessageBox.Show(ve.Message, "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void EditSelectedAppointment()
        {
            AppointmentViewModel appointmentViewModel = _doctorMainViewModel.SelectedPatient;
            Appointment selectedAppointment = Schedule.GetAppointment(Convert.ToInt32(appointmentViewModel.AppointmentID));
            MakeAppointmentView makeAppointmentView = new MakeAppointmentView(_hospital, _doctorMainViewModel, selectedAppointment);
            makeAppointmentView.ShowDialog();
        }

        private void Validate()
        {
            AppointmentViewModel appointmentViewModel = _doctorMainViewModel.SelectedPatient;
            if (appointmentViewModel == null)
            {
                throw new ValidationException("Morate odabrati pregled/operaciju iz tabele!");
            }

            Appointment selectedAppointment = Schedule.GetAppointment(Convert.ToInt32(appointmentViewModel.AppointmentID));
            if (selectedAppointment == null)
            {
                throw new ValidationException("Ups doslo je do greske");
                
            }

        }

    }
}
