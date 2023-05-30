using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModel.DoctorViewModel.Appointments.Commands;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using HealthCare.ViewModels.DoctorViewModel;

namespace HealthCare.ViewModel.DoctorViewModel.Appointments;

public class MakeAppointmentViewModel : ViewModelBase
{
    private readonly ObservableCollection<PatientViewModel> _patients;
    private readonly PatientService _patientService;
    private readonly Patient _selected;
    private int _duration = 15;
    private int _hours;
    private bool _isOperation;
    private int _minutes;
    private PatientViewModel _selectedPatient;

    private DateTime _startDate = DateTime.Today;

    public MakeAppointmentViewModel(DoctorMainViewModel doctorViewModel, Window window, bool edit = false)
    {
        _patientService = Injector.GetService<PatientService>();

        CancelCommand = new CancelCommand(window);
        SubmitCommand = new AddNewAppointmentDoctorCommand(this, doctorViewModel, window, edit);
        _patients = new ObservableCollection<PatientViewModel>();
        Update();
    }

    public MakeAppointmentViewModel(Appointment appointment, DoctorMainViewModel doctorViewModel, Window window) : this(
        doctorViewModel, window, true)
    {
        _startDate = appointment.TimeSlot.Start;
        _hours = Convert.ToInt32(appointment.TimeSlot.Start.TimeOfDay.TotalHours);
        _minutes = appointment.TimeSlot.Start.Minute;
        _isOperation = appointment.IsOperation;
        _duration = Convert.ToInt32(appointment.TimeSlot.Duration.TotalMinutes);
        _selected = _patientService.Get(appointment.PatientJMBG);
    }

    public IEnumerable<PatientViewModel> Patients => _patients;

    public ICommand CancelCommand { get; }
    public ICommand SubmitCommand { get; }

    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            if (value < DateTime.Today)
                _startDate = DateTime.Today;
            else
                _startDate = value;
            OnPropertyChanged();
        }
    }

    public int Hours
    {
        get => _hours;
        set
        {
            if (value > 23 || value < 0)
                _hours = 0;
            else
                _hours = value;
            OnPropertyChanged();
        }
    }

    public int Minutes
    {
        get => _minutes;
        set
        {
            if (value > 59 || value < 0)
                _minutes = 0;
            else
                _minutes = value;
            OnPropertyChanged();
        }
    }

    public bool IsOperation
    {
        get => _isOperation;
        set
        {
            _isOperation = value;
            OnPropertyChanged();
        }
    }

    public int Duration
    {
        get => _duration;
        set
        {
            if (value <= 15)
                _duration = 15;
            else
                _duration = value;
            OnPropertyChanged();
        }
    }

    public PatientViewModel SelectedPatient
    {
        get => _selectedPatient;
        set
        {
            _selectedPatient = value;
            OnPropertyChanged();
        }
    }

    public void Update()
    {
        _patients.Clear();
        foreach (var patient in _patientService.GetAll())
        {
            if (_selected == patient) SelectedPatient = new PatientViewModel(patient);
            _patients.Add(new PatientViewModel(patient));
        }
    }
}