using HealthCare.Core.Users.Model;
using HealthCare.GUI.Command;

namespace HealthCare.GUI.DoctorGUI.Healthcare.Referral.Command;

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