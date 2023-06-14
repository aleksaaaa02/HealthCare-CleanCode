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
    public class DoctorAnalyticsViewModel
    {
        private readonly DoctorService _doctorService;
        public ObservableCollection<DoctorSurveyViewModel> SurveyItems { get; }
        public DoctorSurveyViewModel? SelectedDoctor { get; set; }
        public ICommand ShowDoctorCommentsCommand { get; }
        public ICommand LoadBestDoctorsCommand { get; }
        public ICommand LoadWorstDoctorsCommand { get; }
        public ICommand LoadAllDoctorsCommand { get; }

        public DoctorAnalyticsViewModel()
        {
            _doctorService = Injector.GetService<DoctorService>();
            ShowDoctorCommentsCommand = new ShowDoctorComments(this);
            LoadBestDoctorsCommand = new LoadBestDoctorsCommand(this);
            LoadWorstDoctorsCommand = new LoadWorstDoctorsCommand(this);
            LoadAllDoctorsCommand = new LoadAllDoctorsCommand(this);
            SurveyItems = new ObservableCollection<DoctorSurveyViewModel>();
            LoadAll();
        }

        public void LoadAll()
        {
            SurveyItems.Clear();
            _doctorService.GetAll()
                .ForEach(d => SurveyItems.Add(new DoctorSurveyViewModel(d)));
        }

        public void LoadTop3()
        {
            SurveyItems.Clear();
            _doctorService.GetAll()
                .Select(d => new DoctorSurveyViewModel(d))
                .OrderByDescending(m => m.Surveys.Average(s => s.Rating))
                .ThenBy(m => m.Doctor)
                .Take(3).ToList().ForEach(m => SurveyItems.Add(m));
        }

        public void LoadBottom3()
        {
            SurveyItems.Clear();
            _doctorService.GetAll()
                .Select(d => new DoctorSurveyViewModel(d))
                .OrderBy(m => m.Surveys.Average(s => s.Rating))
                .ThenBy(m => m.Doctor)
                .Take(3).ToList().ForEach(m => SurveyItems.Add(m));
        }
    }
}
