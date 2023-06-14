using HealthCare.GUI.Command;

namespace HealthCare.GUI.DoctorGUI.PatientMedicalRecord.Command;

public class ShowPatientSearchViewCommand : CommandBase
{
    public override void Execute(object parameter)
    {
        new PatientSearchView().Show();
    }
}