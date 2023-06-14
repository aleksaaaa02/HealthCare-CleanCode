using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service.UserService;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using HealthCare.ViewModel.DoctorViewModel.Referrals.Commands;

namespace HealthCare.ViewModel.DoctorViewModel.Referrals;

public class SpecialistReferralViewModel : ViewModelBase
{
    private readonly ObservableCollection<DoctorsViewModel> _doctors;
    private readonly DoctorService _doctorService;
    public readonly Patient ExaminedPatient;
    private bool _isSpecializationReferral;

    private DoctorsViewModel _selectedDoctor;
    private string _specialization;

    public SpecialistReferralViewModel(Patient patient)
    {
        _doctorService = Injector.GetService<DoctorService>();
        ExaminedPatient = patient;
        _specialization = "";
        MakeSpecialistReferralCommand = new AddSpecialistReferralCommand(this);


        _doctors = new ObservableCollection<DoctorsViewModel>();
        Update();
    }

    public IEnumerable<DoctorsViewModel> Doctors => _doctors;

    public string Specialization
    {
        get => _specialization;
        set
        {
            _specialization = value;
            OnPropertyChanged();
        }
    }

    public DoctorsViewModel SelectedDoctor
    {
        get => _selectedDoctor;
        set
        {
            _selectedDoctor = value;
            OnPropertyChanged();
        }
    }

    public bool IsSpecializationReferral
    {
        get => _isSpecializationReferral;
        set
        {
            _isSpecializationReferral = value;
            OnPropertyChanged();
        }
    }

    public ICommand MakeSpecialistReferralCommand { get; }

    private void Update()
    {
        _doctors.Clear();
        foreach (var doctor in _doctorService.GetAll()) _doctors.Add(new DoctorsViewModel(doctor));
    }
}