
using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;

namespace HealthCare.ViewModel.DoctorViewModel.Treatment
{
    public class TreatmentViewModel : ViewModelBase
    {
        private readonly Model.Treatment _treatment;
        public string PatientNameAndLastName { get; }
        public string PatientJMBG { get; }
        public int RoomId => _treatment.RoomId;
        public string Start => ViewUtil.ToString(_treatment.TreatmentDuration.Start, true);
        public string End => ViewUtil.ToString(_treatment.TreatmentDuration.End, true);

        public TreatmentViewModel(Model.Treatment treatment)
        {
            _treatment = treatment;
            TreatmentReferral referral = Injector.GetService<TreatmentReferralService>().Get(treatment.ReferralId);
            Patient patient = Injector.GetService<PatientService>().Get(referral.PatientJMBG);
            PatientNameAndLastName = patient.Name + " " + patient.LastName;
            PatientJMBG = patient.JMBG;
        }
    }
}
