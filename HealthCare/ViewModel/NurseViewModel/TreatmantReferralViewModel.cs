using System.Collections.ObjectModel;
using HealthCare.Model;

namespace HealthCare.ViewModel.NurseViewModel
{
    internal class TreatmantReferralViewModel:ViewModelBase
    {
        private readonly ObservableCollection<TreatmentReferral> _referral;
    }
}
