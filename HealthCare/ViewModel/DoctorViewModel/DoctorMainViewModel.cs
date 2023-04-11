using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthCare.ViewModels.DoctorViewModel
{
    public class DoctorMainViewModel : BaseViewModel
    {
        private DateTime _startDate = DateTime.Now;
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        private int _days = 3;
        public int Days
        {
            get { return _days; }
            set
            {
                _days = value;
                OnPropertyChanged(nameof(Days));
            }
        }


        private AppointmentViewModel _selectedPatient;
        public AppointmentViewModel SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));
            }
        }

        private Hospital _hospital;
        
        public ObservableCollection<AppointmentViewModel> Appointments;

        public IEnumerable<AppointmentViewModel> Appointmentss => Appointments;

        public ICommand CreateAppointmentViewCommand { get; }

        public ICommand EditAppointmentCommand { get; }

        public ICommand DeleteAppointmentCommand { get; }

        public ICommand ShowDetailedPatientInfoCommand { get; }

        public ICommand ApplyFilterCommand { get; }

        public DoctorMainViewModel(Hospital hospital)
        {
            _hospital = hospital;
            Appointments = new ObservableCollection<AppointmentViewModel>();
            Update();
            CreateAppointmentViewCommand = new MakeAppointmentNavigationCommand(this);
            EditAppointmentCommand = new EditAppointmentDoctorCommand();
            // DeleteAppointmentCommand = new ...
            ShowDetailedPatientInfoCommand = new ShowPatientInfoCommand(_hospital, this);

            ApplyFilterCommand = new ApplyFilterCommand(this, _hospital);
        
        }

        public void ApplyFilterOn(List<Appointment> appointments)
        {
            Appointments.Clear();
            foreach (var appointment in appointments)
            {
                Appointments.Add(new AppointmentViewModel(appointment));
            }
        }
        public void Update()
        {
            Appointments.Clear();
            foreach (var appointment in Schedule.GetDoctorAppointments((Doctor)_hospital.Current))
            {
                  Appointments.Add(new AppointmentViewModel(appointment));
            }
        }
    }
}
