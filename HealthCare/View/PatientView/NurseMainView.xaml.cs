using HealthCare.Context;
using HealthCare.Model;
using HealthCare.Observer;
using HealthCare.Service;
using HealthCare.ViewModel.NurseViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
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
    /// Interaction logic for NurseMainView.xaml
    /// </summary>
    /// 
    public partial class NurseMainView : Window
    {
        private PatientService patientService;
        private PatientViewModel vm;
        private Patient? patient;
        public MedicalRecord? Record;

        public NurseMainView(Hospital hospital)
        {
            InitializeComponent();

            patientService = hospital.PatientService;

            vm = new PatientViewModel(patientService);
            DataContext = vm;

            patientService.Load();

            vm.Update();
            patient = null;
            Record = null;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                CreatePatient();
                if (!patientService.CreateAccount(patient))
                    ShowErrorMessageBox("Pacijent sa unetim JMBG vec postoji");
                patientService.Save();
                Record = null;
                vm.Update();
            }
            else
                ShowErrorMessageBox("Unesite sva polja. Datum je u formatu dd-MM-YYYY");
   
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (patient == null)
            {
                ShowErrorMessageBox("Nije selektovan nalog.");
                return;
            }

            string messageBoxText = "Da li ste sigurni da zelite da obrisete pacijenta?";
            string caption = "Potvrda";
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxResult result;
            result = MessageBox.Show(messageBoxText, caption, button, icon);

            if(result == MessageBoxResult.Yes)
            {
                patient = (Patient) lvPatients.SelectedItem;
                if (!patientService.DeleteAccount(patient.JMBG))
                {
                    ShowErrorMessageBox("Pacijent sa unetim JMBG ne postoji");
                }
                patientService.Save();
                ClearBoxes();
                vm.Update();
            }
        }

        private void lvPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            patient = (Patient) lvPatients.SelectedItem;
            if (patient is null) { return; }
            tbName.Text = patient.Name;
            tbLastName.Text = patient.LastName;
            tbJMBG.Text = patient.JMBG;
            tbAddress.Text = patient.Address;
            tbPassword.Text = patient.Password;
            tbPhoneNumber.Text = patient.PhoneNumber;
            tbUsername.Text = patient.UserName;
            tbBirthDate.Text = patient.BirthDate.ToString();
            if (patient.Gender == Gender.Male)
                cbMale.IsChecked = true;
            else cbFemale.IsChecked = true;
            if (patient.Blocked == true)
                chbBlocked.IsChecked = true;
            else chbBlocked.IsChecked = false;
            Record = patient.MedicalRecord;
        }
        private void MedicalRecord_Click(object sender, RoutedEventArgs e)
        {
            AddMedicalRecordView medicalRecordView = new AddMedicalRecordView(this, patientService);
            medicalRecordView.ShowDialog();
            
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                CreatePatient();
                if(!patientService.UpdateAccount(patient))
                    ShowErrorMessageBox("Pacijent sa unetim JMBG ne postoji");
                patientService.Save();
                vm.Update();
            }
            else
                ShowErrorMessageBox("Unesite sva polja. Datum je u formatu dd-MM-YYYY");
        }

        public void CreatePatient()
        {
            patient = new Patient();
            patient.Name = tbName.Text;
            patient.LastName = tbLastName.Text;
            patient.JMBG = tbJMBG.Text;
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

        public void ClearBoxes()
        {
            tbName.Clear();
            tbLastName.Clear();
            tbAddress.Clear();
            tbBirthDate.Clear();
            tbUsername.Clear();
            tbPassword.Clear();
            tbJMBG.Clear();
            tbPhoneNumber.Clear();
            patient = null;
            Record = null;
        }

        public bool ValidateFields()
        {
            DateTime parsed;
            if (tbName.Text != "" && tbLastName.Text != "" && tbAddress.Text!="" && tbBirthDate.Text!="" && 
                DateTime.TryParse(tbBirthDate.Text,out parsed) && tbJMBG.Text!="" && tbPhoneNumber.Text!="" &&
                tbUsername.Text!="" && tbPassword.Text!="")
                return true;
            return false;
        }

        public void ShowErrorMessageBox(string messageBoxText)
        {
            string content = "Greska";
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBox.Show(messageBoxText, content, button, icon);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearBoxes();
        }

    }
}
