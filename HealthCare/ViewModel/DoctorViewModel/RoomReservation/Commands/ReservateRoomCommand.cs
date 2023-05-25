using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.View;
using HealthCare.View.DoctorView;
using System.Windows;

namespace HealthCare.ViewModel.DoctorViewModel.RoomReservation.Commands
{
    public class ReserveRoomCommand : CommandBase
    {
        private readonly RoomReservationViewModel _roomReservationViewModel;
        private readonly Window _window;
        private Appointment _appointment;

        public ReserveRoomCommand(Window window, RoomReservationViewModel viewModel, Appointment appointment) { 
            _roomReservationViewModel = viewModel;
            _window = window;
            _appointment = appointment;
        }   

        public override void Execute(object parameter)
        {
            try
            {
                Validate();
                int roomId = _roomReservationViewModel.SelectedRoom.RoomId;
                _window.Close();
                StartExamination(roomId);
            } catch(ValidationException ve)
            {
                Utility.ShowWarning(ve.Message);
            }
        }
        private void StartExamination(int roomId)
        {
            new DoctorExamView(_appointment, roomId).Show();
        }

        private void Validate()
        {
            if (_roomReservationViewModel.SelectedRoom == null)
            {
                throw new ValidationException("Morate odabrati sobu");
            }

        }
    }
}
