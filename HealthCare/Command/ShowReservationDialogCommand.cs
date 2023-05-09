using HealthCare.Context;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View.DoctorView.RoomReservation;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.Windows;

namespace HealthCare.Command
{
    public class ShowReservationDialogCommand : CommandBase
    {
        private readonly Hospital _hospital;
        private readonly DoctorMainViewModel _viewModel;
        public ShowReservationDialogCommand(Hospital hospital, DoctorMainViewModel viewModel) { 
            _hospital = hospital;
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {

            try
            {
                Validate();
                AppointmentViewModel selectedAppointment = _viewModel.SelectedPatient;
                Appointment appointment = Schedule.GetAppointment(Convert.ToInt32(selectedAppointment.AppointmentID));
                new RoomReservationView(_hospital, appointment).Show();
            }
            catch (ValidationException ve)
            {
                MessageBox.Show(ve.Message, "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void Validate()
        {
            AppointmentViewModel selectedAppointment = _viewModel.SelectedPatient;
            if (selectedAppointment is null)
            {
                throw new ValidationException("Morate odabrati pregled iz tabele!");
            }

            Appointment appointment = Schedule.GetAppointment(Convert.ToInt32(selectedAppointment.AppointmentID));

            if (appointment.AnamnesisID == 0)
            {
                throw new ValidationException("Pacijent jos uvek nije primljen!");
            }

            if (!Schedule.HasAppointmentStarted(appointment))
            {
                throw new ValidationException("Pregled jos uvek nije poceo!");                
            }
        }
    }
}
