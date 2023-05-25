using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Exceptions;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;

namespace HealthCare.ViewModel.DoctorViewModel.Referrals.Commands
{
    public class AddSpecialistReferralCommand : CommandBase
    {
        private readonly SpecialistReferralViewModel _specialistReferralViewModel;
        private readonly DoctorService _doctorService;
        private readonly PatientService _patientService;
        private readonly SpecialistReferralService _specialistReferralService;
        public AddSpecialistReferralCommand(SpecialistReferralViewModel specialistReferralViewModel) 
        {
            _doctorService = (DoctorService)ServiceProvider.services["DoctorService"];
            _patientService = (PatientService)ServiceProvider.services["PatientService"];
            _specialistReferralService = (SpecialistReferralService)ServiceProvider.services["SpecialistReferralService"];
            _specialistReferralViewModel = specialistReferralViewModel;
        }

        public override void Execute(object parameter)
        {
            try
            {
                MakeReferral();
                Utility.ShowInformation("Uput za doktora specijalistu uspesno dodat");
            }
            catch (ValidationException ex){
                Utility.ShowWarning(ex.Message);  
            }
        }

        private void MakeReferral()
        {
            string doctorJMBG = Hospital.Current.JMBG;
            string patientJMBG = _specialistReferralViewModel.ExaminedPatient.JMBG;
            bool isSpecializationReferral = _specialistReferralViewModel.IsSpecializationReferral;
            string referredDoctorJMBG = isSpecializationReferral ? SpecializationReferral() : DoctorReferral();
            
            SpecialistReferral specialistReferral = new SpecialistReferral(doctorJMBG, referredDoctorJMBG);
            _specialistReferralService.Add(specialistReferral);
            _patientService.AddReferral(patientJMBG, specialistReferral.Id, false);
        }

        private string SpecializationReferral() {
            
            string specialization = _specialistReferralViewModel.Specialization;
            if(_doctorService.GetFirstBySpecialization(specialization) is Doctor doctor)
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
