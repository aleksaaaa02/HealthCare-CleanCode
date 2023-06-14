using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service.UserService;
using HealthCare.View;

namespace HealthCare.GUI.DoctorGUI.AbsenceRequesting
{
    public class AbsenceRequestDTO
    {
        private AbsenceRequest _request;

        public AbsenceRequestDTO(AbsenceRequest request)
        {
            _request = request;
            Model.Doctor doctor = Injector.GetService<DoctorService>().Get(request.RequesterJMBG);
            EmployeeName = doctor.Name + " " + doctor.LastName;
        }

        public int Id => _request.Id;
        public string EmployeeName { get; }
        public string Reason => _request.Reason;
        public string Start => ViewUtil.ToString(_request.AbsenceDuration.Start, true);
        public string End => ViewUtil.ToString(_request.AbsenceDuration.End, true);
        public string IsApproved => ViewUtil.Translate(_request.IsApproved);
    }
}