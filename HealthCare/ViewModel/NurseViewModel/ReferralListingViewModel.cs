using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.ViewModel.NurseViewModel.DataViewModel;
using System.Collections.ObjectModel;

namespace HealthCare.ViewModel.NurseViewModel
{
    public class ReferralListingViewModel
    {
        public ObservableCollection<ReferralViewModel> Referrals { get; set; }
        private readonly SpecialistReferralService _specialistReferralService;
        private readonly DoctorService _doctorService;
        private Patient _patient;
        public ReferralListingViewModel(Patient patient) {
            Referrals = new ObservableCollection<ReferralViewModel>();
            _specialistReferralService = (SpecialistReferralService)ServiceProvider.services["SpecialistReferralService"];
            _doctorService = (DoctorService)ServiceProvider.services["DoctorService"];

            _patient = patient;

            Update();
        }    

        public void Update()
        {
            Referrals.Clear();
            foreach (SpecialistReferral referral in (_specialistReferralService.GetPatientsReferrals(_patient)))
                Referrals.Add(new ReferralViewModel(referral,
                    _doctorService.Get(referral.DoctorJMBG),
                    _doctorService.Get(referral.ReferredDoctorJMBG)));
        }
    }
}
