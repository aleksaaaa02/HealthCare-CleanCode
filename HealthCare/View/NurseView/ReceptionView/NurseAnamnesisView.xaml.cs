using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using System;
using System.Collections.Generic;
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

namespace HealthCare.View.PatientView
{
    public partial class NurseAnamnesisView : Window
    {
        private readonly Hospital hospital = new Hospital();
        private readonly int appointmentID;
        private readonly Patient patient;
        public NurseAnamnesisView(Hospital hospital,int appointmentID, Patient patient)
        {
            InitializeComponent();
            this.hospital = hospital;
            this.appointmentID = appointmentID;
            this.patient = patient;
            rtbAllergies.Document.Blocks.Add(new Paragraph(new Run(patient.MedicalRecord.AllergiesToString())));
            rtbMedicalHistory.Document.Blocks.Add(new Paragraph(new Run(patient.MedicalRecord.MedicalHistoryToString())));
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
                   rtbAllergies.Document.ContentEnd
               );
            patient.MedicalRecord.Allergies = textRange.Text.Trim().Split(",");

            textRange = new TextRange(
                   rtbSymptoms.Document.ContentStart,
                   rtbSymptoms.Document.ContentEnd
               );
            anamnesis.Symptoms = textRange.Text.Trim().Split(",");

            textRange = new TextRange(
                   rtbMedicalHistory.Document.ContentStart,
                   rtbMedicalHistory.Document.ContentEnd
               );
            patient.MedicalRecord.MedicalHistory = textRange.Text.Trim().Split(",");

            int newID = hospital.AnamnesisService.AddAnamnesis(anamnesis);
            Schedule.GetAppointment(appointmentID).AnamnesisID = newID;
            hospital.PatientService.UpdateAccount(patient);
            hospital.SaveAll();
            Close();
        }
    }
}
