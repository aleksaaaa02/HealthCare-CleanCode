using HealthCare.Application;
using HealthCare.Command;
using HealthCare.Exceptions;
using HealthCare.Service;
using HealthCare.View;

namespace HealthCare.ViewModel.ManagerViewModel.Command
{
    public class ManageRequestCommand : CommandBase
    {
        private readonly AbsenceRequestListingViewModel _model;
        private readonly bool _approve;

        public ManageRequestCommand(AbsenceRequestListingViewModel model, bool approve)
        {
            _model = model;
            _approve = approve;
        }

        public override void Execute(object parameter)
        {
            var service = Injector.GetService<AbsenceRequestService>();
            try
            {
                var request = _model.TryGetSelectedRequest();
                request.IsApproved = _approve;
                service.ManageRequest(request);
            }
            catch (ValidationException ve)
            {
                ViewUtil.ShowWarning(ve.Message);
            }

            _model.LoadAll();
        }
    }
}
