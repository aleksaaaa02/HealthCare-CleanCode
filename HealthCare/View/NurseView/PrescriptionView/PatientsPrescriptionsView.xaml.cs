﻿using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModel.NurseViewModel;
using HealthCare.ViewModel.NurseViewModel.DataViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HealthCare.View.NurseView.PrescriptionView
{
    public partial class PatientsPrescriptionsView : Window
    {
        private PrescriptionListingViewModel _model;
        private PrescriptionViewModel? _prescription;
        private Patient _patient;
        private Hospital _hospital;
        public PatientsPrescriptionsView(Patient patient, Hospital hospital)
        {
            InitializeComponent();

            _patient = patient;
            _hospital = hospital;

            _model = new PrescriptionListingViewModel(patient,hospital);
            DataContext = _model;
            _model.Update();
            tbDate.SelectedDate = DateTime.Now;
        }
        private void lvPrescriptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _prescription = (PrescriptionViewModel) lvPrescriptions.SelectedItem;
            if (_prescription is null) {
                btnUse.IsEnabled = true;
                btnProlonge.IsEnabled = true;
                return;
            }

            if (_prescription.Prescription.FirstUse)
            {
                btnUse.IsEnabled = true;
                btnProlonge.IsEnabled = false;
            }
            else {
                btnUse.IsEnabled = false;
                btnProlonge.IsEnabled = true;
            }

        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnUse_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
                return;

            Prescription prescription = _hospital.PrescriptionService.Get(_prescription.Prescription.Id);

            if (!prescription.FirstUse) {
                Utility.ShowWarning("Vec ste iskoristili recept.");
                return;
            }

            if (!GiveMedication(prescription))
                return;

            prescription.FirstUse = false;
            prescription.Start = DateTime.Now.Date;
            _hospital.PrescriptionService.Update(prescription);

            _model.Update();
        }

        private void btnProlonge_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
                return;
            Prescription prescription = _hospital.PrescriptionService.Get(_prescription.Prescription.Id);

            if (prescription.FirstUse) {
                Utility.ShowWarning("Niste iskoristili recept.");
                return;
            }

            if (prescription.Start.AddDays(prescription.ConsumptionDays - 1) > DateTime.Now) {
                Utility.ShowWarning("Nije vam isteklo vreme.");
                return;
            }

            if (!GiveMedication(prescription))
                return;

            prescription.Start = DateTime.Now.Date;
            _hospital.PrescriptionService.Update(prescription);

            _model.Update();
        }

        private void btnMakeAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
                return;

            Doctor doctor = _hospital.DoctorService.Get(_prescription.Prescription.DoctorJMBG);

            if (!int.TryParse(tbHours.Text, out _) && !int.TryParse(tbMinutes.Text, out _))
            {
                Utility.ShowWarning("Sati i minuti moraju biti brojevi");
                return;
            }

            int hours = int.Parse(tbHours.Text);
            int minutes = int.Parse(tbMinutes.Text);

            if (!tbDate.SelectedDate.HasValue)
            {
                Utility.ShowWarning("Izaberite datum.");
                return;
            }

            DateTime selectedDate = tbDate.SelectedDate.Value;
            selectedDate = selectedDate.AddHours(hours);
            selectedDate = selectedDate.AddMinutes(minutes);

            TimeSlot slot = new TimeSlot(selectedDate, new TimeSpan(0, 15, 0));
            Appointment appointment = new Appointment(_patient, doctor, slot, false);

            if (!Schedule.CreateAppointment(appointment))
            {
                Utility.ShowWarning("Doktor ili pacijent je zauzet u unetom terminu.");
                return;
            }

            Utility.ShowInformation("Uspesno ste zakazali pregled.");
            _model.Update();
        }

        private bool GiveMedication(Prescription prescription) {
            var reduceItem = new InventoryItem(
            prescription.MedicationId, _hospital.RoomService.GetWarehouseId(), prescription.GetQuantity());

            if (!_hospital.MedicationInventory.TryReduceInventoryItem(reduceItem))
            {
                Utility.ShowError("Nema lekova na stanju.");
                return false;
            }

            Utility.ShowInformation("Uspesno izdati lekovi.");
            return true;
        }

        private bool Validate()
        {
            if (_prescription is null)
            {
                Utility.ShowWarning("Izaberite recept");
                return false;
            }
            return true;
        }

    }
}
