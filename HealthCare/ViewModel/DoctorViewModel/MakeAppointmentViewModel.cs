using HealthCare.Command;
using HealthCare.Model;
using HealthCare.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HealthCare.ViewModels.DoctorViewModel
{
    public class MakeAppointmentViewModel : ViewModelBase
    {
        private DateTime _startDate;
        public DateTime StartDate { 
            get { return _startDate; }
            set {
                _startDate = value; 
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private int _hours;
        public int Hours
        {
            get { return _hours; }
            set
            {
                _hours = value;
                OnPropertyChanged(nameof(Hours));
            }
        }
        private int _minutes;
        public int Minutes
        {
            get { return _minutes; }
            set
            {
                _minutes = value;
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
        private int _duration;
        public int Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }
        public ICommand CancelCommand { get; }
        public ICommand SubmitCommand { get; }
        public MakeAppointmentViewModel(DoctorMainViewModel DoctorViewModel, Window window)
        {
            // For New Appointment
            CancelCommand = new CancelNewAppointmentDoctorCommand(window);
            SubmitCommand = new AddNewAppointmentDoctorCommand(this,  DoctorViewModel, window);
        }

        public MakeAppointmentViewModel(Appointment appointment, DoctorMainViewModel DoctorViewModel, Window window)
        {
            // For Editing Appointment
            _startDate = appointment.TimeSlot.Start;
            _hours = appointment.TimeSlot.Start.Hour;
            _minutes = appointment.TimeSlot.Start.Minute;
            _isOperation = appointment.IsOperation;
            _duration = appointment.TimeSlot.Duration.Minutes;

            CancelCommand = new CancelNewAppointmentDoctorCommand(window);
            SubmitCommand = new AddNewAppointmentDoctorCommand(this, DoctorViewModel, window);
 
        }

    }
}
