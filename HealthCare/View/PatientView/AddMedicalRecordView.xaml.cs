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
    /// <summary>
    /// Interaction logic for AddMedicalRecordView.xaml
    /// </summary>
    public partial class AddMedicalRecordView : Window
    {
        private Patient? patient;
        private PatientService? patientService;
        public AddMedicalRecordView(Patient patient,PatientService patientService)
        {
            InitializeComponent();
            if (patient != null)
            {
                this.patient = patient;
                this.patientService = patientService;
                tbHeight.Text = patient.MedicalRecord.Height.ToString();
                tbWidth.Text = patient.MedicalRecord.Weight.ToString();
                rtbMedicalHistory.AppendText(string.Join(",",patient.MedicalRecord.MedicalHistory));
            }   
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            MedicalRecord medicalRecord = new MedicalRecord();
            medicalRecord.Height = float.Parse(tbHeight.Text);
            medicalRecord.Weight = float.Parse(tbWidth.Text);
            TextRange textRange = new TextRange(
                rtbMedicalHistory.Document.ContentStart,
                rtbMedicalHistory.Document.ContentEnd
            );
            medicalRecord.MedicalHistory = textRange.Text.Trim().Split(",");
            patient.MedicalRecord = medicalRecord;
            patientService.UpdateAccount(patient);
            patientService.Save();
            Close();
        }
    }
}
