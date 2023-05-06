using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel;
using HealthCare.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HealthCare.View.DoctorView
{
    public class PatientInforamtionViewModel : ViewModelBase
    {
        private ObservableCollection<string> previousDiseases;
        public IEnumerable<string> PreviousDisease => previousDiseases;
        private Patient _selectedPatient;
        private Visibility _gridVisibility;
        public Visibility GridVisibility => _gridVisibility;

        // za calendar treba suprotno
        private bool _isFocusable = false;
        public bool IsFocusable => _isFocusable;

        public bool _isReadOnly = true;
        public bool IsReadOnly => _isReadOnly;

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        private string _jmbg;
        public string JMBG
        {
            get { return _jmbg; }
            set
            {
                _jmbg = value;
                OnPropertyChanged(nameof(JMBG));
            }
        }

        private DateTime _birthday;
        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                OnPropertyChanged(nameof(Birthday));
            }
        }
        private Gender _gender;
        public Gender Genderr
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Genderr));
            }
        }
        private float _height;
        public float Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged(nameof(_height));
            }
        }
        private float _weight;
        public float Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }
        private string _selectedDisease;
        public string SelectedDisease
        {
            get { return _selectedDisease; }
            set
            {
                _selectedDisease = value;
                OnPropertyChanged(nameof(_selectedDisease));
            }
        }
        private string _disease;
        public string Disease
        {
            get { return _disease; }
            set
            {
                _disease = value;
                OnPropertyChanged(nameof(Disease));
            }
        }
        public ICommand SaveChangesCommand { get; }
        public ICommand NewDiseaseCommand { get; }
        public ICommand RemoveDiseaseCommand { get; }
        public PatientInforamtionViewModel(Patient patient, Hospital hospital, bool isEdit) 
        {
            SaveChangesCommand = new SavePatientChangesCommand(hospital, patient, this);
            NewDiseaseCommand = new AddDiseaseCommand(this);
            RemoveDiseaseCommand = new RemoveDiseaseCommand(this);
            previousDiseases = new ObservableCollection<string>();
            LoadDataIntoView(patient, isEdit);
        }
        public void LoadDataIntoView(Patient patient, bool isEdit)
        {
            _isFocusable = isEdit;
            _isReadOnly = !isEdit;
            
            _selectedPatient = patient;
            _name = patient.Name;
            _lastName = patient.LastName;
            _birthday = patient.BirthDate;
            _gender = patient.Gender;
            _jmbg = patient.JMBG;
            if (patient.MedicalRecord != null)
            {
                _weight = patient.MedicalRecord.Weight;
                _height = patient.MedicalRecord.Height;
                if (patient.MedicalRecord.MedicalHistory != null)
                {
                    Update();
                }
            }
            if (isEdit)
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
            previousDiseases.Clear();
            foreach(var d in _selectedPatient.MedicalRecord.MedicalHistory)
            {
                previousDiseases.Add(d);
            }
        }
        public void AddDisease(string disease)
        {
            previousDiseases.Add(disease);
        }
        public void RemoveDisease(string disease)
        {
            previousDiseases.Remove(disease);
        }

    }
}
