using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.View;
using System.Collections.Generic;

namespace HealthCare.ViewModel.DoctorViewModel.Referrals.Commands
{
    public class AddTreatmentReferralCommand : CommandBase
    {
        private readonly Hospital _hospital;
        private readonly TreatmentReferralViewModel _treatmentReferralViewModel;
        public AddTreatmentReferralCommand(Hospital hospital, TreatmentReferralViewModel treatmentReferralViewModel) 
        { 
            _hospital = hospital;
            _treatmentReferralViewModel = treatmentReferralViewModel;
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
            int[] initialTherapy = GetTherapy();
            Patient patient = _treatmentReferralViewModel.ExaminedPatient;
            int daysOfTreatment = _treatmentReferralViewModel.DaysOfTreatment;
            string doctorJMBG = _hospital.Current.JMBG;
            string[] additionalExamination = Utility.GetArray(_treatmentReferralViewModel.AdditionalExamination);
            
            CheckPatientAllergies(patient, initialTherapy);
            
            TreatmentReferral treatmentReferral = new TreatmentReferral(daysOfTreatment, doctorJMBG, initialTherapy, additionalExamination);
            _hospital.TreatmentReferralService.Add(treatmentReferral);
            _hospital.PatientService.AddReferral(patient.JMBG, treatmentReferral.Id, true);
        }

        private int[] GetTherapy()
        {
            List<int> selectedMedication = new List<int>();
            foreach(var medication in _treatmentReferralViewModel.Medications)
            {
                if (medication.InitialTherapy)
                {
                    selectedMedication.Add(medication.MedicationId);
                }
            }
            return selectedMedication.ToArray();
        }
        private void CheckPatientAllergies(Patient patient, int[] therapy)
        {
            foreach(int MedicationID in therapy)
            {
                Medication medication = _hospital.MedicationService.Get(MedicationID);

                if (patient.IsAllergic(medication.Ingredients)) 
                    throw new ValidationException("Pacijent je alergican na lek: " + medication.Name);
            }
        }
    }
}
