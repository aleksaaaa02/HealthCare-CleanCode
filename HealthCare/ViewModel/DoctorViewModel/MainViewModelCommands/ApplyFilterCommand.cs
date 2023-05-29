using HealthCare.Command;
using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.ComponentModel.DataAnnotations;
using HealthCare.Service.ScheduleService;

namespace HealthCare.ViewModel.DoctorViewModel.MainViewModelCommands
{
    class ApplyFilterCommand : CommandBase
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

                DateTime startDate = _doctorMainViewModel.StartDate;
                int numberOfDays = _doctorMainViewModel.NumberOfDays;
                _doctorMainViewModel.ApplyFilterOnAppointments(_doctorSchedule.GetAppointmentsForDays((Doctor)Context.Current, startDate, numberOfDays));
            }
            catch (ValidationException ve)
            {
                ViewUtil.ShowWarning(ve.Message);
            }
        }
        private void Validate()
        {
            if (_doctorMainViewModel.NumberOfDays <= 0)
            {
                throw new ValidationException("Morate Uneti pozitivan broj dana");
            }
        }
    }
}
