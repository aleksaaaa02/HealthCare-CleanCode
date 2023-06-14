using HealthCare.Exceptions;
using HealthCare.GUI.Command;
using HealthCare.View;

namespace HealthCare.GUI.DoctorGUI.PatientMedicalRecord.Command;

public class AddAllergyCommand : CommandBase
{
    private readonly PatientInformationViewModel _viewModel;

    public AddAllergyCommand(PatientInformationViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public override void Execute(object parameter)
    {
        try
        {
            Validate();
            _viewModel.AddAllergy(_viewModel.Allergy);
        }
        catch (ValidationException ve)
        {
            ViewUtil.ShowWarning(ve.Message);
        }
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(_viewModel.Allergy))
            throw new ValidationException("Morate uneti alergiju u polje");
    }
}