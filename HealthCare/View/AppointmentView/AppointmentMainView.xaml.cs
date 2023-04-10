using HealthCare.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HealthCare.View.AppointmentView
{
    /// <summary>
    /// Interaction logic for AppointmentMainView.xaml
    /// </summary>
    public partial class AppointmentMainView : Window
    {
        public AppointmentMainView()
        {
            InitializeComponent();
            Doctor doc = new Doctor("Aleksa", "Vukomanovic", "123456789", DateTime.Now, "062173224", "Vuka Karadzica", "aleksa123", "radi", Gender.Male, "Hirurg");
            string[] bolesti = { "dijabetes", "sizofrenija" };
            MedicalRecord record = new MedicalRecord(185, 80, bolesti);
            Patient patient = new Patient("Dimitrije", "Gasic", "234567891", DateTime.Now, "06213214", "Trg Dositeja Obradovica 6", "gasara123", "123123", Gender.Male, false, record);
            TimeSlot nemamIdeju = new TimeSlot(DateTime.Now, new TimeSpan(0,30,0));
            Appointment appointment = new Appointment(patient, doc, nemamIdeju, false);

            Doctor doc2 = new Doctor("Jelena", "Karleusa", "123456789", DateTime.Now, "062173224", "Vuka Karadzica", "aleksa123", "radi", Gender.Male, "Hirurg");
            string[] bolesti2 = { "Insomnia" };
            MedicalRecord record2 = new MedicalRecord(185, 80, bolesti2);
            Patient patient2 = new Patient("Marija", "Serifovic", "234567891", DateTime.Now, "06213214", "Trg Dositeja Obradovica 6", "gasara123", "123123", Gender.Male, false, record2);
            TimeSlot nemamIdeju2 = new TimeSlot(DateTime.Now, new TimeSpan(0, 30, 0));
            Appointment appointment2 = new Appointment(patient2, doc2, nemamIdeju2, false);



            List<Appointment> appointments = new List<Appointment>();
            appointments.Add(appointment);
            appointments.Add(appointment2);
            appListView.ItemsSource = new ObservableCollection<Appointment>(appointments);
        }
    }
}
