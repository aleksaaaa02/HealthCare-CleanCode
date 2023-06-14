
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthCare.Application;
using HealthCare.Service;

namespace HealthCare.ViewModel.DoctorViewModel.AbsenceRequests
{
    public class AbsenceRequestViewModel : ViewModelBase
    {
        private readonly AbsenceRequestService _absenceRequestService;
        private ObservableCollection<ManagerViewModel.DataViewModel.AbsenceRequestViewModel> _doctorRequests;
        public IEnumerable<ManagerViewModel.DataViewModel.AbsenceRequestViewModel> DoctorRequests => _doctorRequests;
        public string AbsenceReason { get; set; }
        public int AbsenceDurationDays { get; set; }
        public DateTime AbsenceStartingDate { get; set; }
        public ICommand MakeAbsenceRequestCommand { get; }
        public AbsenceRequestViewModel()
        {
            _absenceRequestService = Injector.GetService<AbsenceRequestService>();
            _doctorRequests = new ObservableCollection<ManagerViewModel.DataViewModel.AbsenceRequestViewModel>();
            AbsenceStartingDate = DateTime.Today.AddDays(2); 

            MakeAbsenceRequestCommand = new MakeAbsenceRequestCommand(this);

            Update();
        }

        public void Update()
        {
            _doctorRequests.Clear();
            foreach(var request in _absenceRequestService.GetDoctorRequests(Context.Current.JMBG))
            {
                _doctorRequests.Add(new ManagerViewModel.DataViewModel.AbsenceRequestViewModel(request));
            }

        }

    }
}
