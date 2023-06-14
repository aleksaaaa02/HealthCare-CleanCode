using System.Windows.Input;
using HealthCare.Core.PatientHealthcare.Pharmacy;
using HealthCare.View;
using HealthCare.ViewModel;

namespace HealthCare.GUI.DoctorGUI.Healthcare.Pharmacy;

public class MedicationViewModel : ViewModelBase
{
    private readonly Medication _medication;
    private bool _initialTherapy;

    public MedicationViewModel(Medication medication)
    {
        _medication = medication;
        _initialTherapy = false;

        ShowMedicationInformationCommand = new ShowMedicationCommand(medication);
    }

    public int MedicationId => _medication.Id;
    public string MedicationName => _medication.Name;
    public string Description => _medication.Description;
    public string Ingredients => ViewUtil.ToString(_medication.Ingredients);

    public bool InitialTherapy
    {
        get => _initialTherapy;
        set
        {
            _initialTherapy = value;
            OnPropertyChanged();
        }
    }

    public ICommand ShowMedicationInformationCommand { get; }
}