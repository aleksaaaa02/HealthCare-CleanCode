﻿using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
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
        private readonly Hospital _hospital;
        private readonly bool _isEditing;
        // Treba dodati _hospital Zbog Service


        public AddNewAppointmentDoctorCommand(Hospital hospital,MakeAppointmentViewModel viewModel, DoctorMainViewModel docMainViewModel, Window window, bool isEditing)
        {
            _makeAppointmentViewModel = viewModel;
            _doctorMainViewModel = docMainViewModel;
            _window = window;
            _hospital = hospital;
            _isEditing = isEditing;
        }


        public override void Execute(object parameter)
        {
            if(_makeAppointmentViewModel.SelectedPatient is null)
            {
                Utility.ShowWarning("Morate odabrati pacijenta!");
                return;
            }
            Patient patient = _hospital.PatientService.GetAccount(_makeAppointmentViewModel.SelectedPatient.JMBG);
            if(patient is null)
            {
                Utility.ShowWarning("Oops... Doslo je do greske probajte ponovo!");
                return;
                
            }
            DateTime start = _makeAppointmentViewModel.StartDate.Date;
            int hours = _makeAppointmentViewModel.Hours;
            int minutes = _makeAppointmentViewModel.Minutes;
            start = start + new TimeSpan(hours, minutes, 0);
            TimeSpan duration = new TimeSpan(0, 15, 0);
            bool isOperation = _makeAppointmentViewModel.IsOperation;
            if (isOperation)
            {
                int durationMinutes = _makeAppointmentViewModel.Duration;
                duration = new TimeSpan(0, durationMinutes, 0);
            }
            TimeSlot timeSlot = new TimeSlot(start, duration);
            Appointment newAppointment = new Appointment(patient, (Doctor)_hospital.Current, timeSlot , isOperation );
            if (!_isEditing)
            {
                if (Schedule.CreateAppointment(newAppointment))
                {
                    _doctorMainViewModel.Update();
                    _window.Close();
                }
                else
                {
                    Utility.ShowWarning("Doktor ili pacijent je zauzet u ovom terminu, odaberite drugi termin");
                }
            }
            else
            {
                newAppointment.AppointmentID = Convert.ToInt32(_doctorMainViewModel.SelectedPatient.AppointmentID);
                Schedule.UpdateAppointment(newAppointment);
                _doctorMainViewModel.Update();
                _window.Close();
            }

        }
    }
}
