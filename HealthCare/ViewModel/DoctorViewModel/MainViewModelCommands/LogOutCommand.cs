using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthCare.Command;

namespace HealthCare.ViewModel.DoctorViewModel.MainViewModelCommands
{
    public class LogOutCommand : CommandBase
    {
        private readonly MainWindow _mainWindow;
        private readonly Window _window;
        public LogOutCommand(MainWindow mainWindow, Window window)
        {
            _mainWindow = mainWindow;
            _window = window;
        }

        public override void Execute(object parameter)
        {
            _window.Close();
        }
    }
}
