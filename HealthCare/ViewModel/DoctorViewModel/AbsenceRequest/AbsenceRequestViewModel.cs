
using System;
using System.Windows.Input;

namespace HealthCare.ViewModel.DoctorViewModel.AbsenceRequest
{
    public class AbsenceRequestViewModel : ViewModelBase
    {
        public string AbsenceReason { get; set; }
        public int AbsenceDurationDays { get; set; }
        public DateTime AbsenceStartingDate { get; set; }
        public ICommand MakeAbsenceRequestCommand { get; }
        public AbsenceRequestViewModel()
        {
            MakeAbsenceRequestCommand = new MakeAbsenceRequestCommand(this);
        }

    }
}
