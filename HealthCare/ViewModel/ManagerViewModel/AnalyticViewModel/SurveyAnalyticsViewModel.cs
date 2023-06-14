using HealthCare.View.ManagerView.AnalyticsView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModel.ManagerViewModel.AnalyticViewModel
{
    public class SurveyAnalyticsViewModel : ViewModelBase
    {
        public ObservableCollection<AnalyticsTypeViewModel> AnalyticsTypes { get; }

        private AnalyticsTypeViewModel _selectedAnalytic;
        public AnalyticsTypeViewModel SelectedAnalytic 
        {
            get => _selectedAnalytic;
            set
            {
                _selectedAnalytic = value;
                OnPropertyChanged();
            }
        }

        public SurveyAnalyticsViewModel()
        {
            AnalyticsTypes = new ObservableCollection<AnalyticsTypeViewModel>();
            LoadAnalyticTypes();
            _selectedAnalytic = AnalyticsTypes[0];
        }

        private void LoadAnalyticTypes()
        {
            var model1 = new SurveyListingViewModel();
            AnalyticsTypes.Add(new AnalyticsTypeViewModel(
                    "bolnica", "Analitika bolnice", 
                    new SurveyListingControl(model1), model1)
            );

            var model2 = new DoctorAnalyticsViewModel();
            AnalyticsTypes.Add(new AnalyticsTypeViewModel(
                    "doktori", "Analitika zaposlenih doktora", 
                    new DoctorAnalyticsControl(model2), model2)
            );
        }
    }
}
