using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthCare.Application;
using HealthCare.Service;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using HealthCare.ViewModel.DoctorViewModel.PatientInformation.Commands;

namespace HealthCare.ViewModel.DoctorViewModel.PatientInformation;

public class PatientSearchViewModel : ViewModelBase
{
    private readonly AppointmentService _appointmentService;

    private readonly ObservableCollection<PatientViewModel> _patients;
    private readonly PatientService _patientService;

    private PatientViewModel _selectedPatient;

    public PatientSearchViewModel()
    {
        _appointmentService = Injector.GetService<AppointmentService>();
        _patientService = Injector.GetService<PatientService>();
        _patients = new ObservableCollection<PatientViewModel>();
        ShowEditPatientCommand = new ShowPatientInfoCommand(this, true);
        Update();
    }

    public IEnumerable<PatientViewModel> Patients => _patients;

    public PatientViewModel SelectedPatient
    {
        get => _selectedPatient;
        set
        {
            _selectedPatient = value;
            OnPropertyChanged();
        }
    }

    public ICommand ShowEditPatientCommand { get; }

    public void Update()
    {
        _patients.Clear();
        foreach (var patientJMBG in _appointmentService.GetExaminedPatients(Context.Current.JMBG))
        {
            var patient = _patientService.Get(patientJMBG);
            _patients.Add(new PatientViewModel(patient));
        }
    }
}