using System.Windows;
using HealthCare.Command;

namespace HealthCare.GUI.DoctorGUI;

public class LogOutCommand : CommandBase
{
    private readonly Window _window;

    public LogOutCommand(Window window)
    {
        _window = window;
    }

    public override void Execute(object parameter)
    {
        _window.Close();
    }
}