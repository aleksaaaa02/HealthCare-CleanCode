using HealthCare.Model;
using HealthCare.ViewModel;
using HealthCare.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.View.DoctorView
{
    class PatientInforamtionViewModel : ViewModelBase
    {
        public ObservableCollection<string> previousDiseases;
        public IEnumerable<string> PreviousDisease => previousDiseases;

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
        private readonly Patient _selectedPatient;
        public PatientInforamtionViewModel(Patient patient) 
        {
          
            _selectedPatient = patient;
            _name = patient.Name;
            _lastName = patient.LastName;
            _birthday = patient.BirthDate;
            _gender = patient.Gender;
            _jmbg = patient.JMBG;
            if(patient.MedicalRecord != null) 
            {
                _weight = patient.MedicalRecord.Weight;
                _height = patient.MedicalRecord.Height;
                if (patient.MedicalRecord.MedicalHistory != null)
                {
                    previousDiseases = new ObservableCollection<string>(patient.MedicalRecord.MedicalHistory);
                }
                else
                {
                    previousDiseases = new ObservableCollection<string>();
                }
            }

        }




    }
}
