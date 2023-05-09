﻿using HealthCare.Context;
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
        private Hospital hospital;
        private PatientViewModel vm;
        private Patient? patient;
        public UrgentView(Hospital hospital)
        {
            this.hospital = hospital;

            InitializeComponent();

            foreach (String specialization in hospital.DoctorService.GetSpecializations())
                cbSpecialization.Items.Add(specialization);
            cbSpecialization.SelectedIndex = 0;

            vm = new PatientViewModel(hospital.PatientService);
            DataContext = vm;

            vm.Update();

            tbJMBG.IsEnabled = false;

            patient = null;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void lvPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            patient = (Patient)lvPatients.SelectedItem;
            tbJMBG.Text = patient.JMBG;
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
            if (Validate())
            {
                TimeSpan duration = new TimeSpan(0, int.Parse(tbDuration.Text), 0);
                List<Doctor> specialists = hospital.DoctorService.GetBySpecialization(cbSpecialization.SelectedValue.ToString());

                Appointment? appointment = Schedule.GetUrgent(duration, specialists);
                if (appointment is not null)
                {
                    appointment = FillAppointmentDetails(appointment);
                    Schedule.CreateUrgentAppointment(appointment);
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
                new PostponableAppointmentsView(appointment, postponable,hospital).ShowDialog();
            }
            else
            {
                ShowErrorMessageBox("Izaberite pacijenta i unesite duzinu trajanja u minutima.");
            }

        }
        public Appointment FillAppointmentDetails(Appointment? appointment)
        {
            if(appointment == null)
                appointment = new Appointment();

            appointment.Patient = patient;
            appointment.AppointmentID = Schedule.NextId();
            appointment.IsOperation = (cbOperation.IsChecked is bool Checked && Checked);
            return appointment;
        }

        private bool Validate()
        {
            return (tbDuration.Text != "" && tbJMBG.Text != null && patient is not null) ;
        }

        public void ShowErrorMessageBox(string messageBoxText)
        {
            Utility.ShowWarning(messageBoxText);
        }
    }
}
