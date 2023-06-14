using HealthCare.GUI.Command;

namespace HealthCare.GUI.DoctorGUI.Healthcare.Examination.Command;

public class ResetEquipmentQuantityCommand : CommandBase
{
    private readonly UsedDynamicEquipmentViewModel _viewModel;

    public ResetEquipmentQuantityCommand(UsedDynamicEquipmentViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public override void Execute(object parameter)
    {
        _viewModel.Update();
    }
}