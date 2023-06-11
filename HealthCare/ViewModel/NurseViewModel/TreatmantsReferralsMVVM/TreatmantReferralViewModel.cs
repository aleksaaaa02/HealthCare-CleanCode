using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HealthCare.ViewModel.NurseViewModel.TreatmantsReferralsMVVM
{
    public class TreatmantReferralViewModel : ViewModelBase
    {
        private ObservableCollection<PatientsTreatmantRefarralsViewModel> _referrals;
        private readonly TreatmentReferralService _treatmentReferralService;
        public ObservableCollection<PatientsTreatmantRefarralsViewModel> Referrals
        {
            get => _referrals;
            set
            {
                _referrals = value;
            }
        }

        private PatientsTreatmantRefarralsViewModel _selected;
        public PatientsTreatmantRefarralsViewModel Selected {
            get => _selected;
            set {
                _selected = value;
            }
        }

        private Patient _patient;
        public TreatmantReferralViewModel(Patient patient){
            _treatmentReferralService = Injector.GetService<TreatmentReferralService>();
            _patient = patient;
            loadReferrals();
        }

        public void loadReferrals() {
            List<TreatmentReferral> referrals = _treatmentReferralService.GetPatientsReferrals(_patient.JMBG);
            List<PatientsTreatmantRefarralsViewModel> referralsViewModel = new List<PatientsTreatmantRefarralsViewModel>();
            foreach (TreatmentReferral referral in referrals) {
                PatientsTreatmantRefarralsViewModel model = new PatientsTreatmantRefarralsViewModel(referral);
                referralsViewModel.Add(model);
            }
            Referrals = new ObservableCollection<PatientsTreatmantRefarralsViewModel>(referralsViewModel);
        }

    }
}
