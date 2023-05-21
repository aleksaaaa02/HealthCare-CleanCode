﻿
using HealthCare.Model;
using HealthCare.View;

namespace HealthCare.ViewModel.DoctorViewModel.DataViewModel
{
    public class MedicationViewModel : ViewModelBase
    {
        private readonly Medication _medication;
        private bool _initialTherapy;

        public int MedicationId => _medication.Id;
        public string MedicationName => _medication.Name;
        public string Description => _medication.Description;
        public string Ingredients => Utility.ToString(_medication.Ingredients);
        public bool InitialTherapy
        {
            get => _initialTherapy;
            set
            {
                _initialTherapy = value;
                OnPropertyChanged(nameof(InitialTherapy));
            }
        }        
        public MedicationViewModel(Medication medication)
        {
            _medication = medication;
            _initialTherapy = false;
        }
    }
}
