using HealthCare.Model;
using HealthCare.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Command
{
    public class TestCommandModelView : CommandBase
    {
        private ObservableCollection<AppointmentViewModel> Views;

        public TestCommandModelView(ObservableCollection<AppointmentViewModel> views) 
        {
            Views = views;
        }

        public override void Execute(object parameter)
        {
            Doctor doc = new Doctor("Aleksa", "Vukomanovic", "123456789", DateTime.Now, "062173224", "Vuka Karadzica", "aleksa123", "radi", Gender.Male, "Hirurg");
            string[] bowesti = { "dijabetes", "sizofrenija" };

            MedicalRecord record = new MedicalRecord(185, 80, bowesti);
            Patient patient = new Patient("Dimitrije", "Gasic", "234567891", DateTime.Now, "06213214", "Trg Dositeja Obradovica 6", "gasara123", "123123", Gender.Male, false, record);

            Views.Add(new AppointmentViewModel(new Appointment(patient, doc, new TimeSlot(DateTime.Now, TimeSpan.FromMinutes(30)), true)));

        }
    }
}
