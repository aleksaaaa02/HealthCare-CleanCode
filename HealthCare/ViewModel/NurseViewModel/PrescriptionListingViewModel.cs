using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel.NurseViewModel.DataViewModel;
using System.Collections.ObjectModel;

namespace HealthCare.ViewModel.NurseViewModel
{
    public class PrescriptionListingViewModel
    {
        public ObservableCollection<PrescriptionViewModel> Prescriptions { get; set; }
        private Hospital _hospital;
        private Patient _patient;
        public PrescriptionListingViewModel(Patient patient, Hospital hospital) {
            Prescriptions = new ObservableCollection<PrescriptionViewModel>();
            _hospital = hospital;
            _patient = patient;
        }

        public void Update() {
            Prescriptions.Clear();
            foreach (Prescription prescription in _hospital.PrescriptionService.GetPatientsPrescriptions(_patient.JMBG))
                Prescriptions.Add(new PrescriptionViewModel(prescription,
                    _hospital.Inventory.GetTotalQuantity(prescription.MedicationId)));
        }
    }
}
