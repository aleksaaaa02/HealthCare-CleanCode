﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using HealthCare.ViewModel.DoctorViewModel.Treatment;

namespace HealthCare.ViewModel.DoctorViewModel.Prescriptions;

public class PrescriptionViewModel : ViewModelBase
{
    private readonly ObservableCollection<MedicationViewModel> _medications;
    private readonly MedicationService _medicationService;

    public PrescriptionViewModel()
    {
        _medicationService = Injector.GetService<MedicationService>();
        BeforeMeal = true;
        _medications = new ObservableCollection<MedicationViewModel>();
    }
    public PrescriptionViewModel(Patient patient) : this()
    {
        MakePrescriptionCommand = new AddPrescriptionCommand(patient, this);

        Update();
    }

    public PrescriptionViewModel(Therapy therapy) : this()
    {
        MakePrescriptionCommand = new AddMedicationToTreatmentCommand(this, therapy);

        Update();
    }

    public MedicationViewModel SelectedMedication { get; set; }

    public IEnumerable<MedicationViewModel> Medications => _medications;

    public bool BeforeMeal { get; set; }
    public bool DuringMeal { get; set; }
    public bool AfterMeal { get; set; }
    public bool NoPreference { get; set; }
    public int DailyDosage { get; set; }
    public int HoursBetweenConsumption { get; set; }
    public int ConsumptionDays { get; set; }

    public ICommand MakePrescriptionCommand { get; }

    private void Update()
    {
        _medications.Clear();
        foreach (var medication in _medicationService.GetAll()) _medications.Add(new MedicationViewModel(medication));
    }
}