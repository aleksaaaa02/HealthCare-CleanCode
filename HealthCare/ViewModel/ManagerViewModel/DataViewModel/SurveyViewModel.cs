using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModel.ManagerViewModel.DataViewModel
{
    public class SurveyViewModel
    {
        public string Title { get; }
        public double Rating { get; }
        public string RatingPresenter => $"{Math.Round(Rating, 2)}";
        public List<int> RatingCount { get; }

        public SurveyViewModel(string title, double rating, List<int> ratingCount)
        {
            Title = title;
            Rating = rating;
            RatingCount = ratingCount;
        }
    }
}
