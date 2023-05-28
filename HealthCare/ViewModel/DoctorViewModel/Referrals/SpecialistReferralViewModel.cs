using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using HealthCare.ViewModel.DoctorViewModel.Referrals.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HealthCare.ViewModel.DoctorViewModel.Referrals
{
    public class SpecialistReferralViewModel : ViewModelBase
    {
        private readonly DoctorService _doctorService;

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

        public SpecialistReferralViewModel(Patient patient)
        {
            _doctorService = Injector.GetService<DoctorService>();
            ExaminedPatient = patient;
            _specialization = "";
            MakeSpecialistReferralCommand = new AddSpecialistReferralCommand(this);


            _doctors = new ObservableCollection<DoctorsViewModel>();
            Update();
        }

        private void Update()
        {
            _doctors.Clear();
            foreach (var doctor in _doctorService.GetAll())
            {
                _doctors.Add(new DoctorsViewModel(doctor));
            }

        }
    }
}
