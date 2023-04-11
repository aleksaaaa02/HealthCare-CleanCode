using HealthCare.View.DoctorView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Command
{
    public class MakeAppointmentNavigationCommand : CommandBase   {
        public MakeAppointmentNavigationCommand() { }

        public override void Execute(object parameter)
        {
            MakeAppointmentView makeAppointmentView = new MakeAppointmentView();
            makeAppointmentView.Show();

        }
    }
}
