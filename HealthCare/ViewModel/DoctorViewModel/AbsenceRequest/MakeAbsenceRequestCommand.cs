
using System;
using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Exceptions;
using HealthCare.View;

namespace HealthCare.ViewModel.DoctorViewModel.AbsenceRequest
{
    public class MakeAbsenceRequestCommand : CommandBase
    {
        private readonly AbsenceRequestViewModel _absenceRequestViewModel;
        public MakeAbsenceRequestCommand(AbsenceRequestViewModel absenceRequestViewModel)
        {
            _absenceRequestViewModel = absenceRequestViewModel;
        }
        public override void Execute(object parameter)
        {
            try
            {
                
                MakeAbsenceRequest();
            }
            catch (ValidationException ve)
            {
                ViewUtil.ShowWarning(ve.Message);
            }
        }

        private void Validate()
        {
            if (_absenceRequestViewModel.AbsenceStartingDate != null)
            {

            }
        }

        private void MakeAbsenceRequest()
        {
            string DoctorJMBG = Context.Current.JMBG;
            string reason = _absenceRequestViewModel.AbsenceReason;
            int absenceDays = _absenceRequestViewModel.AbsenceDurationDays;
            DateTime startDate = _absenceRequestViewModel.AbsenceStartingDate;
        }
    }
}
