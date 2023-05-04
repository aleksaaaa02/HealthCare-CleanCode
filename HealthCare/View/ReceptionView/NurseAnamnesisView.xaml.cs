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
        public NurseAnamnesisView(Hospital hospital,int appointmentID)
        {
            InitializeComponent();
            this.hospital = hospital;
            this.appointmentID = appointmentID;
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
            anamnesis.Allergies = textRange.Text.Trim().Split(",");

            textRange = new TextRange(
                   rtbSymptoms.Document.ContentStart,
                   rtbSymptoms.Document.ContentEnd
               );
            anamnesis.Symptoms = textRange.Text.Trim().Split(",");

            textRange = new TextRange(
                   rtbMedicalHistory.Document.ContentStart,
                   rtbMedicalHistory.Document.ContentEnd
               );
            anamnesis.MedicalHistory = textRange.Text.Trim().Split(",");

            int newID = hospital.AnamnesisService.AddAnamnesis(anamnesis);
            Schedule.GetAppointment(appointmentID).AnamnesisID = newID;
        }
    }
}
