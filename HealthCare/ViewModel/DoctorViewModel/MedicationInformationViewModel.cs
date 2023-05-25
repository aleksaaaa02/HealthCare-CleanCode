using HealthCare.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HealthCare.ViewModel.DoctorViewModel
{
    public class MedicationInformationViewModel : ViewModelBase
    {
        private ObservableCollection<string> _medicationIngredients;
        private int _id;
        private string _name;
        private string _description;
        private readonly Medication _medication;

        public IEnumerable<string> MedicationIngredients => _medicationIngredients;

        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(ID));
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description)); 
            }
        }

        public MedicationInformationViewModel(Medication medication) 
        {
            _medication = medication;
            _medicationIngredients = new ObservableCollection<string>();

            Description = medication.Description;
            ID = medication.Id;
            Name = medication.Name; 

            Update();
        }
        private void Update()
        {
            _medicationIngredients.Clear();
            foreach(string ingredient in _medication.Ingredients)
            {
                _medicationIngredients.Add(ingredient);
            }
        }
    }
}
