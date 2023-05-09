using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace HealthCare.Command
{
    class ApplyFilterCommand : CommandBase
    {
        private readonly DoctorMainViewModel _doctorMainViewModel;
        private readonly Hospital _hospital;
        public ApplyFilterCommand(Hospital hospital, DoctorMainViewModel viewModel) 
        { 
            _doctorMainViewModel = viewModel;
            _hospital = hospital;
        }

        public override void Execute(object parameter)
        {
            try
            {
                Validate();

                DateTime startDate = _doctorMainViewModel.StartDate;
                int numberOfDays = _doctorMainViewModel.NumberOfDays;
                _doctorMainViewModel.ApplyFilterOnAppointments(Schedule.GetDoctorAppointmentsForDays((Doctor)_hospital.Current, startDate, numberOfDays));
            }
            catch (ValidationException ve)
            {
                MessageBox.Show(ve.Message, "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
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
