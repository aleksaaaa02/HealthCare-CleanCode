using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModel.NurseViewModel;
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
using HealthCare.ViewModel;
using System.Reflection.Metadata.Ecma335;

namespace HealthCare.View.UrgentAppointmentView
{
    public partial class UrgentView : Window
    {
        private Hospital _hospital;
        private PatientViewModel _model;
        private Patient? _patient;
        public UrgentView(Hospital hospital)
        {
            this._hospital = hospital;

            InitializeComponent();

            foreach (string specialization in hospital.DoctorService.GetSpecializations())
                cbSpecialization.Items.Add(specialization);
            cbSpecialization.SelectedIndex = 0;

            _model = new PatientViewModel(hospital.PatientService);
            DataContext = _model;

            _model.Update();
            tbJMBG.IsEnabled = false;
            _patient = null;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void lvPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _patient = (Patient)lvPatients.SelectedItem;
            tbJMBG.Text = _patient.JMBG;
        }

        private void cbOperation_Checked(object sender, RoutedEventArgs e)
        {
            tbDuration.Clear();
            tbDuration.IsEnabled = true;
        }

        private void cbAppointment_Checked(object sender, RoutedEventArgs e)
        {
            tbDuration.Text = "15";
            tbDuration.IsEnabled = false;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
            {
                Utility.ShowWarning("Izaberite pacijenta i unesite duzinu trajanja u minutima.");
                return;
            }

            TimeSpan duration = new TimeSpan(0, int.Parse(tbDuration.Text), 0);
            List<Doctor> specialists = _hospital.DoctorService.GetBySpecialization(cbSpecialization.SelectedValue.ToString());

            Appointment? appointment = Schedule.GetUrgent(duration, specialists);
            if (appointment is not null)
            {
                appointment = FillAppointmentDetails(appointment);
                Schedule.CreateUrgentAppointment(appointment);

                _hospital.NotificationService.Add(new Notification(
                "Hitan termin sa ID-jem " + appointment.AppointmentID + " je kreiran.",
                appointment.Doctor.JMBG));

                Utility.ShowInformation("Uspesno kreiran hitan termin.");
                return;
            }

            List<Appointment> postponable = new List<Appointment>();
            foreach (Doctor doctor in specialists)
            {
                postponable.AddRange(Schedule.GetPostponable(duration, doctor));
            }
            postponable = postponable.OrderBy(x => Schedule.GetSoonestStartingTime(x)).ToList();

            appointment = FillAppointmentDetails(appointment);
            appointment.TimeSlot = new TimeSlot(DateTime.MinValue, duration);
            new PostponableAppointmentsView(appointment, postponable, _hospital).ShowDialog();
        }
        public Appointment FillAppointmentDetails(Appointment? appointment)
        {
            if (appointment == null)
                appointment = new Appointment();

            appointment.Patient = _patient;
            appointment.AppointmentID = Schedule.NextId();
            appointment.IsOperation = (cbOperation.IsChecked is bool Checked && Checked);
            return appointment;
        }

        private bool Validate()
        {
            return tbDuration.Text != "" && tbJMBG.Text != null && _patient is not null;
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