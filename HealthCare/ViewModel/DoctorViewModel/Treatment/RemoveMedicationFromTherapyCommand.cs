using System;
using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;

namespace HealthCare.ViewModel.DoctorViewModel.Treatment
{
    public class RemoveMedicationFromTherapyCommand : CommandBase
    {
        private readonly DoctorTreatmentVisitViewModel _viewModel;
        private readonly Therapy _therapy;
        public RemoveMedicationFromTherapyCommand(DoctorTreatmentVisitViewModel viewModel, Therapy therapy)
        {
            _viewModel = viewModel;
            _therapy = therapy;
        }

        public override void Execute(object parameter)
        {
            try
            {
                Validate();
                RemoveMedication();
                _viewModel.Update();

            }
            catch (ValidationException ve)
            {
                ViewUtil.ShowWarning(ve.Message);
            }
        }

        private void RemoveMedication()
        {
            int selectedMedication = _viewModel.SelectedPrescription.PrescriptionID;
            if (!_therapy.InitialMedication.Remove(selectedMedication))
                throw new ValidationException("Nesto je poslo po zlu");
         
            Injector.GetService<TherapyService>().Update(_therapy);
            Injector.GetService<PrescriptionService>(Injector.THERAPY_PRESCRIPTION_S).Remove(selectedMedication);
         

        }

        private void Validate()
        {
            if (_viewModel.SelectedPrescription is null)
            {
                throw new ValidationException("Morate odabrati lek iz terapije da biste uklonili");
            }

        }
    }
}
