using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

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


        public TreatmantReferralViewModel(){
            _treatmentReferralService = Injector.GetService<TreatmentReferralService>();
            loadReferrals();
        }

        public void loadReferrals() {
            List<TreatmentReferral> referrals = _treatmentReferralService.GetAll();
            List<PatientsTreatmantRefarralsViewModel> referralsViewModel = new List<PatientsTreatmantRefarralsViewModel>();
            foreach (TreatmentReferral referral in referrals) {
                PatientsTreatmantRefarralsViewModel model = new PatientsTreatmantRefarralsViewModel(referral);
                referralsViewModel.Add(model);
            }
            Referrals = new ObservableCollection<PatientsTreatmantRefarralsViewModel>(referralsViewModel);
        }

    }
}
