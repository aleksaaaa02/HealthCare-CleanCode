using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;

namespace HealthCare.GUI.DoctorGUI.Healthcare.Examination.Command;

public class StartExaminationCommand : CommandBase
{
    private readonly AppointmentService _appointmentService;
    private readonly DoctorMainViewModel _doctorMainViewModelViewModel;
    private Appointment? _appointment;

    public StartExaminationCommand(DoctorMainViewModel viewModel)
    {
        _doctorMainViewModelViewModel = viewModel;
        _appointmentService = Injector.GetService<AppointmentService>();
    }

    public override void Execute(object parameter)
    {
        try
        {
            Validate();
            StartExamination();
        }
        catch (ValidationException ve)
        {
            ViewUtil.ShowWarning(ve.Message);
        }
    }

    private void StartExamination()
    {
        new DoctorExamView(_appointment).Show();
    }

    private void Validate()
    {
        var selectedAppointment = _doctorMainViewModelViewModel.SelectedAppointment;
        if (selectedAppointment is null) throw new ValidationException("Morate odabrati pregled iz tabele!");

        _appointment = _appointmentService.Get(selectedAppointment.AppointmentID);

        if (_appointment.AnamnesisID == 0) throw new ValidationException("Pacijent jos uvek nije primljen!");

        if (!_appointment.HasStarted()) throw new ValidationException("Pregled jos uvek nije poceo!");
    }
}