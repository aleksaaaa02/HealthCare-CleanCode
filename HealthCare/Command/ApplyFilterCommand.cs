using HealthCare.Context;
using HealthCare.Service;
using HealthCare.View;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.Collections.Generic;
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
        public ApplyFilterCommand(DoctorMainViewModel viewModel, Hospital hospital) 
        { 
            _doctorMainViewModel = viewModel;
            _hospital = hospital;
        }

        public override void Execute(object parameter)
        {
            DateTime startDate = _doctorMainViewModel.StartDate;
            int days = _doctorMainViewModel.Days;
            if (days >= 0)
            {
                _doctorMainViewModel.ApplyFilterOn(Schedule.GetDoctorAppointmentsForDays((Model.Doctor)_hospital.Current, startDate, days));
            }
            else
            {
                Utility.ShowWarning("Morate Uneti pozitivan broj dana");
            }
        }
    }
}
