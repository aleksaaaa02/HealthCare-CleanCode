using HealthCare.Command;
using HealthCare.Model;
using HealthCare.View.DoctorView.PrescriptionView;

namespace HealthCare.ViewModel.DoctorViewModel.Treatment
{
    public class ShowAddTherapyDialogCommand : CommandBase
    {
        private readonly Therapy _therapy;
        private readonly DoctorTreatmentVisitViewModel _viewModel;
        public ShowAddTherapyDialogCommand(DoctorTreatmentVisitViewModel viewModel ,Therapy therapy)
        {
            _therapy = therapy;
            _viewModel = viewModel;
        }
        public override void Execute(object parameter)
        {
            new PrescriptionView(_therapy).ShowDialog();
            _viewModel.Update();
        }
    }
}
