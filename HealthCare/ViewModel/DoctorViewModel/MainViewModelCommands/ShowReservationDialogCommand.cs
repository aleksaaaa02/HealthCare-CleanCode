using HealthCare.Command;
using HealthCare.Application;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
using HealthCare.View.DoctorView.RoomReservation;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using HealthCare.ViewModels.DoctorViewModel;

namespace HealthCare.ViewModel.DoctorViewModel.MainViewModelCommands
{
    public class ShowReservationDialogCommand : CommandBase
    {
        private readonly DoctorMainViewModel _viewModel;
        private readonly AppointmentService _appointmentService;

        public ShowReservationDialogCommand(DoctorMainViewModel viewModel)
        {
            _viewModel = viewModel;
            _appointmentService = Injector.GetService<AppointmentService>();
        }

        public override void Execute(object parameter)
        {
            try
            {
                Validate();
                AppointmentViewModel selectedAppointment = _viewModel.SelectedAppointment;
                Appointment appointment = _appointmentService.Get(selectedAppointment.AppointmentID);
                new RoomReservationView( appointment).Show();
            }
            catch (ValidationException ve)
            {
                ViewUtil.ShowWarning(ve.Message);
            }
        }

        private void Validate()
        {
            AppointmentViewModel selectedAppointment = _viewModel.SelectedAppointment;
            if (selectedAppointment is null)
            {
                throw new ValidationException("Morate odabrati pregled iz tabele!");
            }

            Appointment appointment = _appointmentService.Get(selectedAppointment.AppointmentID);

            if (appointment.AnamnesisID == 0)
            {
                throw new ValidationException("Pacijent jos uvek nije primljen!");
            }

            if (!appointment.HasStarted())
            {
                throw new ValidationException("Pregled jos uvek nije poceo!");
            }
        }
    }
}
