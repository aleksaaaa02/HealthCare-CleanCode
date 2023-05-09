using HealthCare.View;
using HealthCare.View.DoctorView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthCare.Command
{
    public class RemoveDiseaseCommand : CommandBase
    {

        private readonly PatientInforamtionViewModel _view;
        public RemoveDiseaseCommand(PatientInforamtionViewModel view) {
            _view = view;
        }
        public override void Execute(object parameter)
        {
            string disease = _view.SelectedDisease;
            if(disease is null)
            {
                Utility.ShowWarning("Morate odabrati bolest koju zelite da uklonite.");
                return;
            }
            _view.RemoveDisease(disease);
        }
    }
}
