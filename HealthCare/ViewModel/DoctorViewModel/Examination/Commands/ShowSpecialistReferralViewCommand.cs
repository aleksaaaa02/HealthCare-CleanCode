using HealthCare.Command;
using HealthCare.Model;
using HealthCare.View.DoctorView.ReferralView;

namespace HealthCare.ViewModel.DoctorViewModel.Examination.Commands;

public class ShowSpecialistReferralViewCommand : CommandBase
{
    private readonly Patient _examinedPatient;

    public ShowSpecialistReferralViewCommand(Patient patient)
    {
        _examinedPatient = patient;
    }

    public override void Execute(object parameter)
    {
        new SpecialistReferralView(_examinedPatient).ShowDialog();
    }
}