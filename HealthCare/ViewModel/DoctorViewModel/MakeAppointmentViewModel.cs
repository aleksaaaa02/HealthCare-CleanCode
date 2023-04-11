using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HealthCare.ViewModels.DoctorViewModel
{
    public class MakeAppointmentViewModel : ViewModelBase
    {
        private readonly Hospital _hospital;
        private readonly Patient _selected;
        private ObservableCollection<PatientViewModel> _patients;
        public IEnumerable<PatientViewModel> Patients => _patients;

        private DateTime _startDate = DateTime.Today;
        public DateTime StartDate { 
            get { return _startDate; }
            set {
                if (value < DateTime.Today)
                {
                    _startDate = DateTime.Today;
                }
                else
                {
                    _startDate = value;
                }
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private int _hours = 0;
        public int Hours
        {
            get { return _hours; }
            set
            {
                if (value > 23 || value < 0)
                {
                    _hours = 0;
                }
                else
                {
                    _hours = value;
                }
                
                
                OnPropertyChanged(nameof(Hours));
            }
        }
        private int _minutes = 0;
        public int Minutes
        {
            get { return _minutes; }
            set
            {
                if (value > 59 || value < 0)
                {
                    _minutes = 0;
                }
                else
                {
                    _minutes = value;
                }
                
                
                OnPropertyChanged(nameof(Minutes));
            }
        }
        private bool _isOperation;
        public bool IsOperation
        {
            get { return _isOperation; }
            set
            {
                _isOperation = value;
                OnPropertyChanged(nameof(IsOperation));
            }
        }
        private int _duration = 15;
        public int Duration
        {
            get { return _duration; }
            set
            {
       
                if (value <= 15)
                {
                    _duration = 15;
                }
                else
                {
                    _duration = value;
                }
           
                OnPropertyChanged(nameof(Duration));
            }
        }
        private PatientViewModel _selectedPatient;
        public PatientViewModel SelectedPatient { 
            get { return _selectedPatient; }
            set 
            { 
                _selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));    
            }
        }
        public ICommand CancelCommand { get; }
        public ICommand SubmitCommand { get; }
        public MakeAppointmentViewModel(Hospital hospital, DoctorMainViewModel DoctorViewModel, Window window)
        {
            // For New Appointment
            _hospital = hospital;
            CancelCommand = new CancelNewAppointmentDoctorCommand(window);
            SubmitCommand = new AddNewAppointmentDoctorCommand(hospital ,this,  DoctorViewModel, window, false);
            _patients = new ObservableCollection<PatientViewModel>();
            Update();
        }

        public MakeAppointmentViewModel(Hospital hospital, Appointment appointment, DoctorMainViewModel DoctorViewModel, Window window)
        {
            // For Editing Appointment
            _hospital = hospital;
            _startDate = appointment.TimeSlot.Start;
            _hours = Convert.ToInt32(appointment.TimeSlot.Start.TimeOfDay.TotalHours);
            _minutes = appointment.TimeSlot.Start.Minute;
            _isOperation = appointment.IsOperation;
            _duration = Convert.ToInt32(appointment.TimeSlot.Duration.TotalMinutes);
            _patients = new ObservableCollection<PatientViewModel>();
            _selected = appointment.Patient;
            Update();
            
            CancelCommand = new CancelNewAppointmentDoctorCommand(window);
            SubmitCommand = new AddNewAppointmentDoctorCommand(_hospital, this, DoctorViewModel, window, true);
 
        }
        public void Update()
        {
            _patients.Clear();
            foreach(Patient patient in _hospital.PatientService.Patients)
            {
                if(_selected == patient) { SelectedPatient = new PatientViewModel(patient); }
                _patients.Add(new PatientViewModel(patient));
            }
        }
       
    }
}
