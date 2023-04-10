﻿using HealthCare.Model;
using HealthCare.Observer;
using HealthCare.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class NurseMainView : Window,IObserver,INotifyPropertyChanged
    {
        private PatientService patientService;
        public ObservableCollection<Patient> Patients { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public NurseMainView()
        {
            InitializeComponent();
            DataContext = this;
            patientService = new PatientService("..\\..\\..\\da.csv");
            patientService.Load();
            patientService.Subscribe(this);
            lvPatients.ItemsSource = patientService.Patients;
            Patients = new ObservableCollection<Patient>(patientService.Patients);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = new Patient();
            patient.Name = tbName.Text;
            patient.LastName = tbLastName.Text;
            patient.JMBG = tbJMBG.Text;
            patient.BirthDate = DateTime.Parse(tbBirthDate.Text);
            patient.PhoneNumber = tbPhoneNumber.Text;
            patient.Address = tbAddress.Text;
            if (cbMale.IsChecked is bool Checked && Checked){
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
            patientService.CreateAccount(patient);
            patientService.Save();

            lvPatients.ItemsSource = patientService.Patients;

        }

        public void Update()
        {
            Patients.Clear();
            foreach(var patient in patientService.Patients)
            {
                Patients.Add(patient);
            }
        }
    }
}
