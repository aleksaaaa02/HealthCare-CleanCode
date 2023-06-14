using System.Collections.ObjectModel;
using HealthCare.View.ManagerView.AnalyticsView;

namespace HealthCare.ViewModel.ManagerViewModel.AnalyticViewModel
{
    public class SurveyAnalyticsViewModel : ViewModelBase
    {
        private AnalyticsTypeViewModel _selectedAnalytic;

        public SurveyAnalyticsViewModel()
        {
            AnalyticsTypes = new ObservableCollection<AnalyticsTypeViewModel>();
            LoadAnalyticTypes();
            _selectedAnalytic = AnalyticsTypes[0];
        }

        public ObservableCollection<AnalyticsTypeViewModel> AnalyticsTypes { get; }

        public AnalyticsTypeViewModel SelectedAnalytic
        {
            get => _selectedAnalytic;
            set
            {
                _selectedAnalytic = value;
                OnPropertyChanged();
            }
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