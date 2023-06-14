using HealthCare.Core.Users.Model;
using HealthCare.WPF.Common;

namespace HealthCare.WPF.DoctorGUI.PatientHealthcare.Referral;

public class DoctorsViewModel : ViewModelBase
{
    private readonly Doctor _doctor;


    public DoctorsViewModel(Doctor doctor)
    {
        _doctor = doctor;
    }

    public string JMBG => _doctor.JMBG;
    public string NameAndLastName => _doctor.Name + " " + _doctor.LastName;
    public string Specialization => _doctor.Specialization;
}