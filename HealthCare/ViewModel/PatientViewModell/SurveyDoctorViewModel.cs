using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
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
        public DoctorService doctorService = Injector.GetService<DoctorService>();

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
                SelectedRating = 0,
                AdditionalComment = ""
            });


            Surveys.Add(new Survey
            {
                TopicName = "DOKTOR_PREPORUCILI",
                DoctorJMBG = "",
                Description = "Da li biste predlozili bolnicu svojim prijateljima?",
                SelectedRating = 0,
                AdditionalComment = ""
            });


            SubmitSurvey = new RelayCommand(o =>
            {
                if (selectedAppointment == null)
                {
                    ViewUtil.ShowWarning("Morate izabrati pregled");
                    return;
                }
                if (!checkAllSurveys())
                {
                    ViewUtil.ShowWarning("Morate popuniti sva polja");
                    return;
                }

                ViewUtil.ShowInformation("Uspesno ste popunili anketu");
                foreach (Survey survey in Surveys)
                {
                    Survey newSurvey = new Survey();
                    newSurvey.TopicName = survey.TopicName;
                    newSurvey.Description = survey.Description;
                    newSurvey.AdditionalComment = survey.AdditionalComment;
                    newSurvey.SelectedRating = survey.SelectedRating;
                    newSurvey.DoctorJMBG = selectedAppointment.DoctorJMBG;
                    surveyService.Add(newSurvey);
                }
                double newRating = surveyService.GetAverageDoctor(selectedAppointment.DoctorJMBG);
                Doctor doctor = doctorService.Get(selectedAppointment.DoctorJMBG);
                doctor.Rating = newRating;
                doctorService.Update(doctor);
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
