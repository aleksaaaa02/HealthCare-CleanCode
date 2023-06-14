using HealthCare.Core.PatientHealthcare.Pharmacy;
using HealthCare.GUI.Command;

namespace HealthCare.GUI.DoctorGUI.Healthcare.Pharmacy;

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