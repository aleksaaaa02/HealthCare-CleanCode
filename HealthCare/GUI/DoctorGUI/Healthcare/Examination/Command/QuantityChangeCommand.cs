using HealthCare.Exceptions;
using HealthCare.GUI.Command;
using HealthCare.View;

namespace HealthCare.GUI.DoctorGUI.Healthcare.Examination.Command;

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