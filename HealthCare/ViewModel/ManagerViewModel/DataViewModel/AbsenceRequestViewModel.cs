using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View;

namespace HealthCare.ViewModel.ManagerViewModel.DataViewModel
{
    public class AbsenceRequestViewModel
    {
        private AbsenceRequest _request;

        public int Id => _request.Id;
        public string EmployeeName { get; }
        public string Reason => _request.Reason;
        public string Start => ViewUtil.ToString(_request.AbsenceDuration.Start, true);
        public string End => ViewUtil.ToString(_request.AbsenceDuration.End, true);
        public string IsApproved => ViewUtil.Translate(_request.IsApproved);

        public AbsenceRequestViewModel(AbsenceRequest request)
        {
            _request = request;
            Doctor doctor = Injector.GetService<DoctorService>().Get(request.RequesterJMBG);
            EmployeeName = doctor.Name + " " + doctor.LastName;
        }
    }
}
