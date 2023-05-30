using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.Service.ScheduleService;
using HealthCare.ViewModel.NurseViewModel;

namespace HealthCare.View.UrgentAppointmentView
{
    public partial class UrgentView : Window
    {
        private readonly DoctorSchedule _doctorSchedule;
        private readonly DoctorService _doctorService;
        private readonly NotificationService _notificationService;
        private readonly Schedule _schedule;
        private PatientViewModel _model;
        private Patient? _patient;

        public UrgentView()
        {
            InitializeComponent();

            _model = new PatientViewModel();
            DataContext = _model;

            _notificationService = Injector.GetService<NotificationService>();
            _doctorService = Injector.GetService<DoctorService>();
            _schedule = Injector.GetService<Schedule>();
            _doctorSchedule = Injector.GetService<DoctorSchedule>();

            _model.Update();
            tbJMBG.IsEnabled = false;
            _patient = null;

            PopulateComboBox();
        }

        private void PopulateComboBox()
        {
            foreach (string specialization in _doctorService.GetSpecializations())
                cbSpecialization.Items.Add(specialization);
            cbSpecialization.SelectedIndex = 0;
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
                ViewUtil.ShowWarning("Izaberite pacijenta i unesite duzinu trajanja u minutima.");
                return;
            }

            TimeSpan duration = new TimeSpan(0, int.Parse(tbDuration.Text), 0);
            List<string> specialists = _doctorService.GetBySpecialization(cbSpecialization.SelectedValue.ToString());

            Appointment? appointment = _schedule.TryGetUrgent(duration, specialists);
            if (appointment is not null)
            {
                appointment = FillAppointmentDetails(appointment);
                _schedule.AddUrgent(appointment);

                _notificationService.Add(new Notification(
                    $"Hitan termin sa ID-jem {appointment.AppointmentID} je kreiran.",
                    _doctorService.Get(appointment.DoctorJMBG).JMBG));

                ViewUtil.ShowInformation("Uspesno kreiran hitan termin.");
                return;
            }

            List<Appointment> postponable = new List<Appointment>();
            foreach (string doctorJmbg in specialists)
                postponable.AddRange(_doctorSchedule.GetPostponable(duration, doctorJmbg));

            postponable = postponable.OrderBy(x => _schedule.GetSoonestTimeSlot(x).Start).ToList();

            appointment = FillAppointmentDetails(appointment);
            appointment.TimeSlot = new TimeSlot(DateTime.MinValue, duration);
            new PostponableAppointmentsView(appointment, postponable).ShowDialog();
        }

        public Appointment FillAppointmentDetails(Appointment? appointment)
        {
            if (appointment is null)
                appointment = new Appointment();

            appointment.PatientJMBG = _patient.JMBG;
            appointment.IsOperation = (cbOperation.IsChecked is bool Checked && Checked);
            return appointment;
        }

        private bool Validate()
        {
            return tbDuration.Text != "" && tbJMBG.Text != null && _patient is not null;
        }

        public void ShowErrorMessageBox(string messageBoxText)
        {
            ViewUtil.ShowWarning(messageBoxText);
        }
    }
}