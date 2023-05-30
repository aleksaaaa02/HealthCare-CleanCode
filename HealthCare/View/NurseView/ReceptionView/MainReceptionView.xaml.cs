using System.Windows;
using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.Service.ScheduleService;
using HealthCare.View.NurseView;

namespace HealthCare.View.ReceptionView
{
    public partial class MainReceptionView : Window
    {
        private readonly PatientSchedule _patientSchedule;
        private readonly PatientService _patientService;

        public MainReceptionView()
        {
            InitializeComponent();
            _patientService = Injector.GetService<PatientService>();
            _patientSchedule = Injector.GetService<PatientSchedule>();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string JMBG = tbJMBG.Text.Trim();
            Patient? patient = _patientService.TryGet(JMBG);

            if (patient is null)
            {
                new CreatePatientView(JMBG).ShowDialog();
                return;
            }

            Appointment? starting = _patientSchedule.TryGetReceptionAppointment(patient.JMBG);
            if (starting is null)
            {
                ViewUtil.ShowWarning("Pacijent nema preglede u narednih 15 minuta.");
                return;
            }

            new NurseAnamnesisView(starting.AppointmentID, patient).ShowDialog();
        }
    }
}