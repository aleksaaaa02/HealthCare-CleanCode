using HealthCare.Model;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthCare.Command
{
    public class AddNewAppointmentDoctorCommand : CommandBase
    {
        private readonly MakeAppointmentViewModel _makeAppointmentViewModel;
        private readonly DoctorMainViewModel _doctorMainViewModel;
        private readonly Window _window;
        // Treba dodati Hospital Zbog Service


        public AddNewAppointmentDoctorCommand(MakeAppointmentViewModel viewModel, DoctorMainViewModel docMainViewModel, Window window)
        {
            _makeAppointmentViewModel = viewModel;
            _doctorMainViewModel = docMainViewModel;
            _window = window;
        }


        public override void Execute(object parameter)
        {
            List<Appointment> appointments = new List<Appointment>();
            Doctor doc = new Doctor("Aleksa", "Vukomanovic", "123456789", DateTime.Now, "062173224", "Vuka Karadzica", "aleksa123", "radi", Gender.Male, "Hirurg");
            string[] bowesti = { "dijabetes", "sizofrenija" };

            MedicalRecord record = new MedicalRecord(185, 80, bowesti);
            Patient patient = new Patient("Dimitrije", "Gasic", "234567891", DateTime.Now, "06213214", "Trg Dositeja Obradovica 6", "gasara123", "123123", Gender.Male, false, record);
            Appointment appointment = new Appointment(patient, doc, new TimeSlot(DateTime.Now, TimeSpan.FromMinutes(15)), false);

            // Schedule.AddAppointment(appointment);
            // View
            _doctorMainViewModel.Appointments.Add(new AppointmentViewModel(appointment));
            _doctorMainViewModel.Update();
            _window.Close();
        }
    }
}
