using HealthCare.Core.Users.Model;
using HealthCare.GUI.Command;

namespace HealthCare.GUI.DoctorGUI.Healthcare.Referral.Command;

public class ShowTreatmentReferralViewCommand : CommandBase
{
    private readonly Patient _examinedPatient;

    public ShowTreatmentReferralViewCommand(Patient patient)
    {
        _examinedPatient = patient;
    }

    public override void Execute(object parameter)
    {
        new TreatmentReferralView(_examinedPatient).ShowDialog();
    }
}