using HealthCare.Application;
using HealthCare.Model;
using HealthCare.Service;
using System.Windows;
using System.Windows.Documents;

namespace HealthCare.View.PatientView
{
    public partial class NurseAnamnesisView : Window
    {
        private readonly AnamnesisService _anamnesisService;
        private readonly PatientService _patientService;
        private readonly int _appointmentId;
        private readonly Patient _patient;

        public NurseAnamnesisView(int appointmentID, Patient patient)
        {
            InitializeComponent();
            _anamnesisService = Injector.GetService<AnamnesisService>();
            _patientService = Injector.GetService<PatientService>();

            _appointmentId = appointmentID;
            _patient = patient;

            string allergies = Utility.ToString(patient.MedicalRecord.Allergies);
            string medicalHistory = Utility.ToString(patient.MedicalRecord.MedicalHistory);
            rtbAllergies.AppendText(allergies);
            rtbMedicalHistory.AppendText(medicalHistory);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Anamnesis anamnesis = new Anamnesis();

            TextRange textRange = new TextRange(
                   rtbAllergies.Document.ContentStart,
                   rtbAllergies.Document.ContentEnd);
            _patient.MedicalRecord.Allergies = Utility.GetStringList(textRange.Text);

            textRange = new TextRange(
                   rtbSymptoms.Document.ContentStart,
                   rtbSymptoms.Document.ContentEnd);
            anamnesis.Symptoms = Utility.GetStringList(textRange.Text);

            textRange = new TextRange(
                   rtbMedicalHistory.Document.ContentStart,
                   rtbMedicalHistory.Document.ContentEnd);
            _patient.MedicalRecord.MedicalHistory = Utility.GetStringList(textRange.Text);

            int newID = _anamnesisService.Add(anamnesis);
            Schedule.GetAppointment(_appointmentId).AnamnesisID = newID;

            _patientService.Update(_patient);
            Close();
        }
    }
}
