
using HealthCare.Model;
using HealthCare.View;
using HealthCare.ViewModel.DoctorViewModel.Referrals.Commands;
using System.Windows.Input;

namespace HealthCare.ViewModel.DoctorViewModel.DataViewModel
{
    public class MedicationViewModel : ViewModelBase
    {
        private readonly Medication _medication;
        private bool _initialTherapy;

        public int MedicationId => _medication.Id;
        public string MedicationName => _medication.Name;
        public string Description => _medication.Description;
        public string Ingredients => ViewUtil.ToString(_medication.Ingredients);
        public bool InitialTherapy
        {
            get => _initialTherapy;
            set
            {
                _initialTherapy = value;
                OnPropertyChanged(nameof(InitialTherapy));
            }
        }        
        public ICommand ShowMedicationInformationCommand { get; }
        public MedicationViewModel(Medication medication)
        {
            _medication = medication;
            _initialTherapy = false;

            ShowMedicationInformationCommand = new ShowMedicationCommand(medication);
        }
    }
}
