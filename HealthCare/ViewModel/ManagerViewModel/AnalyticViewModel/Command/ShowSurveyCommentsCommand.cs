using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Service;
using HealthCare.View;
using HealthCare.View.ManagerView.AnalyticsView;
using HealthCare.ViewModel.ManagerViewModel.AnalyticViewModel;
using HealthCare.ViewModel.ManagerViewModel.DataViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModel.ManagerViewModel.AnalyticViewModel.Command
{
    internal class ShowSurveyCommentsCommand : CommandBase
    {
        private readonly SurveyListingViewModel _surveyListingModel;

        public ShowSurveyCommentsCommand(SurveyListingViewModel model)
        {
            _surveyListingModel = model;
        }

        public override void Execute(object parameter)
        {
            SurveyViewModel? surveyModel;

            if (_surveyListingModel is not null)
                surveyModel = _surveyListingModel.SelectedSurvey;
            else return;

            if (surveyModel is null) return;

            var surveys = Injector.GetService<SurveyService>()
                .GetForUser("")
                .Where(s => s.TopicName == surveyModel.Title && s.AdditionalComment != "")
                .ToList();

            new SurveyCommentsView(surveys).ShowDialog();
        }
    }
}
