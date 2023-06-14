using HealthCare.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HealthCare.ViewModel.ManagerViewModel.AnalyticViewModel
{
    public class SurveyCommentsViewModel
    {
        public ObservableCollection<Survey> Surveys { get; }
        public SurveyCommentsViewModel(List<Survey> surveys)
        {
            Surveys = new ObservableCollection<Survey>();
            surveys
                .OrderByDescending(s => s.SelectedRating)
                .ThenBy(s => s.surveyID)
                .ToList().ForEach(s => Surveys.Add(s));
        }
    }
}
