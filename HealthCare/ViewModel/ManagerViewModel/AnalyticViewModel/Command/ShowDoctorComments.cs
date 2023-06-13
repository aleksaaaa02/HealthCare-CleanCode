using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Service;
using HealthCare.View.ManagerView.AnalyticsView;
using HealthCare.ViewModel.ManagerViewModel.AnalyticViewModel;
using HealthCare.ViewModel.ManagerViewModel.DataViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthCare.ViewModel.ManagerViewModel.AnalyticViewModel.Command
{
    public class ShowDoctorComments : CommandBase
    {
        private readonly DoctorAnalyticsViewModel _surveyListingModel;

        public ShowDoctorComments(DoctorAnalyticsViewModel model)
        {
            _surveyListingModel = model;
        }

        public override void Execute(object parameter)
        {
            DoctorSurveyViewModel? surveyModel;

            if (_surveyListingModel is not null)
                surveyModel = _surveyListingModel.SelectedDoctor;
            else return;

            if (surveyModel is null) return;

            var surveys = Injector.GetService<SurveyService>()
                .GetForUser(surveyModel.Jmbg)
                .Where(s => s.AdditionalComment != "")
                .ToList();

            new SurveyCommentsView(surveys).ShowDialog();
        }
    }
}
