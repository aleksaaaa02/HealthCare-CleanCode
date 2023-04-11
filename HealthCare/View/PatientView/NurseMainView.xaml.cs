using HealthCare.Model;
using HealthCare.Observer;
using HealthCare.Service;
using HealthCare.ViewModel;
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

        public NurseMainView()
        {
            InitializeComponent();

            vm = new PatientViewModel();
            DataContext = vm;

            patientService = new PatientService("../../../Resources/patients.csv");
            patientService.Load();

            UpdateViewModel();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            CreatePatient();
            patientService.CreateAccount(patient);
            patientService.Save();
        }

        public void UpdateViewModel()
        {
            vm.Patients = new ObservableCollection<Patient>(patientService.Patients);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            patient = (Patient) lvPatients.SelectedItem;
            patientService.DeleteAccount(patient.JMBG);
            ClearBoxes();
        }

        private void lvPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            patient = (Patient) lvPatients.SelectedItem;
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


        }
        private void MedicalRecord_Click(object sender, RoutedEventArgs e)
        {
            AddMedicalRecordView medicalRecordView = new AddMedicalRecordView(patient,patientService);
            medicalRecordView.ShowDialog();
            
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            CreatePatient();
            patientService.UpdateAccount(patient);
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
            patient.MedicalRecord = new MedicalRecord();
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
        }
    }
}
