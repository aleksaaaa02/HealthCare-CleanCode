using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel;
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
    public class DoctorMainViewModel : ViewModelBase
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
        
        private ObservableCollection<AppointmentViewModel> _appointments;

        public IEnumerable<AppointmentViewModel> Appointments => _appointments;

        public ICommand CreateAppointmentViewCommand { get; }

        public ICommand EditAppointmentCommand { get; }

        public ICommand DeleteAppointmentCommand { get; }

        public ICommand ShowDetailedPatientInfoCommand { get; }

        public ICommand ApplyFilterCommand { get; }

        public ICommand ShowPatientSearchCommand { get; }
        public ICommand StartExaminationCommand { get; }
        public DoctorMainViewModel(Hospital hospital)
        {
            _hospital = hospital;
            _appointments = new ObservableCollection<AppointmentViewModel>();
            Update();

            CreateAppointmentViewCommand = new MakeAppointmentNavigationCommand(hospital, this);
            EditAppointmentCommand = new EditAppointmentDoctorCommand(hospital, this);
            DeleteAppointmentCommand = new DeleteAppointmentCommand(hospital, this);
            ShowDetailedPatientInfoCommand = new ShowPatientInfoCommand(hospital, this, false);
            ApplyFilterCommand = new ApplyFilterCommand(hospital, this);
            ShowPatientSearchCommand = new ShowPatientSearchViewCommand(hospital);
            StartExaminationCommand = new ShowReservationDialogCommand(hospital, this);
        }

        public void ApplyFilterOn(List<Appointment> appointments)
        {
            _appointments.Clear();
            foreach (var appointment in appointments)
            {
                _appointments.Add(new AppointmentViewModel(appointment));
            }
        }
        public void Update()
        {
            _appointments.Clear();
            foreach (var appointment in Schedule.GetDoctorAppointments((Doctor)_hospital.Current))
            {
                  _appointments.Add(new AppointmentViewModel(appointment));
            }
        }
    }
}
