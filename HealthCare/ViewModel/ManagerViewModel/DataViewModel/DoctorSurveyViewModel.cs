using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModel.ManagerViewModel.DataViewModel
{
    public class DoctorSurveyViewModel
    {
        public string Jmbg { get; }
        public string Doctor { get; }
        public string RatingPresenter => $"{Math.Round(Surveys.Average(s => s.Rating), 2)}";
        public List<SurveyViewModel> Surveys { get; }

        public DoctorSurveyViewModel(Doctor doctor)
        {
            Jmbg = doctor.JMBG;
            Doctor = doctor.Name + " " + doctor.LastName;

            Surveys = Injector.GetService<SurveyService>()
                .GetForUser(Jmbg)
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
            ).ToList();
        }
    }
}
