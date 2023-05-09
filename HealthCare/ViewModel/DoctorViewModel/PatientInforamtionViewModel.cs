using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HealthCare.View.DoctorView
{
    public class PatientInforamtionViewModel : ViewModelBase
    {
        private ObservableCollection<string> _previousDiseases;
        private Patient _selectedPatient;
        private Visibility _gridVisibility;
        private bool _isReadOnly = true;
        private bool _isFocusable = false;
        private string _name;
        private string _lastName;
        private string _jmbg;
        private DateTime _birthday;
        private Gender _gender;
        private float _height;
        private float _weight;
        private string _selectedDisease;
        private string _disease;

        public IEnumerable<string> PreviousDisease => _previousDiseases;
        public Visibility GridVisibility => _gridVisibility;
        public bool IsFocusable => _isFocusable;
        public bool IsReadOnly => _isReadOnly;
        public ICommand SaveChangesCommand { get; }
        public ICommand NewDiseaseCommand { get; }
        public ICommand RemoveDiseaseCommand { get; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public string JMBG
        {
            get => _jmbg;
            set
            {
                _jmbg = value;
                OnPropertyChanged(nameof(JMBG));
            }
        }

        public DateTime Birthday
        {
            get => _birthday;
            set
            {
                _birthday = value;
                OnPropertyChanged(nameof(Birthday));
            }
        }
        public Gender Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }
        public float Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }
        public float Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }
        public string SelectedDisease
        {
            get => _selectedDisease;
            set
            {
                _selectedDisease = value;
                OnPropertyChanged(nameof(SelectedDisease));
            }
        }
        public string Disease
        {
            get => _disease;
            set
            {
                _disease = value;
                OnPropertyChanged(nameof(Disease));
            }
        }
        public PatientInforamtionViewModel(Patient patient, Hospital hospital, bool isEditing) 
        {
            _selectedPatient = patient;
            _isFocusable = isEditing;
            _isReadOnly = !isEditing;

            SaveChangesCommand = new SavePatientChangesCommand(hospital, patient, this);
            NewDiseaseCommand = new AddDiseaseCommand(this);
            RemoveDiseaseCommand = new RemoveDiseaseCommand(this);
            _previousDiseases = new ObservableCollection<string>();
            LoadDataIntoView(patient, isEditing);
        }
        public void LoadDataIntoView(Patient patient, bool isEditing)
        {
            Name = patient.Name;
            LastName = patient.LastName;
            Birthday = patient.BirthDate;
            Gender = patient.Gender;
            JMBG = patient.JMBG;

            if (patient.MedicalRecord != null)
            {
                Weight = patient.MedicalRecord.Weight;
                Height = patient.MedicalRecord.Height;
                if (patient.MedicalRecord.MedicalHistory != null)
                {
                    Update();
                }
            }
            if (isEditing)
            {
                _gridVisibility = Visibility.Visible;
            }
            else
            {
                _gridVisibility = Visibility.Collapsed;
            }

        }
        public void Update()
        {
            _previousDiseases.Clear();
            foreach(var d in _selectedPatient.MedicalRecord.MedicalHistory)
            {
                _previousDiseases.Add(d);
            }
        }
        public void AddPreviousDisease(string disease)
        {
            _previousDiseases.Add(disease);
        }
        public void RemovePreviousDisease(string disease)
        {
            _previousDiseases.Remove(disease);
        }

    }
}
