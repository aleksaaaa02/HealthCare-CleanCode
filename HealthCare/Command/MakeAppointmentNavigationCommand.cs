using HealthCare.Model;
using HealthCare.View.DoctorView;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthCare.Command
{
    public class MakeAppointmentNavigationCommand : CommandBase   {

        private DoctorMainViewModel _viewModel;
        public MakeAppointmentNavigationCommand(DoctorMainViewModel viewModel) 
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            
            MakeAppointmentView makeAppointmentView = new MakeAppointmentView(_viewModel);
            makeAppointmentView.ShowDialog();

        }
    }
}
