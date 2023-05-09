using HealthCare.Context;
using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HealthCare.ViewModel.NurseViewModel;
using HealthCare.Service;

namespace HealthCare.View.UrgentAppointmentView
{
    public partial class PostponableAppointmentsView : Window
    {
        private PostponableAppointmentsViewModel _model;
        private Appointment _selected;
        private Appointment _newAppointment;
        private readonly Hospital _hospital;
        public PostponableAppointmentsView(Appointment newAppointment, List<Appointment> postponable, Hospital hospital)
        {
            InitializeComponent();
            _newAppointment = newAppointment;
            _hospital = hospital;
            _model = new PostponableAppointmentsViewModel(postponable);
            DataContext = _model;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void lvAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selected = (Appointment)lvAppointments.SelectedItem;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (_selected is null)
            {
                ShowErrorMessageBox("Izaberite polje iz tabele");
                return;
            }
            _newAppointment.TimeSlot.Start = _selected.TimeSlot.Start;
            _newAppointment.Doctor = _selected.Doctor;
            Schedule.PostponeAppointment(_selected);
            Schedule.CreateUrgentAppointment(_newAppointment);

            _hospital.NotificationService.Add(new Notification(
                "Termin sa ID-jem " + _selected.AppointmentID + " je pomeren",
                _selected.Doctor.JMBG, _selected.Patient.JMBG));
            _hospital.NotificationService.Add(new Notification(
                "Hitan termin sa ID-jem " + _selected.AppointmentID + " je kreiran.",
                _selected.Doctor.JMBG));

            Utility.ShowInformation("Uspesno odlozen termin.");
            Close();
        }

        public void ShowErrorMessageBox(string messageBoxText)
        {
            string content = "Greska";
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBox.Show(messageBoxText, content, button, icon);
        }

    }
}