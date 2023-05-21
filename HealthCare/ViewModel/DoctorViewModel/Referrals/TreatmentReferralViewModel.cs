using HealthCare.Context;
using HealthCare.ViewModel.DoctorViewModel.DataViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HealthCare.ViewModel.DoctorViewModel.Referrals
{
    public class TreatmentReferralViewModel : ViewModelBase
    {
        private readonly Hospital _hospital;
        private ObservableCollection<MedicationViewModel> _medications;
        private string _additionalExamination;
        private int _daysOfTreatment;

        public IEnumerable<MedicationViewModel> Medications => _medications;
        public ICommand SubmitReferral { get; }

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

        public TreatmentReferralViewModel(Hospital hospital) 
        {
            _medications = new ObservableCollection<MedicationViewModel>();
            _hospital = hospital;

            Update();
        }

        public void Update()
        {

            // TO-DO
            //_medications.Clear();
            //foreach (var doctor in _hospital.DoctorService.GetAll())
            //{
            //    _medications.Add(new MedicationViewModel());
            //}
        
        }
    }
}
