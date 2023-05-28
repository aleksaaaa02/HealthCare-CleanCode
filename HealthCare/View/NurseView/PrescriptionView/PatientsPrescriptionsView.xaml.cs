﻿using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.Service.ScheduleTest;
using HealthCare.ViewModel.NurseViewModel;
using HealthCare.ViewModel.NurseViewModel.DataViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HealthCare.View.NurseView.PrescriptionView
{
    public partial class PatientsPrescriptionsView : Window
    {
        private readonly PrescriptionService _prescriptionService;
        private readonly InventoryService _medicationInventory;
        private readonly DoctorService _doctorService;
        private readonly RoomService _roomService;
        private readonly TestSchedule _schedule;
        private readonly AppointmentService _appointmentService;
        private PrescriptionListingViewModel _model;
        private PrescriptionViewModel? _prescription;
        private Patient _patient;
        public PatientsPrescriptionsView(Patient patient)
        {
            InitializeComponent();

            _model = new PrescriptionListingViewModel(patient);
            DataContext = _model;
            _model.Update();

            _prescriptionService = Injector.GetService<PrescriptionService>(Injector.REGULAR_PRESCRIPTION_S);
            _medicationInventory = Injector.GetService<InventoryService>(Injector.MEDICATION_INVENTORY_S);
            _doctorService = Injector.GetService<DoctorService>();
            _roomService = Injector.GetService<RoomService>();
            _appointmentService = Injector.GetService<AppointmentService>();

            _schedule = new TestSchedule();

            _patient = patient;
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

            Prescription prescription = _prescriptionService.Get(_prescription.Prescription.Id);

            if (!prescription.FirstUse) {
                Utility.ShowWarning("Vec ste iskoristili recept.");
                return;
            }

            if (!GiveMedication(prescription))
                return;

            prescription.FirstUse = false;
            prescription.Start = DateTime.Now.Date;
            _prescriptionService.Update(prescription);

            _model.Update();
        }

        private void btnProlonge_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
                return;
            Prescription prescription = _prescriptionService.Get(_prescription.Prescription.Id);

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
            _prescriptionService.Update(prescription);

            _model.Update();
        }

        private void btnMakeAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
                return;

            Doctor doctor = _doctorService.Get(_prescription.Prescription.DoctorJMBG);

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
            Appointment appointment = new Appointment(_patient.JMBG, doctor.JMBG, slot, false);

            if (!_schedule.CheckAvailability(doctor.JMBG, _patient.JMBG, slot))
            {
                Utility.ShowWarning("Doktor ili pacijent je zauzet u unetom terminu.");
                return;
            }
            _appointmentService.Add(appointment);
            Utility.ShowInformation("Uspesno ste zakazali pregled.");
            _model.Update();
        }

        private bool GiveMedication(Prescription prescription) {
            var reduceItem = new InventoryItem(
            prescription.MedicationId, _roomService.GetWarehouseId(), prescription.GetQuantity());

            if (!_medicationInventory.TryReduceInventoryItem(reduceItem))
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
