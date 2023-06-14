
using System.Windows;
using System.Windows.Forms;
using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Service;
using HealthCare.View.DoctorView.TreatmentView;
using MessageBox = System.Windows.MessageBox;

namespace HealthCare.ViewModel.DoctorViewModel.Treatment
{
    public class ReleasePatientCommand : CommandBase
    {
        private readonly Model.Treatment _treatment;
        private readonly Window _window;
        private readonly TreatmentService _treatmentService;
        public ReleasePatientCommand(Window window, Model.Treatment treatment)
        {
            _treatmentService = Injector.GetService<TreatmentService>();

            _window = window;
            _treatment = treatment;
        }
        
        public override void Execute(object parameter)
        {

            MessageBoxResult answer = MessageBox.Show("Da li je potrebna kontrola?", "Kontrola nakon otpusta", MessageBoxButton.YesNo,
                MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes)
            {
                new PatientReleaseAppointmentView(_treatment).ShowDialog();
            }

            _treatmentService.Remove(_treatment.Id);
            _window.Close();

        }
    }
}
