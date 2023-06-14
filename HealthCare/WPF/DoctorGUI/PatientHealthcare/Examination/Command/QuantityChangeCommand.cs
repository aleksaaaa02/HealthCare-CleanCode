using HealthCare.Application.Exceptions;
using HealthCare.WPF.Common;
using HealthCare.WPF.Common.Command;

namespace HealthCare.WPF.DoctorGUI.PatientHealthcare.Examination.Command;

public class QuantityChangeCommand : CommandBase
{
    private readonly EquipmentViewModel _equipmentViewModel;

    public QuantityChangeCommand(EquipmentViewModel equipmentViewModel)
    {
        _equipmentViewModel = equipmentViewModel;
    }

    public override void Execute(object parameter)
    {
        try
        {
            Validate();
            _equipmentViewModel.CurrentQuantity -= 1;
        }
        catch (ValidationException ve)
        {
            ViewUtil.ShowWarning(ve.Message);
        }
    }

    private void Validate()
    {
        if (_equipmentViewModel.CurrentQuantity <= 0) throw new ValidationException("Trenutne opreme nema na stanju!");
    }
}