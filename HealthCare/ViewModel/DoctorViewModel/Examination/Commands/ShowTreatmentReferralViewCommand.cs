using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.View.DoctorView.ReferralView;
using System;

namespace HealthCare.ViewModel.DoctorViewModel.Examination.Commands
{
    public class ShowTreatmentReferralViewCommand : CommandBase
    {
        private readonly Hospital _hospital;
        private Patient _examinedPatient;

        public ShowTreatmentReferralViewCommand(Hospital hospital, Patient patient) 
        {
            _hospital = hospital;
            _examinedPatient = patient;
        }
        public override void Execute(object parameter)
        {
            new TreatmentReferralView(_hospital, _examinedPatient).ShowDialog();
        }
    }
}
