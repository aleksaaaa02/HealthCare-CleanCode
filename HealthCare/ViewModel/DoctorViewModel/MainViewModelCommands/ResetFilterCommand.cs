using HealthCare.Command;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModel.DoctorViewModel.MainViewModelCommands
{
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
}
