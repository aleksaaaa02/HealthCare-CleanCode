using HealthCare.Command;
using HealthCare.Exceptions;
using HealthCare.View;

namespace HealthCare.GUI.DoctorGUI.PatientMedicalRecord.Command;

public class RemoveAllergyCommand : CommandBase
{
    private readonly PatientInformationViewModel _viewModel;

    public RemoveAllergyCommand(PatientInformationViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public override void Execute(object parameter)
    {
        try
        {
            Validate();
            var selectedAllergy = _viewModel.SelectedAllergy;
            _viewModel.RemoveAllergy(selectedAllergy);
        }
        catch (ValidationException ve)
        {
            ViewUtil.ShowWarning(ve.Message);
        }
    }

    private void Validate()
    {
        if (_viewModel.SelectedAllergy is null)
            throw new ValidationException("Morate odabrati alergiju koju zelite da uklonite.");
    }
}