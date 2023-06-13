using HealthCare.Application;
using HealthCare.Service;
using HealthCare.ViewModel.ManagerViewModel.AnalyticViewModel.Command;
using HealthCare.ViewModel.ManagerViewModel.Command;
using HealthCare.ViewModel.ManagerViewModel.DataViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthCare.ViewModel.ManagerViewModel.AnalyticViewModel
{
    public class SurveyListingViewModel
    {
        public ObservableCollection<SurveyViewModel> SurveyItems { get; }

        public SurveyViewModel? SelectedSurvey { get; set; }
        public ICommand ShowSurveyCommentsCommand { get; }

        public SurveyListingViewModel()
        {
            ShowSurveyCommentsCommand = new ShowSurveyCommentsCommand(this);
            SurveyItems = new ObservableCollection<SurveyViewModel>();
            LoadAll();
        }

        private void LoadAll()
        {
            SurveyItems.Clear();

            var service = Injector.GetService<SurveyService>();
            service.GetHospitalSurveys()
                .GroupBy(s => s.TopicName)
                .Select(g => new SurveyViewModel(
                    g.Key,
                    g.Average(x => x.SelectedRating ?? 0), 
                    new List<int> {
                        g.Count(x => x.SelectedRating == 1),
                        g.Count(x => x.SelectedRating == 2),
                        g.Count(x => x.SelectedRating == 3),
                        g.Count(x => x.SelectedRating == 4),
                        g.Count(x => x.SelectedRating == 5)
                    }
                )
            ).ToList().ForEach(m => SurveyItems.Add(m));
        }
    }
}
