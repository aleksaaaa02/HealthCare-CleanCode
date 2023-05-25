using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using HealthCare.ViewModel.DoctorViewModel.Referrals.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HealthCare.ViewModel.DoctorViewModel.Referrals
{
    public class SpecialistReferralViewModel : ViewModelBase
    {
        private readonly Hospital _hospital;

        private DoctorsViewModel _selectedDoctor;
        private ObservableCollection<DoctorsViewModel> _doctors;
        private string _specialization;
        private bool _isSpecializationReferral;
        public readonly Patient ExaminedPatient;        

        public IEnumerable<DoctorsViewModel> Doctors => _doctors;
        public string Specialization
        {
            get => _specialization;
            set
            {
                _specialization = value;
                OnPropertyChanged(nameof(Specialization));
            }
        }
        public DoctorsViewModel SelectedDoctor
        {
            get => _selectedDoctor;
            set
            {
                _selectedDoctor = value;
                OnPropertyChanged(nameof(SelectedDoctor));
            }
        }
        public bool IsSpecializationReferral
        {
            get => _isSpecializationReferral;
            set
            {
                _isSpecializationReferral = value;
                OnPropertyChanged(nameof(IsSpecializationReferral));
            }
        }
        public ICommand MakeSpecialistReferralCommand { get; }

        public SpecialistReferralViewModel(Hospital hospital, Patient patient)
        {
            _hospital = hospital;
            ExaminedPatient = patient;

            MakeSpecialistReferralCommand = new AddSpecialistReferralCommand(_hospital, this);


            _doctors = new ObservableCollection<DoctorsViewModel>();
            Update();
        }

        private void Update()
        {
            _doctors.Clear();
            foreach (var doctor in _hospital.DoctorService.GetAll())
            {
                _doctors.Add(new DoctorsViewModel(doctor));
            }

        }
    }
}
