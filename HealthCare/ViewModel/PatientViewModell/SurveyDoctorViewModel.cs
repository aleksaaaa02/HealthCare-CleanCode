using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Model;
using HealthCare.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthCare.ViewModel.PatientViewModell
{
    public class SurveyDoctorViewModel
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


        private ObservableCollection<Appointment> appointments;
        public ObservableCollection<Appointment> Appointments
        {
            get => appointments;
            set
            {
                if (appointments != value)
                {
                    appointments = value;
                    OnPropertyChanged();
                }
            }
        }



        private Appointment selectedAppointment;

        public Appointment SelectedAppointment
        {
            get { return selectedAppointment; }
            set { selectedAppointment = value;}
        }



        public AppointmentService appointmentService = Injector.GetService<AppointmentService>();
        public SurveyService surveyService = Injector.GetService<SurveyService>();

        public RelayCommand SubmitSurvey { get; set; }

        public SurveyDoctorViewModel()
        {
            Surveys = new ObservableCollection<Survey>();
            loadSurveys();
            loadAppointments();
        }


        public void loadSurveys()
        {

            Surveys.Add(new Survey
            {
                TopicName = "DOKTOR_GENERAL",
                DoctorJMBG = "",
                Description = "Kako ste zadovoljni generalnim kvalitetom usluge bolince?",
                SelectedRating = 1,
                AdditionalComment = ""
            });


            Surveys.Add(new Survey
            {
                TopicName = "DOKTOR_PREPORUCILI",
                DoctorJMBG = "",
                Description = "Da li biste predlozili bolnicu svojim prijateljima?",
                SelectedRating = 1,
                AdditionalComment = ""
            });


            SubmitSurvey = new RelayCommand(o =>
            {
                foreach (Survey survey in Surveys)
                {
                    survey.DoctorJMBG = selectedAppointment.DoctorJMBG;
                    surveyService.Add(survey);
                }
            });




        }

        public bool checkAllSurveys()
        {
            int unCheckedSurveys = Surveys.Count(m => m.SelectedRating == 0);
            if (unCheckedSurveys > 0) return false;
            return true;
        }

        public void loadAppointments()
        {
            Appointments = new ObservableCollection<Appointment>(appointmentService.GetByPatient(Context.Current.JMBG));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
