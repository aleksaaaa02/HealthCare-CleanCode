using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
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
    public class ShowExaminationDialogCommand : CommandBase
    {
        private Hospital _hospital;
        private DoctorMainViewModel _doctorMainView;
        public ShowExaminationDialogCommand(Hospital hospital, DoctorMainViewModel doctorMainView) { 
           _doctorMainView = doctorMainView;
           _hospital = hospital;
        }
        public override void Execute(object parameter)
        {
            AppointmentViewModel  selectedAppointment = _doctorMainView.SelectedPatient;

            if (selectedAppointment == null)
            {
                MessageBox.Show("Morate odabrati pregled iz tabele!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Appointment appointment = Schedule.GetAppointment(Convert.ToInt32(selectedAppointment.AppointmentID));

            new DoctorExamView(_hospital, appointment).Show();
        }
    }
}
