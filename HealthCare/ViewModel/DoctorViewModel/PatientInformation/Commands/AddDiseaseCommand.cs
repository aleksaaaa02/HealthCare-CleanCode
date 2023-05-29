﻿using HealthCare.Command;
using HealthCare.Exceptions;
using HealthCare.View;

namespace HealthCare.ViewModel.DoctorViewModel.PatientInformation.Commands
{
    public class AddDiseaseCommand : CommandBase
    {
        private readonly PatientInformationViewModel _viewModel;

        public AddDiseaseCommand(PatientInformationViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            try
            {
                Validate();
                _viewModel.AddPreviousDisease(_viewModel.Disease);
            }
            catch (ValidationException ve)
            {
                ViewUtil.ShowWarning(ve.Message);
            }
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(_viewModel.Disease))
            {
                throw new ValidationException("Morate uneti bolest u polje");
            }
        }
    }
}
