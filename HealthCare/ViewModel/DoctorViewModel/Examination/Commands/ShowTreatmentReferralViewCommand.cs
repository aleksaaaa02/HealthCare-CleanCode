using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.View.DoctorView.ReferralView;
using System;

namespace HealthCare.ViewModel.DoctorViewModel.Examination.Commands
{
    public class ShowTreatmentReferralViewCommand : CommandBase
    {
        private Patient _examinedPatient;

        public ShowTreatmentReferralViewCommand(Patient patient) 
        {
            _examinedPatient = patient;
        }
        public override void Execute(object parameter)
        {
            new TreatmentReferralView(_examinedPatient).ShowDialog();
        }
    }
}
