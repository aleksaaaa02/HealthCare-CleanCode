using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.View;
using HealthCare.View.DoctorView.ReferralView;
using System.Collections.Generic;

namespace HealthCare.ViewModel.DoctorViewModel.Referrals.Commands
{
    public class AddTreatmentReferralCommand : CommandBase
    {
        private readonly Hospital _hospital;
        private readonly TreatmentReferralViewModel _treatmentReferralViewModel;
        private readonly Patient _examinedPatient;
        public AddTreatmentReferralCommand(Hospital hospital, TreatmentReferralViewModel treatmentReferralViewModel) 
        { 
            _hospital = hospital;
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
            string doctorJMBG = _hospital.Current.JMBG;
            string[] additionalExamination = Utility.GetArray(_treatmentReferralViewModel.AdditionalExamination);
            
            CheckPatientAllergies(_examinedPatient, medication);



            GetTherapy(medication);

            TreatmentReferral treatmentReferral = new TreatmentReferral(daysOfTreatment, doctorJMBG, 1, additionalExamination);
            _hospital.TreatmentReferralService.Add(treatmentReferral);
            _hospital.PatientService.AddReferral(_examinedPatient.JMBG, treatmentReferral.Id, true);
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
                Medication medication = _hospital.MedicationService.Get(MedicationID);

                if (patient.IsAllergic(medication.Ingredients)) 
                    throw new ValidationException("Pacijent je alergican na lek: " + medication.Name);
            }
        }
        private int GetTherapy(List<int> medication) 
        {   
            Therapy therapy = new Therapy(new List<int>(), _examinedPatient.JMBG);

            foreach(int MedicationID in medication)
            {
                new TherapyInformation(_hospital, _examinedPatient, MedicationID, therapy).ShowDialog();
            }
            _hospital.TherapyService.Add(therapy);

            return therapy.Id;
        }
    }
}
