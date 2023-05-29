using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.Service.ScheduleTest;
using HealthCare.View.PatientView;
using System.Windows;

namespace HealthCare.View.ReceptionView
{
    public partial class MainReceptionView : Window 
    {
        private readonly PatientService _patientService;
        private readonly PatientSchedule _patientSchedule;
        public MainReceptionView()
        {
            InitializeComponent();
            _patientService = Injector.GetService<PatientService>();
            _patientSchedule = new PatientSchedule();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string JMBG = tbJMBG.Text.Trim();
            Patient? patient = _patientService.TryGet(JMBG);

            if(patient is null)
            {
                new CreatePatientView(JMBG).ShowDialog();
                return;
            }

            Appointment? starting = _patientSchedule.TryGetReceptionAppointment(patient);
            if (starting is null)
            {
                ViewUtil.ShowWarning("Pacijent nema preglede u narednih 15 minuta.");
                return;
            }
            new NurseAnamnesisView(starting.AppointmentID, patient).ShowDialog();
        }
    }
}
