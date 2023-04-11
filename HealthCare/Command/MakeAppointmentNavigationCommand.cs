using HealthCare.Context;
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
        private readonly Hospital _hospital;
        public MakeAppointmentNavigationCommand(Hospital hospital,DoctorMainViewModel viewModel) 
        {
            _viewModel = viewModel;
            _hospital = hospital;
        }

        public override void Execute(object parameter)
        {
            
            MakeAppointmentView makeAppointmentView = new MakeAppointmentView(_hospital, _viewModel);
            makeAppointmentView.ShowDialog();

        }
    }
}
