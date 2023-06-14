using HealthCare.Command;
using HealthCare.Model;

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