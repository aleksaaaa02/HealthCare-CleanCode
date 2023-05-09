using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
using HealthCare.View.DoctorView;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            AppointmentViewModel appointmentViewModel = _doctorMainViewModel.SelectedPatient;
            if (appointmentViewModel is null)
            {
                Utility.ShowWarning("Morate odabrati pregled/operaciju iz tabele!");
                return;
            }

            Appointment selectedAppointment = Schedule.GetAppointment(Convert.ToInt32(appointmentViewModel.AppointmentID));           
            if (selectedAppointment is null)
            {
                Utility.ShowError("Ups doslo je do greske");
                return;
            }
            MakeAppointmentView makeAppointmentView = new MakeAppointmentView(_hospital, _doctorMainViewModel, selectedAppointment);
            makeAppointmentView.ShowDialog();


        }

    }
}
