using HealthCare.Command;
using HealthCare.Context;
using HealthCare.View;
using System;

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
            Utility.ShowInformation("Not Yet Implemented");


        }
    }
}
