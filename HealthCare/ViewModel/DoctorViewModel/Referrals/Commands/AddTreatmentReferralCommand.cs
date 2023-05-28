using HealthCare.Command;
using HealthCare.Application;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;
using HealthCare.View.DoctorView.ReferralView;
using System.Collections.Generic;

namespace HealthCare.ViewModel.DoctorViewModel.Referrals.Commands
{
    public class AddTreatmentReferralCommand : CommandBase
    {
        private readonly TreatmentReferralService _treatmentReferralService;
        private readonly MedicationService _medicationService;
        private readonly TherapyService _therapyService;
        private readonly TreatmentReferralViewModel _treatmentReferralViewModel;
        private readonly Patient _examinedPatient;
        public AddTreatmentReferralCommand(TreatmentReferralViewModel treatmentReferralViewModel) 
        { 
            _treatmentReferralService = Injector.GetService<TreatmentReferralService>();
            _medicationService = Injector.GetService<MedicationService>();
            _therapyService = Injector.GetService<TherapyService>();

            _treatmentReferralViewModel = treatmentReferralViewModel;
            _examinedPatient = treatmentReferralViewModel.ExaminedPatient;
        }
        public override void Execute(object parameter)
        {
            try
            {
                MakeReferral();
                Utility.ShowInformation("Uput za bolnicko lecenje uspesno dodat");
            }
            catch (ValidationException ex)
            {
                Utility.ShowWarning(ex.Message);
            }
        }

        private void MakeReferral()
        {
            List<int> medication = GetMedication();
            int daysOfTreatment = _treatmentReferralViewModel.DaysOfTreatment;
            string doctorJMBG = Context.Current.JMBG;
            List<string> additionalExamination = Utility.GetStringList(_treatmentReferralViewModel.AdditionalExamination);
            
            CheckPatientAllergies(_examinedPatient, medication);

            int therapyID = GetTherapy(medication);

            TreatmentReferral treatmentReferral = new TreatmentReferral(daysOfTreatment, _examinedPatient.JMBG, doctorJMBG, therapyID, additionalExamination);
            _treatmentReferralService.Add(treatmentReferral);
        }

        private List<int> GetMedication()
        {
            List<int> selectedMedication = new List<int>();
            foreach(var medication in _treatmentReferralViewModel.Medications)
            {
                if (medication.InitialTherapy)
                {
                    selectedMedication.Add(medication.MedicationId);
                }
            }
            return selectedMedication;
        }
        private void CheckPatientAllergies(Patient patient, List<int> therapy)
        {
            foreach(int MedicationID in therapy)
            {
                Medication medication = _medicationService.Get(MedicationID);

                if (patient.IsAllergic(medication.Ingredients)) 
                    throw new ValidationException("Pacijent je alergican na lek: " + medication.Name);
            }
        }
        private int GetTherapy(List<int> medication) 
        {   
            Therapy therapy = new Therapy(new List<int>(), _examinedPatient.JMBG);

            foreach(int MedicationID in medication)
            {
                new TherapyInformation(_examinedPatient, MedicationID, therapy).ShowDialog();
            }
            _therapyService.Add(therapy);

            return therapy.Id;
        }
    }
}
