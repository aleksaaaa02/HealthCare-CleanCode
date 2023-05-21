using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.View;

namespace HealthCare.ViewModel.DoctorViewModel.Referrals.Commands
{
    public class AddSpecialistReferralCommand : CommandBase
    {
        private readonly Hospital _hospital;
        private readonly SpecialistReferralViewModel _specialistReferralViewModel;

        public AddSpecialistReferralCommand(Hospital hospital, SpecialistReferralViewModel specialistReferralViewModel) 
        {
            _specialistReferralViewModel = specialistReferralViewModel;
            _hospital = hospital;
        }

        public override void Execute(object parameter)
        {
            try
            {
                MakeReferral();
            }
            catch (ValidationException ex){
                Utility.ShowWarning(ex.Message);  
            }
        }

        private void MakeReferral()
        {
            string doctorJMBG = _hospital.Current.JMBG;
            string patientJMBG = _specialistReferralViewModel.ExaminedPatient.JMBG;
            bool isSpecializationReferral = _specialistReferralViewModel.IsSpecializationReferral;
            string referredDoctorJMBG = isSpecializationReferral ? SpecializationReferral() : DoctorReferral();
            
            SpecialistReferral specialistReferral = new SpecialistReferral(doctorJMBG, referredDoctorJMBG);
            _hospital.SpecialistReferralService.Add(specialistReferral);
            _hospital.PatientService.AddReferral(patientJMBG, specialistReferral.Id, false);
        }

        private string SpecializationReferral() {
            
            string specialization = _specialistReferralViewModel.Specialization;
            if(_hospital.DoctorService.GetFirstBySpecialization(specialization) is Doctor doctor)
            {
                return doctor.JMBG;
            }
            throw new ValidationException("Za datu specijalizaciju ne postoji trenutno doktor");

        }
        private string DoctorReferral()
        {
            if(_specialistReferralViewModel.SelectedDoctor is null)
            {
                throw new ValidationException("Morate odabrati doktora iz tabele");
            }

            return _specialistReferralViewModel.SelectedDoctor.JMBG;
        }
    }
}
