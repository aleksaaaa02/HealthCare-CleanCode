using HealthCare.Command;
using HealthCare.Model;
using HealthCare.View.DoctorView.PrescriptionView;

namespace HealthCare.ViewModel.DoctorViewModel.Referrals.Commands
{
    public class ShowMedicationCommand : CommandBase
    {
        private readonly Medication _medication;
        public ShowMedicationCommand(Medication medication) 
        {
            _medication = medication;
        }

        public override void Execute(object parameter)
        {
            new MedicationInformationView(_medication).ShowDialog();
        }
    }
}
