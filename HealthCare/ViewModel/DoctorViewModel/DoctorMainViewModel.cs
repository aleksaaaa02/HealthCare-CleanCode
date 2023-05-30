﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModel;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using HealthCare.ViewModel.DoctorViewModel.MainViewModelCommands;
using HealthCare.ViewModel.DoctorViewModel.PatientInformation.Commands;

namespace HealthCare.ViewModels.DoctorViewModel;

public class DoctorMainViewModel : ViewModelBase
{
    private readonly ObservableCollection<AppointmentViewModel> _appointments;
    private readonly AppointmentService _appointmentService;
    private int _numberOfDays = 3;
    private AppointmentViewModel _selectedAppointment;
    private DateTime _startDate = DateTime.Now;

    public DoctorMainViewModel(Window window)
    {
        _appointments = new ObservableCollection<AppointmentViewModel>();

        _appointmentService = Injector.GetService<AppointmentService>();

        ResetFilterCommand = new ResetFilterCommand(this);
        LogOutCommand = new LogOutCommand(window);
        CreateAppointmentViewCommand = new MakeAppointmentNavigationCommand(this);
        EditAppointmentCommand = new EditAppointmentDoctorCommand(this);
        DeleteAppointmentCommand = new DeleteAppointmentCommand(this);
        ShowDetailedPatientInfoCommand = new ShowPatientInfoCommand(this, false);
        ApplyFilterCommand = new ApplyFilterCommand(this);
        ShowPatientSearchCommand = new ShowPatientSearchViewCommand();
        StartExaminationCommand = new StartExaminationCommand(this);
        Update();
    }

    public IEnumerable<AppointmentViewModel> Appointments => _appointments;
    public ICommand CreateAppointmentViewCommand { get; }
    public ICommand EditAppointmentCommand { get; }
    public ICommand DeleteAppointmentCommand { get; }
    public ICommand ShowDetailedPatientInfoCommand { get; }
    public ICommand ApplyFilterCommand { get; }
    public ICommand ShowPatientSearchCommand { get; }
    public ICommand StartExaminationCommand { get; }
    public ICommand ResetFilterCommand { get; }
    public ICommand LogOutCommand { get; }

    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            _startDate = value;
            OnPropertyChanged();
        }
    }

    public int NumberOfDays
    {
        get => _numberOfDays;
        set
        {
            _numberOfDays = value;
            OnPropertyChanged();
        }
    }

    public AppointmentViewModel SelectedAppointment
    {
        get => _selectedAppointment;
        set
        {
            _selectedAppointment = value;
            OnPropertyChanged();
        }
    }

    public void ApplyFilterOnAppointments(List<Appointment> appointments)
    {
        _appointments.Clear();
        foreach (var appointment in appointments) _appointments.Add(new AppointmentViewModel(appointment));
    }

    public void Update()
    {
        _appointments.Clear();
        foreach (var appointment in _appointmentService.GetByDoctor(Context.Current.JMBG))
            _appointments.Add(new AppointmentViewModel(appointment));
    }
}