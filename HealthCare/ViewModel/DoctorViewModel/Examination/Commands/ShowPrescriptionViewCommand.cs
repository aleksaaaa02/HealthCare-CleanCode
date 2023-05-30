using HealthCare.Command;
using HealthCare.Model;
using HealthCare.View.DoctorView.PrescriptionView;

namespace HealthCare.ViewModel.DoctorViewModel.Examination.Commands;

public class ShowPrescriptionViewCommand : CommandBase
{
    private readonly Patient _patient;

    public ShowPrescriptionViewCommand(Patient patient)
    {
        _patient = patient;
    }

    public override void Execute(object parameter)
    {
        new PrescriptionView(_patient).ShowDialog();
    }
}