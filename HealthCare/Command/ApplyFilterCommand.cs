using HealthCare.Context;
using HealthCare.Service;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthCare.Command
{
    class ApplyFilterCommand : CommandBase
    {
        private DoctorMainViewModel _doctorMainViewModel;
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
                int days = _doctorMainViewModel.Days;
                _doctorMainViewModel.ApplyFilterOn(Schedule.GetDoctorAppointmentsForDays((Model.Doctor)_hospital.Current, startDate, days));
            }
            catch (ValidationException ve)
            {
                MessageBox.Show(ve.Message, "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Validate()
        {
            if (_doctorMainViewModel.Days >= 0)
            {
                throw new ValidationException("Morate Uneti pozitivan broj dana");
            }
        }
    }
}
