using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HealthCare.ViewModel.DoctorViewModel.Prescriptions
{
    public class PrescriptionViewModel : ViewModelBase
    {
        private readonly MedicationService _medicationService;
        private ObservableCollection<MedicationViewModel> _medications;
        private readonly Patient _examinedPatient;
        private MedicationViewModel _selectedMedication;

        public MedicationViewModel SelectedMedication { get; set; }

        public IEnumerable<MedicationViewModel> Medications => _medications;

        public bool BeforeMeal { get; set; }
        public bool DuringMeal { get; set; }
        public bool AfterMeal { get; set; }
        public bool NoPreference { get; set; }
        public int DailyDosage { get; set; }
        public int HoursBetweenConsumption { get; set; }
        public int ConsumptionDays { get; set; }

        public ICommand MakePrescriptionCommand { get; }
        public PrescriptionViewModel(Patient patient) 
        {
            _medicationService = Injector.GetService<MedicationService>();
            _examinedPatient = patient;
            BeforeMeal = true;
            _medications = new ObservableCollection<MedicationViewModel>();

            MakePrescriptionCommand = new AddPrescriptionCommand(patient, this);

            Update();
        }
    
        private void Update()
        {
            _medications.Clear();
            foreach(var medication in _medicationService.GetAll())
            {
                _medications.Add(new MedicationViewModel(medication));
            }
        }
    }
}
