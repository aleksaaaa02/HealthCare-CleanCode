
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthCare.Application;
using HealthCare.Service;

namespace HealthCare.ViewModel.DoctorViewModel.Treatment
{
    public class DoctorTreatmentViewModel : ViewModelBase
    {
        private ObservableCollection<TreatmentViewModel> _treatments;
        private TreatmentService _treatmentService;
        public IEnumerable<TreatmentViewModel> Treatments => _treatments;

        public TreatmentViewModel? SelectedTreatment { get; set; }
        public ICommand PayPatientVisitCommand { get; }


        public DoctorTreatmentViewModel()
        {
            _treatmentService = Injector.GetService<TreatmentService>();
            _treatments = new ObservableCollection<TreatmentViewModel>();
            PayPatientVisitCommand = new VisitPatientCommand(this);

            Update();
        }


        public void Update()
        {
            _treatments.Clear();
            foreach (var treatment in _treatmentService.GetAll())
            {
                _treatments.Add(new TreatmentViewModel(treatment));
            }

        }
    }
}
