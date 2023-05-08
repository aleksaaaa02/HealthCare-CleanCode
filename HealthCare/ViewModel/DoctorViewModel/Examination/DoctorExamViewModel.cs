using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.Examination.Commands;
using HealthCare.ViewModels.DoctorViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HealthCare.ViewModel.DoctorViewModel.Examination
{
    public class DoctorExamViewModel : ViewModelBase
    {
        private ObservableCollection<string> _previousDiseases;
        public IEnumerable<string> PreviousDisease => _previousDiseases;

        private Appointment _appointment;
        private Patient _selectedPatient;
        private Hospital _hospital;

        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));
            }
        }

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
        private string _symptoms;
        public string Symptoms
        {
            get { return _symptoms; }
            set
            {
                _disease = value;
                OnPropertyChanged(nameof(Symptoms));
            }
        }

        private string _conclusion;
        public string Conclusion
        {
            get { return _conclusion; }
            set
            {
                _conclusion = value;
                OnPropertyChanged(nameof(Conclusion));
            }
        }

        public ICommand FinishExaminationCommand { get; }
        public ICommand CancelExaminationCommand { get; }
        public ICommand UpdatePatientCommand { get; }

        public DoctorExamViewModel(Hospital hospital, Window window, Appointment appointment, int roomId)
        {
            _hospital = hospital;
            _appointment = appointment;
            _selectedPatient = _appointment.Patient;

            UpdatePatientCommand = new ShowPatientInfoCommand(hospital, this, true);
            CancelExaminationCommand = new CancelCommand(window);
            FinishExaminationCommand = new FinishExaminationCommand(hospital, window, appointment, this, roomId);

            LoadView();
        }
        private void LoadView()
        {
            _name = _selectedPatient.Name;
            _lastName = _selectedPatient.LastName;
            _jmbg = _selectedPatient.JMBG;
            _gender = _selectedPatient.Gender;
            _birthday = _selectedPatient.BirthDate;
            _height = _selectedPatient.MedicalRecord.Height;
            _weight = _selectedPatient.MedicalRecord.Weight;

            Anamnesis anamnesis = _hospital.AnamnesisService.GetByID(_appointment.AnamnesisID);
            _symptoms = string.Join(", ", anamnesis.Symptoms);
            _previousDiseases = new ObservableCollection<string>();
            Update();
        }
        public void Update()
        {
            _previousDiseases.Clear();
            foreach (var disease in _selectedPatient.MedicalRecord.MedicalHistory)
            {
                _previousDiseases.Add(disease);
            }
        }

    }
}
