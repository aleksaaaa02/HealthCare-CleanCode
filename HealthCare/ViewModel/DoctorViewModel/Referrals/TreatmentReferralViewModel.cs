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
    public class TreatmentReferralViewModel : ViewModelBase
    {
        private readonly MedicationService _medicationService;
        private ObservableCollection<MedicationViewModel> _medications;
        private string _additionalExamination;
        private int _daysOfTreatment;

        public readonly Patient ExaminedPatient;
        public IEnumerable<MedicationViewModel> Medications => _medications;

        public ICommand MakeTreatmentReferralCommand { get; }

        public int DaysOfTreatment
        {
            get { return _daysOfTreatment; }
            set {
                _daysOfTreatment = value;
                OnPropertyChanged(nameof(DaysOfTreatment));
            }
        }
        public string AdditionalExamination
        {
            get => _additionalExamination;
            set
            {
                _additionalExamination = value;
                OnPropertyChanged(nameof(AdditionalExamination));
            }
        }

        public TreatmentReferralViewModel(Patient patient) 
        {
            _medicationService = Injector.GetService<MedicationService>();
            _medications = new ObservableCollection<MedicationViewModel>();
            ExaminedPatient = patient;
            _additionalExamination = " ";
            DaysOfTreatment = 0;

            MakeTreatmentReferralCommand = new AddTreatmentReferralCommand(this);

            Update();
        }

        public void Update()
        {
            _medications.Clear();
            foreach (var medication in _medicationService.GetAll())
            {
                _medications.Add(new MedicationViewModel(medication));
            }
        
        }

    }
}
