using HealthCare.Command;
using HealthCare.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModel.ManagerViewModel.Command
{
    public class ApproveRequestCommand : CommandBase
    {
        private readonly AbsenceRequestListingViewModel _model;

        public ApproveRequestCommand(AbsenceRequestListingViewModel model)
        {
            _model = model;
        }

        public override void Execute(object parameter)
        {
            ViewUtil.ShowInformation("WOAww");
        }
    }
}
