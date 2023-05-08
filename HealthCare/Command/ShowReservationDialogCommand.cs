using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View.DoctorView;
using HealthCare.View.DoctorView.RoomReservation;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthCare.Command
{
    public class ShowReservationDialogCommand : CommandBase
    {
        private readonly Hospital _hospital;
        private readonly DoctorMainViewModel _view;
        public ShowReservationDialogCommand(Hospital hospital, DoctorMainViewModel view) { 
            _hospital = hospital;
            _view = view;
        }

        public override void Execute(object parameter)
        {

            AppointmentViewModel selectedAppointment = _view.SelectedPatient;
            // Moze se validacije izvcu vani da bi funkcija bila 'Clean'
            if (selectedAppointment == null)
            {
                MessageBox.Show("Morate odabrati pregled iz tabele!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Appointment appointment = Schedule.GetAppointment(Convert.ToInt32(selectedAppointment.AppointmentID));

            if(appointment.AnamnesisID == 0)
            {
                MessageBox.Show("Pacijent jos uvek nije primljen!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!Schedule.HasAppointmentStarted(appointment))
            {
                MessageBox.Show("Pregled jos uvek nije poceo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            new RoomReservationView(_hospital, appointment).Show();

        }

    }
}
