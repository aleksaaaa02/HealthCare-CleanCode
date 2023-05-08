using HealthCare.Context;
using HealthCare.View.DoctorView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthCare.Command
{
    public class ShowPatientSearchViewCommand : CommandBase
    {
        private Hospital _hospital;

        public ShowPatientSearchViewCommand(Hospital hospital) {
            _hospital = hospital;
        
        }
        public override void Execute(object parameter)
        {
            new PatientSearchView(_hospital).Show();
        }
    }
}
