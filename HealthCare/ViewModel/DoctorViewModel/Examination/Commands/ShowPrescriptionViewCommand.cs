using HealthCare.Command;
using HealthCare.Context;
using HealthCare.Model;
using HealthCare.View.DoctorView.PrescriptionView;

namespace HealthCare.ViewModel.DoctorViewModel.Examination.Commands
{
    public class ShowPrescriptionViewCommand : CommandBase
    {
        private readonly Hospital _hospital;
        private readonly Patient _patient;
        public ShowPrescriptionViewCommand(Hospital hospital, Patient patient) 
        {
            _hospital = hospital;
            _patient = patient;
        }

        public override void Execute(object parameter)
        {
            new PrescriptionView(_hospital, _patient).ShowDialog();
        }
    }
}
