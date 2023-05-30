﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using HealthCare.ViewModel.DoctorViewModel.Referrals.Commands;

namespace HealthCare.ViewModel.DoctorViewModel.Referrals;

public class TreatmentReferralViewModel : ViewModelBase
{
    private readonly ObservableCollection<MedicationViewModel> _medications;
    private readonly MedicationService _medicationService;

    public readonly Patient ExaminedPatient;
    private string _additionalExamination;
    private int _daysOfTreatment;

    public TreatmentReferralViewModel(Patient patient)
    {
        _medicationService = Injector.GetService<MedicationService>();
        _medications = new ObservableCollection<MedicationViewModel>();
        ExaminedPatient = patient;
        _additionalExamination = " ";
        DaysOfTreatment = 0;

        MakeTreatmentReferralCommand = new AddTreatmentReferralCommand(this);

        Update();
    }

    public IEnumerable<MedicationViewModel> Medications => _medications;

    public ICommand MakeTreatmentReferralCommand { get; }

    public int DaysOfTreatment
    {
        get => _daysOfTreatment;
        set
        {
            _daysOfTreatment = value;
            OnPropertyChanged();
        }
    }

    public string AdditionalExamination
    {
        get => _additionalExamination;
        set
        {
            _additionalExamination = value;
            OnPropertyChanged();
        }
    }

    public void Update()
    {
        _medications.Clear();
        foreach (var medication in _medicationService.GetAll()) _medications.Add(new MedicationViewModel(medication));
    }
}