﻿
using System;
using System.Windows;
using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.Service.ScheduleService;
using HealthCare.View;

namespace HealthCare.ViewModel.DoctorViewModel.Treatment
{
    public class MakeAppointmentFromReleaseCommand : CommandBase
    {
        private readonly Window _window;
        private readonly Model.Treatment _treatment;
        private readonly PatientReleaseAppointmentViewModel _viewModel;
        private readonly Schedule _schedule;
        private readonly TreatmentReferralService _treatmentReferralService;
        private Appointment _appointment;

        public MakeAppointmentFromReleaseCommand(PatientReleaseAppointmentViewModel viewModel ,Window window, Model.Treatment treatment)
        {
            _treatmentReferralService = Injector.GetService<TreatmentReferralService>();
            _schedule = Injector.GetService<Schedule>();
            _window = window;
            _viewModel = viewModel;
            _treatment = treatment;
        }
        public override void Execute(object parameter)
        {
            try
            {
                Validate();
                MakeAppointment();
                _window.Close();
            }
            catch (ValidationException ve)
            {
                ViewUtil.ShowWarning(ve.Message);
            }

        }

        private void Validate()
        {

            DateTime date = _viewModel.Date;

            if (date < DateTime.Now)
            {
                throw new ValidationException("Datum mora biti u buducnosti");
            }

            TimeSpan timeSpan = new TimeSpan(_viewModel.Hours, _viewModel.Minutes, 0);
            TreatmentReferral referral = _treatmentReferralService.Get(_treatment.ReferralId);
            Appointment appointment = new Appointment(referral.PatientJMBG, referral.DoctorJMBG, new TimeSlot(date, timeSpan), false);
            if (!_schedule.IsAvailable(appointment))
            {
                throw new ValidationException("Nazalost uneti termin nije slobodan");
            }

            _appointment = appointment;

        }

        private void MakeAppointment()
        {
            _schedule.Add(_appointment);
        }
    }
}
