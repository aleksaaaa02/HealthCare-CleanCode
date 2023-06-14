using System.ComponentModel.DataAnnotations;
using HealthCare.Application;
using HealthCare.Core.Scheduling.Schedules;
using HealthCare.Core.Users.Model;
using HealthCare.GUI.Command;
using HealthCare.View;

namespace HealthCare.GUI.DoctorGUI.AppointmentSchedule.Command;

internal class ApplyFilterCommand : CommandBase
{
    private readonly DoctorMainViewModel _doctorMainViewModel;
    private readonly DoctorSchedule _doctorSchedule;

    public ApplyFilterCommand(DoctorMainViewModel viewModel)
    {
        _doctorMainViewModel = viewModel;
        _doctorSchedule = Injector.GetService<DoctorSchedule>();
    }

    public override void Execute(object parameter)
    {
        try
        {
            Validate();

            var startDate = _doctorMainViewModel.StartDate;
            var numberOfDays = _doctorMainViewModel.NumberOfDays;
            _doctorMainViewModel.ApplyFilterOnAppointments(
                _doctorSchedule.GetAppointmentsForDays((Doctor)Context.Current, startDate, numberOfDays));
        }
        catch (ValidationException ve)
        {
            ViewUtil.ShowWarning(ve.Message);
        }
    }

    private void Validate()
    {
        if (_doctorMainViewModel.NumberOfDays <= 0) throw new ValidationException("Morate Uneti pozitivan broj dana");
    }
}