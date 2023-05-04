using HealthCare.Context;
using HealthCare.View.DoctorView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Command
{
    public class SavePatientChangesCommand : CommandBase
    {
        private readonly PatientInforamtionViewModel _viewModel;
        private Hospital _hospital;
        public SavePatientChangesCommand(Hospital hospital, PatientInforamtionViewModel viewModel) {
            _hospital = hospital;
            _viewModel = viewModel;
        }
        public override void Execute(object parameter)
        {
            _hospital.PatientService.Save();
        }
    }
}
