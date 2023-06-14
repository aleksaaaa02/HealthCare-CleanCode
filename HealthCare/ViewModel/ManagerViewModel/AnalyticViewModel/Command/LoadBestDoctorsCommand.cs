using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Service;
using HealthCare.View.ManagerView.AnalyticsView;
using HealthCare.ViewModel.ManagerViewModel.DataViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModel.ManagerViewModel.AnalyticViewModel.Command
{
    public class LoadBestDoctorsCommand : CommandBase
    {
        private readonly DoctorAnalyticsViewModel _model;

        public LoadBestDoctorsCommand(DoctorAnalyticsViewModel model)
        {
            _model = model;
        }

        public override void Execute(object parameter)
        {
            _model.LoadTop3();
        }
    }
}
