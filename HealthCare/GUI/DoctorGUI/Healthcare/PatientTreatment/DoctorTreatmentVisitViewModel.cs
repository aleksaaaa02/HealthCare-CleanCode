using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HealthCare.Application;
using HealthCare.GUI.DoctorGUI.Healthcare.MedicalPrescription;
using HealthCare.GUI.DoctorGUI.Healthcare.MedicationTherapy.Command;
using HealthCare.GUI.DoctorGUI.Healthcare.PatientTreatment.Command;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.Service.UserService;
using HealthCare.View;
using HealthCare.ViewModel;

namespace HealthCare.GUI.DoctorGUI.Healthcare.PatientTreatment
{
    public class DoctorTreatmentVisitViewModel : ViewModelBase
    {
        private readonly Patient _patient;
        private readonly PrescriptionService _prescriptionService;
        private readonly Therapy _therapy;
        private readonly Model.Treatment _treatment;
        private readonly TreatmentReferral _treatmentReferral;
        private DateTime _end;

        private ObservableCollection<TherapyPrescriptionViewModel> _therapyMedications;

        public DoctorTreatmentVisitViewModel(Window window, Model.Treatment treatment)
        {
            _prescriptionService = Injector.GetService<PrescriptionService>(Injector.THERAPY_PRESCRIPTION_S);
            _treatment = treatment;
            _treatmentReferral = Injector.GetService<TreatmentReferralService>().Get(treatment.ReferralId);
            _patient = Injector.GetService<PatientService>().Get(_treatmentReferral.PatientJMBG);
            _therapy = Injector.GetService<TherapyService>().Get(_treatmentReferral.TherapyID);


            LoadInformation(treatment);

            ContinueTreatmentCommand = new IncreaseTreatmentDurationCommand(this, _treatment);
            RemoveMedicationFromTherapyCommand = new RemoveMedicationFromTherapyCommand(this, _therapy);
            AddMedicationToTherapyCommand = new ShowAddTherapyDialogCommand(this, _therapy);
            ReleasePatientCommand = new ReleasePatientCommand(window, _treatment);
            _therapyMedications = new ObservableCollection<TherapyPrescriptionViewModel>();
            Update();
        }

        public TherapyPrescriptionViewModel SelectedPrescription { get; set; }
        public string PatientJMBG { get; set; }
        public string PatientNameAndLastName { get; set; }
        public DateTime Start { get; set; }

        public DateTime End
        {
            get => _end;
            set
            {
                _end = value;
                OnPropertyChanged(nameof(End));
            }
        }

        public string AdditionalExamination { get; set; }
        public int DurationIncreaseDays { get; set; }
        public IEnumerable<TherapyPrescriptionViewModel> TherapyMedication => _therapyMedications;


        public ICommand ReleasePatientCommand { get; }
        public ICommand ContinueTreatmentCommand { get; }
        public ICommand AddMedicationToTherapyCommand { get; }
        public ICommand RemoveMedicationFromTherapyCommand { get; }

        public void LoadInformation(Model.Treatment treatment)
        {
            Start = treatment.TreatmentDuration.Start;
            End = treatment.TreatmentDuration.End;
            PatientJMBG = _treatmentReferral.PatientJMBG;
            PatientNameAndLastName = _patient.Name + " " + _patient.LastName;
            AdditionalExamination = ViewUtil.ToString(_treatmentReferral.AdditionalExamination);
        }

        public void Update()
        {
            _therapyMedications.Clear();
            foreach (var prescriptionID in _therapy.InitialMedication)
            {
                Prescription prescription = _prescriptionService.Get(prescriptionID);
                _therapyMedications.Add(new TherapyPrescriptionViewModel(prescription));
            }
        }
    }
}