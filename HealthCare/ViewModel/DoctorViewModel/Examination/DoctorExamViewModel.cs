using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModel.DoctorViewModel.Examination.Commands;
using HealthCare.ViewModel.DoctorViewModel.PatientInformation.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HealthCare.ViewModel.DoctorViewModel.Examination
{
    public class DoctorExamViewModel : ViewModelBase
    {
        private readonly AnamnesisService _anamnesisService;
        private readonly PatientService _patientService;
        private Appointment _appointment;
        private Patient _selectedPatient;

        private ObservableCollection<string> _previousDiseases;
        private ObservableCollection<string> _allergies;
        private string _name;
        private string _lastName;
        private string _jmbg;
        private string _selectedDisease;
        private float _height;
        private float _weight;
        private string _disease;
        private string _symptoms;
        private string _conclusion;

        public IEnumerable<string> Allergies => _allergies;
        public IEnumerable<string> PreviousDisease => _previousDiseases;

        public ICommand FinishExaminationCommand { get; }
        public ICommand MakeSpecialistReferralCommand { get; }
        public ICommand MakeTreatmentReferralCommand { get; }
        public ICommand MakePrescriptionCommand { get; }
        public ICommand UpdatePatientCommand { get; }

        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public string JMBG
        {
            get { return _jmbg; }
            set
            {
                _jmbg = value;
                OnPropertyChanged(nameof(JMBG));
            }
        }
        public float Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }
        public float Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }
        public string SelectedDisease
        {
            get { return _selectedDisease; }
            set
            {
                _selectedDisease = value;
                OnPropertyChanged(nameof(SelectedDisease));
            }
        }
        public string Disease
        {
            get { return _disease; }
            set
            {
                _disease = value;
                OnPropertyChanged(nameof(Disease));
            }
        }
        public string Symptoms
        {
            get { return _symptoms; }
            set
            {
                _disease = value;
                OnPropertyChanged(nameof(Symptoms));
            }
        }
        public string Conclusion
        {
            get { return _conclusion; }
            set
            {
                _conclusion = value;
                OnPropertyChanged(nameof(Conclusion));
            }
        }


        public DoctorExamViewModel(Window window, Appointment appointment, int roomId)
        {
            _anamnesisService = (AnamnesisService)ServiceProvider.services["AnamnesisService"];
            _patientService = (PatientService)ServiceProvider.services["PatientService"];
            _appointment = appointment;
            _selectedPatient = _patientService.Get(appointment.Patient.Key);
            
            
            UpdatePatientCommand = new ShowPatientInfoCommand(this, true);
            FinishExaminationCommand = new FinishExaminationCommand(window, appointment, this, roomId);
            MakeSpecialistReferralCommand = new ShowSpecialistReferralViewCommand(_selectedPatient);
            MakeTreatmentReferralCommand = new ShowTreatmentReferralViewCommand(_selectedPatient);
            MakePrescriptionCommand = new ShowPrescriptionViewCommand(_selectedPatient);
            LoadView();
        }
        private void LoadView()
        {
            _name = _selectedPatient.Name;
            _lastName = _selectedPatient.LastName;
            _jmbg = _selectedPatient.JMBG;
            _height = _selectedPatient.MedicalRecord.Height;
            _weight = _selectedPatient.MedicalRecord.Weight;

            Anamnesis anamnesis = _anamnesisService.Get(_appointment.AnamnesisID);
            _symptoms = string.Join(", ", anamnesis.Symptoms);
            _previousDiseases = new ObservableCollection<string>();
            _allergies = new ObservableCollection<string>();
            Update();
        }
        private void Update()
        {
            UpdateDiseases();
            UpdateAllergies();
        }
        private void UpdateDiseases()
        {
            _previousDiseases.Clear();
            foreach (var disease in _selectedPatient.MedicalRecord.MedicalHistory)
            {
                _previousDiseases.Add(disease);
            }
        }
        private void UpdateAllergies()
        {
            _allergies.Clear();
            foreach (var allergy in _selectedPatient.MedicalRecord.Allergies)
            {
                _allergies.Add(allergy);
            }
        }
        public void RefreshView()
        {
            Update();
            Height = SelectedPatient.MedicalRecord.Height;
            Weight = SelectedPatient.MedicalRecord.Weight;
        }

    }
}
