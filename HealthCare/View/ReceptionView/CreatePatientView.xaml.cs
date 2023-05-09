using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Service;
using HealthCare.View.PatientView;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace HealthCare.View.ReceptionView
{
    public partial class CreatePatientView : Window
    {
        private PatientService patientService;
        private Patient? patient;
        public MedicalRecord? Record;
        private string JMBG;
        public CreatePatientView(Hospital hospital,string JMBG)
        {
            InitializeComponent();
            patientService = hospital.PatientService;

            patient = null;
            Record = null;
            this.JMBG = JMBG;
            tbJMBG.IsEnabled = false;
            tbJMBG.Text = JMBG;
        }

        private void btnMedicalRecord_Click(object sender, RoutedEventArgs e)
        {
            AddMedicalRecordView medicalRecordView = new AddMedicalRecordView(this, patientService);
            medicalRecordView.ShowDialog();
        }
        public bool ValidateFields()
        {
            if (tbName.Text != "" && tbLastName.Text != "" && tbAddress.Text != "" && tbBirthDate.Text != "" &&
                DateTime.TryParse(tbBirthDate.Text, out _) && tbJMBG.Text != "" && tbPhoneNumber.Text != "" &&
                tbUsername.Text != "" && tbPassword.Text != "")
                return true;
            return false;
        }

        public void CreatePatient()
        {
            patient = new Patient();
            patient.Name = tbName.Text;
            patient.LastName = tbLastName.Text;
            patient.JMBG = JMBG;
            patient.BirthDate = DateTime.Parse(tbBirthDate.Text);
            patient.PhoneNumber = tbPhoneNumber.Text;
            patient.Address = tbAddress.Text;
            if (cbMale.IsChecked is bool Checked && Checked)
            {
                patient.Gender = Gender.Male;
            }
            else patient.Gender = Gender.Female;
            patient.UserName = tbUsername.Text;
            patient.Password = tbPassword.Text;
            if (chbBlocked.IsChecked is bool CheckedBlocked && CheckedBlocked)
            {
                patient.Blocked = true;
            }
            else patient.Blocked = false;
            if (Record is null)
                Record = new MedicalRecord();
            patient.MedicalRecord = Record;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                CreatePatient();
                if (!patientService.CreateAccount(patient))
                    ShowErrorMessageBox("Pacijent sa unetim JMBG vec postoji");
                Record = null;
                Close();
            }
            else
                ShowErrorMessageBox("Unesite sva polja. Datum je u formatu dd-MM-YYYY");
        }
        public void ShowErrorMessageBox(string messageBoxText)
        {
            Utility.ShowInformation(messageBoxText);
        }

    }
}
