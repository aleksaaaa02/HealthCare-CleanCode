using HealthCare.Command;

namespace HealthCare.GUI.DoctorGUI.AppointmentSchedule.Command;

public class ResetFilterCommand : CommandBase
{
    private readonly DoctorMainViewModel _viewModel;

    public ResetFilterCommand(DoctorMainViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public override void Execute(object parameter)
    {
        _viewModel.Update();
    }
}