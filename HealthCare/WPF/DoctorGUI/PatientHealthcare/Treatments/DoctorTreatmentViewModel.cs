using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthCare.Application;
using HealthCare.Core.PatientHealthcare.HealthcareTreatment;
using HealthCare.WPF.Common;
using HealthCare.WPF.DoctorGUI.PatientHealthcare.Treatments.Command;

namespace HealthCare.WPF.DoctorGUI.PatientHealthcare.Treatments
{
    public class DoctorTreatmentViewModel : ViewModelBase
    {
        private ObservableCollection<TreatmentViewModel> _treatments;
        private TreatmentService _treatmentService;


        public DoctorTreatmentViewModel()
        {
            _treatmentService = Injector.GetService<TreatmentService>();
            _treatments = new ObservableCollection<TreatmentViewModel>();
            PayPatientVisitCommand = new VisitPatientCommand(this);

            Update();
        }

        public IEnumerable<TreatmentViewModel> Treatments => _treatments;

        public TreatmentViewModel? SelectedTreatment { get; set; }
        public ICommand PayPatientVisitCommand { get; }


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