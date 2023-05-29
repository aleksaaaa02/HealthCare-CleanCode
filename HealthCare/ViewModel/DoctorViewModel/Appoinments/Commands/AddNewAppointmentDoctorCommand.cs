using HealthCare.Command;
using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.Windows;
using HealthCare.Service.ScheduleService;

namespace HealthCare.ViewModel.DoctorViewModel.Appointments.Commands
{
    public class AddNewAppointmentDoctorCommand : CommandBase
    {
        private readonly MakeAppointmentViewModel _makeAppointmentViewModel;
        private readonly DoctorMainViewModel _doctorMainViewModel;
        private readonly Window _window;
        private readonly Schedule _schedule;
        private readonly PatientService _patientService;
        private readonly AppointmentService _appointmentService;
        private readonly bool _isEditing;

        public AddNewAppointmentDoctorCommand(MakeAppointmentViewModel viewModel, DoctorMainViewModel docMainViewModel, Window window, bool isEditing)
        {
            _makeAppointmentViewModel = viewModel;
            _doctorMainViewModel = docMainViewModel;
            _window = window;
            _isEditing = isEditing;
            _appointmentService = Injector.GetService<AppointmentService>();
            _patientService = Injector.GetService<PatientService>();
            _schedule = Injector.GetService<Schedule>();
        }

        public override void Execute(object parameter)
        {
            if (_makeAppointmentViewModel.SelectedPatient is null)
            {
                ViewUtil.ShowWarning("Morate odabrati pacijenta!");
                return;
            }

            Patient? patient = _patientService.TryGet(_makeAppointmentViewModel.SelectedPatient.JMBG);
            if (patient is null)
            {
                ViewUtil.ShowError("Oops... Doslo je do greske probajte ponovo!");
                return;
            }

            Appointment newAppointment = MakeAppointment(patient);

            if (_isEditing)
            {
                EditAppointment(newAppointment);
            }
            else
            {
                if (!_schedule.CheckAvailability(newAppointment.DoctorJMBG, newAppointment.PatientJMBG, newAppointment.TimeSlot))
                {
                    ViewUtil.ShowWarning("Doktor ili pacijent je zauzet u ovom terminu, odaberite drugi termin");
                    return;
                }
                else
                {
                    _appointmentService.Add(newAppointment);
                    _doctorMainViewModel.Update();
                    _window.Close();
                }
            }

        }

        private void EditAppointment(Appointment newAppointment)
        {
            newAppointment.AppointmentID = Convert.ToInt32(_doctorMainViewModel.SelectedAppointment.AppointmentID);
            if (!_schedule.CheckAvailability(newAppointment.DoctorJMBG, newAppointment.PatientJMBG, newAppointment.TimeSlot))
            {
                ViewUtil.ShowWarning("Doktor ili pacijent je zauzet u ovom terminu, odaberite drugi termin");
                return;
            }
            _appointmentService.Update(newAppointment);
            _doctorMainViewModel.Update();
            _window.Close();
        }

        private Appointment MakeAppointment(Patient patient)
        {
            DateTime start = _makeAppointmentViewModel.StartDate.Date + new TimeSpan(_makeAppointmentViewModel.Hours, _makeAppointmentViewModel.Minutes, 0);
            TimeSpan duration = TimeSpan.FromMinutes(_makeAppointmentViewModel.IsOperation ? _makeAppointmentViewModel.Duration : 15);
            TimeSlot timeSlot = new TimeSlot(start, duration);
            Appointment newAppointment = new Appointment(patient.JMBG, Context.Current.JMBG, timeSlot, _makeAppointmentViewModel.IsOperation);
            return newAppointment;
        }
    }
}
