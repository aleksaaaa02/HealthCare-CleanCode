﻿using System;
using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Exceptions;
using HealthCare.Service;
using HealthCare.View;

namespace HealthCare.ViewModel.DoctorViewModel.Treatment
{
    public class IncreaseTreatmentDurationCommand : CommandBase
    {
        private readonly Model.Treatment _treatment;
        private readonly DoctorTreatmentVisitViewModel _viewModel;
        public IncreaseTreatmentDurationCommand(DoctorTreatmentVisitViewModel viewModel ,Model.Treatment treatment)
        {
            _treatment = treatment;
            _viewModel = viewModel;
        }
        public override void Execute(object parameter)
        {
            try
            {
                Validate();            
                IncreaseTreatmentDuration();
                _viewModel.LoadInformation(_treatment);
            } catch (ValidationException va)
            {
                ViewUtil.ShowWarning(va.Message);
            }

        }
        private void Validate()
        {
            if (_viewModel.DurationIncreaseDays <= 0)
            {
                throw new ValidationException("Broj dana mora biti veci od nula!");
            }

        }

        private void IncreaseTreatmentDuration()
        {
            int days = _viewModel.DurationIncreaseDays;
            _treatment.TreatmentDuration.Duration = _treatment.TreatmentDuration.Duration.Add(TimeSpan.FromDays(days));
            Injector.GetService<TreatmentService>().Update(_treatment);
          
        }

    }
}
