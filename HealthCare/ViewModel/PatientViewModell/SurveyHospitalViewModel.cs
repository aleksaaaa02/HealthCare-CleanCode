using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModel.PatientViewModell
{
    public class SurveyHospitalViewModel
    {
        private ObservableCollection<Survey> surveys;
        public ObservableCollection<Survey> Surveys
        {
            get => surveys;
            set
            {
                if (surveys != value)
                {
                    surveys = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public AppointmentService appointmentService = Injector.GetService<AppointmentService>();
        public SurveyService surveyService = Injector.GetService<SurveyService>();

        public RelayCommand SubmitSurvey { get; set; }

        public SurveyHospitalViewModel()
        {
            Surveys = new ObservableCollection<Survey>();
            loadSurveys();
        }


        public void loadSurveys()
        {

            Surveys.Add(new Survey
            {
                TopicName = "HOSPITAL_GENERAL",
                DoctorJMBG = "",
                Description = "Kako ste zadovoljni generalnim kvalitetom usluge bolince?",
                SelectedRating = 0,
                AdditionalComment = ""
            });

            Surveys.Add(new Survey
            {
                TopicName = "HOSPITAL_CISTOCA",
                DoctorJMBG = "",
                Description = "Kako ste zadovoljni cistocom bolnice?",
                SelectedRating = 0,
                AdditionalComment = ""
            });

            Surveys.Add(new Survey
            {
                TopicName = "HOSPITAL_PREPORUCILI",
                DoctorJMBG = "",
                Description = "Da li biste predlozili bolnicu svojim prijateljima?",
                SelectedRating = 0,
                AdditionalComment = ""
            });


            SubmitSurvey = new RelayCommand(o =>
            {
                if (!checkAllSurveys())
                {
                    ViewUtil.ShowWarning("Niste popunili anketu");
                }
                else
                {
                    ViewUtil.ShowInformation("Uspesno ste popunili anketu");
                    foreach (Survey survey in Surveys)
                    {
                        Survey newSurvey = new Survey();
                        newSurvey.TopicName = survey.TopicName;
                        newSurvey.Description = survey.Description;
                        newSurvey.AdditionalComment = survey.AdditionalComment;
                        newSurvey.SelectedRating = survey.SelectedRating;
                        newSurvey.DoctorJMBG = survey.DoctorJMBG;
                        surveyService.Add(newSurvey);
                    }
                }
            });




        }

        public bool checkAllSurveys()
        {
            int unCheckedSurveys = Surveys.Count(m => m.SelectedRating==0);
            if (unCheckedSurveys > 0) return false;
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }


}
