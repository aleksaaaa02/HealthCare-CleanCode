using HealthCare.Model;
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
    public class AddDiseaseCommand : CommandBase
    {
        private readonly PatientInforamtionViewModel _viewModel;

        public AddDiseaseCommand(PatientInforamtionViewModel viewModel) 
        { 
            _viewModel = viewModel;
        }
        public override void Execute(object parameter)
        {
            string newDisease = _viewModel.Disease;
            if (newDisease is null)
            {
                Utility.ShowWarning("Morate uneti bolest u polje");
                return;
            }
            _viewModel.AddDisease(newDisease);
        }
    }
}
