using HealthCare.Command;
using HealthCare.Model;
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
        private Hospital _hospital;

        public ObservableCollection<AppointmentViewModel> Appointments;

        public IEnumerable<AppointmentViewModel> Appointmentss => Appointments;


        public ICommand CreateAppointmentViewCommand { get; }

        public ICommand EditAppointmentCommand { get; }

        public ICommand DeleteAppointmentCommand { get; }

        public ICommand ShowDetailedPatientInfoCommand { get; }

        public DoctorMainViewModel(Hospital hospital)
        {
            _hospital = hospital;
            Update();
            Appointments = new ObservableCollection<AppointmentViewModel>();

            CreateAppointmentViewCommand = new MakeAppointmentNavigationCommand(this);
            EditAppointmentCommand = new EditAppointmentDoctorCommand();
            // DeleteAppointmentCommand = new ...
            // ShowDetailedPatientInfoCommand = new 
            
        
        }
        public void Update()
        {
            // ovde Mozemo Dobaviti podatke i prebaciti ih u viewmodel
        }
    }
}
