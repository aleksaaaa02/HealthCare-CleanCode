using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModel.NurseViewModel.DataViewModel;
using System.Collections.ObjectModel;

namespace HealthCare.ViewModel.NurseViewModel
{
    public class PrescriptionListingViewModel
    {
        private readonly PrescriptionService _prescriptionService;
        private readonly MedicationService _medicationService;
        private readonly Inventory _medicationInventory;
        private readonly DoctorService _doctorService;
        public ObservableCollection<PrescriptionViewModel> Prescriptions { get; set; }
        private Patient _patient;
        public PrescriptionListingViewModel(Patient patient) {
            Prescriptions = new ObservableCollection<PrescriptionViewModel>();

            _prescriptionService = (PrescriptionService)ServiceProvider.services["PrescriptionService"];
            _medicationService = (MedicationService)ServiceProvider.services["MedicationService"];
            _medicationInventory = (Inventory)ServiceProvider.services["MedicationInventory"];
            _doctorService = (DoctorService)ServiceProvider.services["DoctorService"];

            _patient = patient;
        }

        public void Update()
        {
            Prescriptions.Clear();
            foreach (Prescription prescription in _prescriptionService.GetPatientsPrescriptions(_patient.JMBG)) {
                Doctor doctor = _doctorService.Get(prescription.DoctorJMBG);
                Prescriptions.Add(new PrescriptionViewModel(prescription,
                _medicationInventory.GetTotalQuantity(prescription.MedicationId),
                _medicationService.Get(prescription.MedicationId).Name,
                doctor.Name + " " + doctor.LastName));
            }
        }
    }
}
