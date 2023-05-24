using HealthCare.Context;
using HealthCare.Model;
using HealthCare.ViewModel.NurseViewModel.DataViewModel;
using System.Collections.ObjectModel;

namespace HealthCare.ViewModel.NurseViewModel
{
    public class ReferralListingViewModel
    {
        public ObservableCollection<ReferralViewModel> Referrals { get; set; }
        private Hospital _hospital;
        private Patient _patient;
        public ReferralListingViewModel(Patient patient,Hospital hospital) {
            Referrals = new ObservableCollection<ReferralViewModel>();
            _hospital = hospital;
            _patient = patient;

            Update();
        }    

        public void Update()
        {
            Referrals.Clear();
            foreach (SpecialistReferral referral in (_hospital.SpecialistReferralService.GetPatientsReferrals(_patient)))
                Referrals.Add(new ReferralViewModel(referral,
                    _hospital.DoctorService.Get(referral.DoctorJMBG),
                    _hospital.DoctorService.Get(referral.ReferredDoctorJMBG)));
        }
    }
}
