using System.Collections.ObjectModel;
using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.Service.UserService;
using HealthCare.ViewModel.NurseViewModel.DataViewModel;

namespace HealthCare.ViewModel.NurseViewModel
{
    public class ReferralListingViewModel
    {
        private readonly DoctorService _doctorService;
        private readonly SpecialistReferralService _specialistReferralService;
        private Patient _patient;

        public ReferralListingViewModel(Patient patient)
        {
            Referrals = new ObservableCollection<ReferralViewModel>();
            _specialistReferralService = Injector.GetService<SpecialistReferralService>();
            _doctorService = Injector.GetService<DoctorService>();

            _patient = patient;

            Update();
        }

        public ObservableCollection<ReferralViewModel> Referrals { get; set; }

        public void Update()
        {
            Referrals.Clear();
            foreach (SpecialistReferral referral in (_specialistReferralService.GetPatientsReferrals(_patient.JMBG)))
                Referrals.Add(new ReferralViewModel(referral,
                    _doctorService.Get(referral.DoctorJMBG),
                    _doctorService.Get(referral.ReferredDoctorJMBG)));
        }
    }
}