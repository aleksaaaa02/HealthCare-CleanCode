using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.View.DoctorView.ReferralView;

namespace HealthCare.ViewModel.DoctorViewModel.Examination.Commands
{
    public class ShowSpecialistReferralViewCommand : CommandBase
    {
        private readonly Hospital _hospital;
        private Patient _examinedPatient;
        public ShowSpecialistReferralViewCommand(Hospital hospital, Patient patient)
        {
            _hospital = hospital;
            _examinedPatient = patient;
        }

        public override void Execute(object parameter)
        {
            new SpecialistReferralView(_hospital, _examinedPatient).ShowDialog();
        }
    }
}
